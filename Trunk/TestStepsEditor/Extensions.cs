using System;

namespace TestStepsEditor
{
	public static class Extensions
	{
		public static bool HasValue(this String str)
		{
			return !String.IsNullOrEmpty(str);
		}
	}
}
