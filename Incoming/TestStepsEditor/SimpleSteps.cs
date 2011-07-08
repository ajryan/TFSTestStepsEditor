using System.ComponentModel;
using System.Linq;

namespace TestStepsEditor
{
	public class SimpleSteps : BindingList<SimpleStep>
	{
		private int _originalCount;

		public SimpleSteps(int originalCount)
		{
			_originalCount = originalCount;

		}
		
		public bool Dirty
		{
			get
			{
				return 
					_originalCount != this.Count ||
					this.Any(simpleStep => simpleStep.Dirty);
			}
		}

		public void ResetDirtyState()
		{
			_originalCount = this.Count;

			foreach (var simpleStep in this)
			{
				simpleStep.ResetDirtyState();
			}
		}
	}
}