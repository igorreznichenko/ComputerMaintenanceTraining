using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress
{
	public class ConditionActionController : MonoBehaviour
	{
		[SerializeField]
		private ConditionAction[] _conditionActions;

		public void Activate()
		{
			SubscribeConditionActions();
		}

		public void Deactivate()
		{
			UnsubscribeConditionActions();
		}

		private void SubscribeConditionActions()
		{
			for (int i = 0; i < _conditionActions.Length; i++)
			{
				_conditionActions[i].SubscribeActionOnCondition();
			}
		}

		private void UnsubscribeConditionActions()
		{
			for (int i = 0; i < _conditionActions.Length; i++)
			{
				_conditionActions[i].UnsubscribeActionOnCondition();
			}
		}
	}
}