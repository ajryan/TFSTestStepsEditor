using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Client;
using Microsoft.Win32;

namespace TestStepsEditor
{
	public class UserPreferences
	{
		private const string REG_KEY = @"Lonza\TestEditor";

		private const string REG_TFSURI = "CollectionUri";
		public Uri TfsUri { get; set; }

		private const string REG_TESTPROJECT = "Project";
		public string TestProject { get; set; }

		private const string REG_WORKITEMIDMRU = "WorkItemIdMRU";
		public BindingList<int> WorkItemIdMru { get; set; }

		private const string REG_WINDOWSIZE = "WindowSize";
		public Size? WindowSize { get; set; }

		private const string REG_WINDOWLOCATION = "WindowLocation";
		public Point? WindowLocation { get; set; }

		private const string REG_WITTOOLBARTOP = "WorkItemToolbarTop";
		public bool? WorkItemToolbarTop { get; set; }

		private const string REG_FINDTOOLBARTOP = "FindToolbarTop";
		public bool? FindToolbarTop { get; set; }

		private UserPreferences()
		{
		}

		public static UserPreferences Load()
		{
			return 
				LoadFromRegistry() ?? 
				LoadProjectSelectionFromUser() ??
				new UserPreferences();
		}

		public static UserPreferences LoadProjectSelectionFromUser()
		{
			Uri tfsUri = null;
			string testProject = null;
			BindingList<int> workItemIdMru = LoadMruFromRegistry();
			
			if (LoadProjectSelectionFromUser(out tfsUri, out testProject))
			{
				var newPreferences = new UserPreferences()
					{
						TfsUri = tfsUri, 
						TestProject = testProject, 
						WorkItemIdMru = workItemIdMru
					};

				newPreferences.SaveToRegistry();
				return newPreferences;
			}

			return null;
		}

		public void AddWorkItemIdMru(int id)
		{
			if (WorkItemIdMru.Contains(id))
				return;

			while (WorkItemIdMru.Count >= 10)
				WorkItemIdMru.RemoveAt(9);

			WorkItemIdMru.Insert(0, id);
		}

		public void SaveToRegistry()
		{
			var key = Registry.CurrentUser.OpenSubKey(REG_KEY, true);
			if (key == null)
			{
				key = Registry.CurrentUser.CreateSubKey(REG_KEY);
				if (key == null)
					return;
			}

			if (TfsUri != null && !String.IsNullOrEmpty(TfsUri.ToString()))
				key.SetValue(REG_TFSURI, TfsUri.ToString());
			
			if (!String.IsNullOrEmpty(TestProject))
				key.SetValue(REG_TESTPROJECT, TestProject);
			
			if (WorkItemIdMru != null && WorkItemIdMru.Count > 0)
				key.SetValue(REG_WORKITEMIDMRU, String.Join(",", WorkItemIdMru));

			if (WindowLocation.HasValue)
				key.SetValue(REG_WINDOWLOCATION, WindowLocation.Value.X + "," + WindowLocation.Value.Y);

			if (WindowSize.HasValue)
				key.SetValue(REG_WINDOWSIZE, WindowSize.Value.Width + "," + WindowSize.Value.Height);

			if (FindToolbarTop.HasValue)
				key.SetValue(REG_FINDTOOLBARTOP, FindToolbarTop.Value.ToString());

			if (WorkItemToolbarTop.HasValue)
				key.SetValue(REG_WITTOOLBARTOP, WorkItemToolbarTop.Value.ToString());
		}

		private static UserPreferences LoadFromRegistry()
		{
			try
			{
				return LoadFromRegistryUnsafe();
			}
			catch
			{
				// convenience feature. no need to expose errors.
				return null;
			}
		}

		private static UserPreferences LoadFromRegistryUnsafe()
		{
			var key = Registry.CurrentUser.OpenSubKey(REG_KEY);
			if (key == null)
				return null;

			var tfsUriReg = key.GetValue(REG_TFSURI) as string;
			if (tfsUriReg == null)
				return null;

			var testProjectReg = key.GetValue(REG_TESTPROJECT) as string;
			if (testProjectReg == null)
				return null;

			var workItemIdMru = LoadMruFromRegistry(key);

			Point? windowLocation = NullableFromRegistry(
				key, REG_WINDOWLOCATION,
				v =>
					{
						var windowLocTokens = v.Split(',');
						return new Point(
							Int32.Parse(windowLocTokens[0]),
							Int32.Parse(windowLocTokens[1]));
					});

			Size? windowSize = NullableFromRegistry(
				key, REG_WINDOWSIZE,
				v =>
					{
						var windowSizeTokens = v.Split(',');
						return new Size(
							Int32.Parse(windowSizeTokens[0]),
							Int32.Parse(windowSizeTokens[1]));
					});

			bool? findToolbarTop = NullableFromRegistry(key, REG_FINDTOOLBARTOP, Boolean.Parse);

			bool? witToolbarTop = NullableFromRegistry(key, REG_WITTOOLBARTOP, Boolean.Parse);

			return
				new UserPreferences()
					{
						TfsUri = String.IsNullOrEmpty(tfsUriReg)? null : new Uri(tfsUriReg),
						TestProject = testProjectReg,
						WorkItemIdMru = workItemIdMru,
						WindowLocation = windowLocation,
						WindowSize = windowSize,
						FindToolbarTop = findToolbarTop,
						WorkItemToolbarTop = witToolbarTop
					};
		}

		private static BindingList<int> LoadMruFromRegistry(RegistryKey key = null)
		{
			BindingList<int> workItemIdMru = new BindingList<int>();

			if (key == null)
			{
				key = Registry.CurrentUser.OpenSubKey(REG_KEY);
				if (key == null)
					return workItemIdMru;
			}
			
			var workItemIdMruString = key.GetValue(REG_WORKITEMIDMRU) as string;
			if (!String.IsNullOrWhiteSpace(workItemIdMruString))
			{
				workItemIdMru = new BindingList<int>(
					workItemIdMruString
					.Split(new []{','}, StringSplitOptions.RemoveEmptyEntries)
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

			return workItemIdMru;
		}

		private static bool LoadProjectSelectionFromUser(out Uri tfsUri, out string testProject)
		{
			tfsUri = null;
			testProject = null;

			try
			{
				using (var projectPicker = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false))
				{
					var userSelected = projectPicker.ShowDialog();

					if (userSelected == DialogResult.Cancel)
						return false;

					if (projectPicker.SelectedTeamProjectCollection != null)
					{
						tfsUri = projectPicker.SelectedTeamProjectCollection.Uri;
						testProject = projectPicker.SelectedProjects[0].Name;
					}
				}

				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error selecting team project: " + ex.Message, "Error");
				return false;
			}
		}

		private static T NullableFromRegistry<T>(RegistryKey key, string regName, Func<string, T> assignFunc)
		{
			T result = default(T);

			var regValue = key.GetValue(regName) as string;
			if (regValue != null)
			{
				result = assignFunc(regValue);
			}

			return result;
		}
	}
}
