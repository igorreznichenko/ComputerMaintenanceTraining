using ComputerMaintenanceTraining.TaskProgress.Conditions;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace ComputerMaintenanceTraining.TaskProgress.ConditionReaction
{
	[Serializable]
	public class ConditionReaction
	{
		[SerializeField]
		private Condition _condition;

		[SerializeField]
		private UnityEvent _reaction;

		private bool _isActive = false;

		public void Activate()
		{
			if (!_isActive)
			{
				_isActive = true;
				SubscribeConditionToReaction();
				DoOnSatisfyAction(_condition.Satisfied);
			}
		}

		public void Deactivate()
		{
			if (_isActive)
			{
				_isActive = false;
				UnsubscribeConditionFromReaction();
			}
		}

		private void SubscribeConditionToReaction()
		{
			_condition.OnSatisfyStateChanged += OnConditionStateChanged;
		}

		private void UnsubscribeConditionFromReaction()
		{
			_condition.OnSatisfyStateChanged -= OnConditionStateChanged;
		}

		private void OnConditionStateChanged(bool isSatisfied)
		{
			DoOnSatisfyAction(isSatisfied);
		}

		public void DoOnSatisfyAction(bool isSatisfied)
		{
			if (isSatisfied)
			{
				_reaction.Invoke();
			}
		}
	}
}