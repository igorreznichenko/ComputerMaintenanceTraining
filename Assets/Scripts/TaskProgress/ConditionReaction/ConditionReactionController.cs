using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress.ConditionReaction
{
	public class ConditionReactionController : MonoBehaviour
	{
		[SerializeField]
		private ConditionReaction[] _conditionReactions;

		public void Activate()
		{
			for (int i = 0; i < _conditionReactions.Length; i++)
			{
				_conditionReactions[i].Activate();
			}
		}

		public void Deactivate()
		{
			for (int i = 0; i < _conditionReactions.Length; i++)
			{
				_conditionReactions[i].Deactivate();
			}
		}
	}
}