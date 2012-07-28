using System;

namespace TestStepsEditor.Preferences
{
	public class BooleanPreference : Preference<bool>
	{
		public BooleanPreference(string regValueName) :
			base(
				regValueName,
				_ => true,
				b => b.ToString(),
				obj => Boolean.Parse((string)obj))
		{
		}
	}
}
