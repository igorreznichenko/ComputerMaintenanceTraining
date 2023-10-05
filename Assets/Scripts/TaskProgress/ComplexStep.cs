using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress
{
	public class ComplexStep : Step
	{
		[SerializeField]
		private ConditionActionController _conditionActionController;

		public override void Activate()
		{
			base.Activate();
			_conditionActionController.Activate();
		}

		public override void Deactivate()
		{
			base.Deactivate();
			_conditionActionController?.Deactivate();
		}
	}
}