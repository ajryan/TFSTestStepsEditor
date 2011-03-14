using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.TestManagement.Client;
using Microsoft.Win32;

namespace TestStepsEditor
{
	public class ServerSettings
	{
		public TfsTeamProjectCollection Tfs { get; private set; }
		public ITestManagementTeamProject TestProject { get; private set; }
		public BindingList<int> WorkItemIdMru { get; private set; }

		private ServerSettings(
			TfsTeamProjectCollection tfs, 
			ITestManagementTeamProject testProject,
			BindingList<int> workItemIdMru)
		{
			Tfs = tfs;
			TestProject = testProject;
			WorkItemIdMru = workItemIdMru;
		}

		private const string REG_KEY = @"Lonza\TestEditor";

		public static ServerSettings Load()
		{
			TfsTeamProjectCollection tfs = null;
			ITestManagementTeamProject testProject = null;
			BindingList<int> workItemIdMru = null;

			if (!LoadProjectSelectionFromRegistry(out tfs, out testProject, out workItemIdMru))
				LoadProjectSelectionFromUser(out tfs, out testProject);

			return new ServerSettings(tfs, testProject, workItemIdMru);
		}

		private static bool LoadProjectSelectionFromRegistry(
			out TfsTeamProjectCollection tfs, 
			out ITestManagementTeamProject testProject,
			out BindingList<int> workItemIdMru)
		{
			tfs = null;
			testProject = null;
			workItemIdMru = new BindingList<int>();

			var key = Registry.CurrentUser.OpenSubKey(REG_KEY);
			if (key == null)
				return false;

			var collectionValue = key.GetValue("CollectionUri") as string;
			if (collectionValue == null)
				return false;

			var projectValue = key.GetValue("Project") as string;
			if (projectValue == null)
				return false;

			tfs = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(collectionValue));
			testProject = tfs.GetService<ITestManagementService>().GetTeamProject(projectValue);

			workItemIdMru = LoadMruFromRegistry(key);

			return true;
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
			
			var workItemIdMruString = key.GetValue("WorkItemIdMRU") as string;
			if (workItemIdMruString != null)
				workItemIdMru = new BindingList<int>(workItemIdMruString.Split(',').Select(Int32.Parse).ToList());
			
			return workItemIdMru;
		}

		public static ServerSettings LoadProjectSelectionFromUser()
		{
			TfsTeamProjectCollection tfs = null;
			ITestManagementTeamProject testProject = null;
			BindingList<int> workItemIdMru = LoadMruFromRegistry();
			
			if (LoadProjectSelectionFromUser(out tfs, out testProject))
			{
				var selectedSettings = new ServerSettings(tfs, testProject, workItemIdMru);
				selectedSettings.SaveToRegistry();
				return selectedSettings;
			}

			return null;
		}

		private static bool LoadProjectSelectionFromUser(out TfsTeamProjectCollection tfs, out ITestManagementTeamProject testProject)
		{
			tfs = null;
			testProject = null;

			try
			{
				var projectPicker = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false);
				var userSelected = projectPicker.ShowDialog();

				if (userSelected == DialogResult.Cancel)
					return false;

				if (projectPicker.SelectedTeamProjectCollection != null)
				{
					tfs = projectPicker.SelectedTeamProjectCollection;
					testProject = tfs.GetService<ITestManagementService>()
						.GetTeamProject(projectPicker.SelectedProjects[0].Name);
				}

				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error selecting team project: " + ex.Message, "Error");
				return false;
			}
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

			key.SetValue("CollectionUri", Tfs.Uri);
			key.SetValue("Project", TestProject.WitProject.Name);
			key.SetValue("WorkItemIdMRU", String.Join(",", WorkItemIdMru));
		}
	}
}
