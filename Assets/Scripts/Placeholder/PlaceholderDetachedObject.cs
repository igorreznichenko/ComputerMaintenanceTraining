using ComputerMaintenanceTraining.AssemblyObjects;
using ComputerMaintenanceTraining.AssemblyObjects.Detachables;
using ComputerMaintenanceTraining.Enums;
using Oculus.Interaction;
using UnityEngine;

namespace ComputerMaintenanceTraining.PlaceholderLogic
{
	public class PlaceholderDetachedObject : PlaceholderObjectBase
	{
		[SerializeField]
		private DetachedObject _target;

		protected override AssemblyObject Target
		{
			get { return _target; }
		}

		protected override void OnDeselectEventHandler(PointerEvent arg0)
		{
			if (_target.DetachedObjectState == DetachedObjectState.Hover)
			{
				CanBePlacedToPlaceholder = false;
			}
			else
			{
				CanBePlacedToPlaceholder = true;
			}
		}
	}
}