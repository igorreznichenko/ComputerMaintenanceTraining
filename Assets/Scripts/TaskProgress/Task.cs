using System;
using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress
{
	public class Task : MonoBehaviour
	{
		[SerializeField]
		private Step[] _steps;

		private int _currentStepIndex = 0;

		public event Action OnTaskCompleted = default;

		public void Activate()
		{
			ActivateCurrentask();
		}

		private void OnCurrentStepFinished()
		{
			_steps[_currentStepIndex].OnComplete -= OnCurrentStepFinished;
			_steps[_currentStepIndex].Deactivate();

			_currentStepIndex++;

			if (_currentStepIndex >= _steps.Length)
			{
				OnTaskCompleted?.Invoke();
			}
			else
			{
				ActivateCurrentask();
			}
		}

		private void ActivateCurrentask()
		{
			_steps[_currentStepIndex].OnComplete += OnCurrentStepFinished;
			_steps[_currentStepIndex].Activate();
		}
	}
}