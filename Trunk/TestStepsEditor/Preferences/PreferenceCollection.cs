using System.Collections.Generic;
using Microsoft.Win32;

namespace TestStepsEditor.Preferences
{
	public class PreferenceCollection
	{
		private readonly string _regKeyBase;

		public List<IPreference> Preferences { get; private set; }
		public bool Loaded { get; private set; }

		public PreferenceCollection(string regKeyBase)
		{
			_regKeyBase = regKeyBase;

			Preferences = new List<IPreference>();
		}

		public void LoadFromRegistry()
		{
			var key = Registry.CurrentUser.OpenSubKey(_regKeyBase);
			if (key == null)
				return;

			foreach (var preference in Preferences)
			{
				object regData = key.GetValue(preference.RegValueName);
				if (regData != null)
					preference.SetFromRegistry(regData);
			}

			Loaded = true;
		}

		public void SaveToRegistry()
		{
			var key = Registry.CurrentUser.OpenSubKey(_regKeyBase, true);
			if (key == null)
			{
				key = Registry.CurrentUser.CreateSubKey(_regKeyBase);
				if (key == null)
					return;
			}

			foreach (var preference in Preferences)
			{
				if (preference.ShouldSave())
				{
					key.SetValue(preference.RegValueName, preference.GetValueForRegistry());
				}
			}
		}
	}
}
