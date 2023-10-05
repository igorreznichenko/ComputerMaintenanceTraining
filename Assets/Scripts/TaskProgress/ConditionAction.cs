using ComputerMaintenanceTraining.TaskProgress.Conditions;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace ComputerMaintenanceTraining.TaskProgress
{
	[Serializable]
	public class ConditionAction
	{
		[SerializeField]
		private Condition _condition;

		[SerializeField]
		private UnityEvent _onSatisfied;

		[SerializeField]
		private UnityEvent _onNotSatisfied;

		public Condition Condition
		{
			get { return _condition; }
		}

		public UnityEvent OnSatisfied
		{
			get { return _onSatisfied; }
		}

		public UnityEvent OnNotSatisfied
		{
			get { return _onNotSatisfied; }
		}

		public void SubscribeActionOnCondition()
		{
			Condition.OnSatisfyStateChanged += OnConditionStateChanged;
		}

		public void UnsubscribeActionOnCondition()
		{
			Condition.OnSatisfyStateChanged -= OnConditionStateChanged;
		}

		private void OnConditionStateChanged(bool isSatisfied)
		{
			if(isSatisfied)
			{
				_onSatisfied?.Invoke();
			}
			else
			{
				_onNotSatisfied?.Invoke();
			}
		}
	}
}