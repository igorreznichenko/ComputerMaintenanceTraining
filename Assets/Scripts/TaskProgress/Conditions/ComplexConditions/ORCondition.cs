using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress.Conditions
{
	public class ORCondition : Condition
	{
		[SerializeField]
		private Condition[] _conditions;

		private void OnEnable()
		{
			SubscribeEvents();
		}

		private void OnDisable()
		{
			UnsubscribeEvents();
		}

		private void SubscribeEvents()
		{
			for (int i = 0; i < _conditions.Length; i++)
			{
				_conditions[i].OnSatisfyStateChanged += OnConditionSatisfyStateChanged;
			}
		}

		private void UnsubscribeEvents()
		{
			for (int i = 0; i < _conditions.Length; i++)
			{
				_conditions[i].OnSatisfyStateChanged -= OnConditionSatisfyStateChanged;
			}
		}

		private void OnConditionSatisfyStateChanged(bool isSatisfied)
		{
			CheckConditions();
		}

		private void CheckConditions()
		{
			for (int i = 0; i < _conditions.Length; i++)
			{
				if (_conditions[i].Satisfied)
				{
					Satisfied = true;

					return;
				}
			}

			Satisfied = false;
		}
	}
}