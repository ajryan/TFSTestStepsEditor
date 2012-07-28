namespace TestStepsEditor.Preferences
{
	public interface IPreference
	{
		string RegValueName { get; }
		bool ShouldSave();
		void SetFromRegistry(object regData);
		object GetValueForRegistry();
	}
}