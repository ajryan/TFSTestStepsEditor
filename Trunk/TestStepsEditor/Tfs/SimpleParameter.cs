using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TestStepsEditor.Tfs
{
	public class SimpleParameter
	{
		public string Name { get; private set; }
		public ReadOnlyCollection<string> Values { get; private set; }

		public SimpleParameter(string name, IList<string> values)
		{
			Name = name;
			Values = new ReadOnlyCollection<string>(values);
		}
	}
}
