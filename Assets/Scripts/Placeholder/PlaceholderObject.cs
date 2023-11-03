using ComputerMaintenanceTraining.AssemblyObjects;
using UnityEngine;

namespace ComputerMaintenanceTraining.PlaceholderLogic
{
	public class PlaceholderObject : PlaceholderObjectBase
	{
		[SerializeField]
		private AssemblyObject _target;

		protected override AssemblyObject Target
		{
			get { return _target; }
		}
	}
}