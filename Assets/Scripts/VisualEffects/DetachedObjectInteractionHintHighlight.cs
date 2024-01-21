using ComputerMaintenanceTraining.AssemblyObjects.Detachables;
using UnityEngine;

namespace ComputerMaintenanceTraining.VisualEffects
{
	public class DetachedObjectInteractionHintHighlight : InteractionHintHighlight
	{
		[SerializeField]
		private DetachedObject _target;

		protected override void Deactivate()
		{
			if (_target.DetachedObjectState != Enums.DetachedObjectState.Hover)
			{
				base.Deactivate();
			}
			else
			{
				IsActive = false;
			}
		}
	}
}