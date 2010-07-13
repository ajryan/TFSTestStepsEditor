using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.TestManagement.Client;
using Microsoft.Win32;

namespace TestStepsEditor
{
	public class ServerSettings
	{
		private const string RegKey = @"Lonza\TestEditor";

		public static void Load(out TfsTeamProjectCollection tfs, out ITestManagementTeamProject testProject)
		{
			if (LoadProjectSelectionFromRegistry(out tfs, out testProject))
				return;

			LoadProjectSelectionFromUser(out tfs, out testProject);
		}

		private static bool LoadProjectSelectionFromRegistry(out TfsTeamProjectCollection tfs, out ITestManagementTeamProject testProject)
		{
			tfs = null;
			testProject = null;

			var key = Registry.CurrentUser.OpenSubKey(RegKey);
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

			return true;
		}

		public static void LoadProjectSelectionFromUser(out TfsTeamProjectCollection tfs, out ITestManagementTeamProject testProject)
		{
			tfs = null;
			testProject = null;

			var projectPicker = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false);
			projectPicker.ShowDialog();

			if (projectPicker.SelectedTeamProjectCollection != null)
			{
				tfs = projectPicker.SelectedTeamProjectCollection;
				testProject = tfs.GetService<ITestManagementService>()
					.GetTeamProject(projectPicker.SelectedProjects[0].Name);

				SaveProjectSelectionToRegistry(tfs, testProject);
			}
		}

		private static void SaveProjectSelectionToRegistry(TfsTeamProjectCollection tfs, ITestManagementTeamProject testProject)
		{
			var key = Registry.CurrentUser.OpenSubKey(RegKey, true);
			if (key == null)
			{
				key = Registry.CurrentUser.CreateSubKey(RegKey);
				if (key == null)
					return;
			}

			key.SetValue("CollectionUri", tfs.Uri);
			key.SetValue("Project", testProject.WitProject.Name);
		}
	}
}
