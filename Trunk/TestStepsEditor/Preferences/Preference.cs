using System;
using System.Diagnostics;

namespace TestStepsEditor.Preferences
{
	public class Preference<T> : IPreference
	{
		public T Value { get; set; }
		public string RegValueName { get; set; }
		public Predicate<T> ShouldSavePredicate { get; set; }
		public Func<T, object> ToRegFunc { get; set; }
		public Func<object, T> FromRegFunc { get; set; }
		
		public Preference(string regValueName)
			: this(regValueName, t => t != null)
		{
			Debug.Assert(!(typeof (T).IsValueType));
		}

		public Preference(string regValueName, Predicate<T> shouldSavePredicate)
		{
			RegValueName = regValueName;
			ShouldSavePredicate = shouldSavePredicate;
			ToRegFunc = t => t;
			FromRegFunc = obj => (T)obj;
		}

		public Preference(
			string regValueName, 
			Predicate<T> shouldSavePredicate, 
			Func<T, object> toRegFunc, 
			Func<object, T> fromRegFunc)
		{
			RegValueName = regValueName;
			ShouldSavePredicate = shouldSavePredicate;
			ToRegFunc = toRegFunc;
			FromRegFunc = fromRegFunc;
		}

		public static implicit operator T(Preference<T> preference)
		{
			return preference.Value;
		}

		public bool ShouldSave()
		{
			return ShouldSavePredicate(Value);
		}

		public void SetFromRegistry(object regData)
		{
			Value = FromRegFunc(regData);
		}

		public object GetValueForRegistry()
		{
			return ToRegFunc(Value);
		}

		public override string ToString()
		{
			if (ReferenceEquals(Value, null))
				return "<null>";

			return Value.ToString();
		}
	}
}