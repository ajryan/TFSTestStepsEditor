using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Client;
using TestStepsEditor.Preferences;

namespace TestStepsEditor
{
	public class UserPreferences
	{
		private readonly PreferenceCollection _preferenceCollection = new PreferenceCollection(@"Software\TestStepsEditor");

		public Preference<Uri> TfsUri { get; private set; }
		public Preference<string> TestProject { get; private set; }
		public Preference<bool> WorkItemToolbarTop { get; set; }
		public Preference<bool> FindToolbarTop { get; set; }
		public Preference<BindingList<int>> WorkItemIdMru { get; private set; }
		public Preference<Size> WindowSize { get; private set; }
		public Preference<Point> WindowLocation { get; private set; }

		public UserPreferences()
		{
			TfsUri = new Preference<Uri>(
				"CollectionUri",
				uri => uri != null && uri.ToString().HasValue(),
				uri => uri.ToString(),
				obj => new Uri((string)obj));

			TestProject = new Preference<string>(
				"Project",
				str => str.HasValue());

			WorkItemToolbarTop = new BooleanPreference("WorkItemToolbarTop");
			WorkItemToolbarTop.Value = true;

			FindToolbarTop = new BooleanPreference("FindToolbarTop");
			FindToolbarTop.Value = false;

			WorkItemIdMru = new Preference<BindingList<int>>(
				"WorkItemIdMRU",
				list => list != null && list.Count > 0,
				list => String.Join(",", list),
				IntBindingListFromObj);
			WorkItemIdMru.Value = new BindingList<int>();

			WindowSize = new Preference<Size>(
				"WindowSize",
				size => !size.IsEmpty,
				size => size.Width + "," + size.Height,
				obj =>
				{
					var sizeTokens = ((string)obj).Split(',');
					return new Size(
						Int32.Parse(sizeTokens[0]),
						Int32.Parse(sizeTokens[1]));
				});

			WindowLocation = new Preference<Point>(
				"WindowLocation",
				point => !point.IsEmpty,
				point => point.X + "," + point.Y,
				obj =>
				{
					var pointTokens = ((string)obj).Split(',');
					return new Point(
						Int32.Parse(pointTokens[0]),
						Int32.Parse(pointTokens[1]));
				});

			_preferenceCollection.Preferences.AddRange(
				new List<IPreference>
				{
					TfsUri, TestProject, WorkItemToolbarTop, FindToolbarTop,
					WorkItemIdMru, WindowSize, WindowLocation
				});
		}

		public void Save()
		{
			_preferenceCollection.SaveToRegistry();
		}

		public void Load()
		{
			_preferenceCollection.LoadFromRegistry();
		}

		public void LoadProjectSelectionFromUser()
		{
			TfsUri.Value = null;
			TestProject.Value = null;

			try
			{
				using (var projectPicker = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false))
				{
					var userSelected = projectPicker.ShowDialog();

					if (userSelected == DialogResult.Cancel)
						return;

					if (projectPicker.SelectedTeamProjectCollection != null)
					{
						TfsUri.Value = projectPicker.SelectedTeamProjectCollection.Uri;
						TestProject.Value = projectPicker.SelectedProjects[0].Name;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error selecting team project: " + ex.Message, "Error");
			}

			_preferenceCollection.SaveToRegistry();
		}

		public void AddWorkItemIdMru(int id)
		{
			if (WorkItemIdMru.Value == null)
				WorkItemIdMru.Value = new BindingList<int>();
			else if (WorkItemIdMru.Value.Contains(id))
				return;

			while (WorkItemIdMru.Value.Count >= 10)
				WorkItemIdMru.Value.RemoveAt(9);

			WorkItemIdMru.Value.Insert(0, id);
		}

		private static BindingList<int> IntBindingListFromObj(Object obj)
		{
			return new BindingList<int>(
				((string)obj)
					.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
					.Select(
						s =>
						{
							int n = 0;
							Int32.TryParse(s, out n);
							return n;
						})
					.Where(n => n > 0)
					.ToList());
		}
	}
}
