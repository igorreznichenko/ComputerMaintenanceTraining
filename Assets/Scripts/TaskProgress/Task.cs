using System;
using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress
{
	public class Task : MonoBehaviour
	{
		[SerializeField]
		private Step[] _steps;

		private int _currentStepIndex = 0;

		public int StepIndex 
		{ 
			get { return _currentStepIndex; } 
		}

		public event Action OnTaskCompleted = default;
		public event Action<int> OnStepChanged = default;

		public void Activate()
		{
			ActivateCurrentStep();
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
				ActivateCurrentStep();
			}
		}

		private void ActivateCurrentStep()
		{
			_steps[_currentStepIndex].OnComplete += OnCurrentStepFinished;
			_steps[_currentStepIndex].Activate();

			OnStepChanged?.Invoke(_currentStepIndex);
		}
	}
}