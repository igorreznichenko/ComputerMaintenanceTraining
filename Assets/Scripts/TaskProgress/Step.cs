using ComputerMaintenanceTraining.TaskProgress.Conditions;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace ComputerMaintenanceTraining.TaskProgress
{
	public class Step : MonoBehaviour
	{
		[SerializeField]
		private Condition[] _stepConditions;

		public UnityEvent OnActivateStep;

		public UnityEvent OnDeactivateStep;

		public event Action OnComplete = default;

		public virtual void Activate()
		{
			SubscribeEvents();
			OnActivateStep?.Invoke();

			CheckTaskCompletion();

		}

		public virtual void Deactivate()
		{
			UnsubscribeEvents();
			OnDeactivateStep?.Invoke();
		}

		private void SubscribeEvents()
		{
			for (int i = 0; i < _stepConditions.Length; i++)
			{
				_stepConditions[i].OnSatisfyStateChanged += OnConditionSatisfyStateChangedEventHandler;
			}
		}

		private void UnsubscribeEvents()
		{
			for (int i = 0; i < _stepConditions.Length; i++)
			{
				_stepConditions[i].OnSatisfyStateChanged -= OnConditionSatisfyStateChangedEventHandler;
			}
		}

		private void OnConditionSatisfyStateChangedEventHandler(bool isSatisfied)
		{
			CheckTaskCompletion();
		}

		private void CheckTaskCompletion()
		{
			int i = 0;

			while (i < _stepConditions.Length && _stepConditions[i].Satisfied)
			{
				i++;
			}

			if (i != _stepConditions.Length)
			{
				return;
			}

			OnComplete?.Invoke();
		}
	}
}