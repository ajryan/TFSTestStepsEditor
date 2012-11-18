using System.Collections.Generic;
using Microsoft.Win32;
using NLog;

namespace TestStepsEditor.Preferences
{
	public class PreferenceCollection
	{
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
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
			_logger.Info("Load preferences from registry at " + _regKeyBase);

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
            _logger.Info("Save preferences to registry at " + _regKeyBase);

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
