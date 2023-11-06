using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress.ConditionReaction
{
	public class StepWithConditionReaction : Step
	{
		[SerializeField]
		private ConditionReactionController _conditionReactionController;

		public override void Activate()
		{
			base.Activate();

			_conditionReactionController.Deactivate();
		}

		public override void Deactivate()
		{
			base.Deactivate();

			_conditionReactionController.Activate();
		}
	}
}