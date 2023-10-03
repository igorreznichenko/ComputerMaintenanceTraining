using System;
using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress
{
	public class TasksController : MonoBehaviour, ITasksController
	{
		[SerializeField]
		private Task[] _tasks;

		private int _currentTaskIndex = 0;

		public int TaskIndex
		{
			get { return _currentTaskIndex; }
		}

		public int StepIndex
		{
			get { return _tasks[_currentTaskIndex].StepIndex; }
		}

		public event Action OnComplete = default;

		public event Action<int, int> OnProgressChanged = default;

		public void StartExecution()
		{
			ActivateCurrentTask();
		}

		private void ActivateCurrentTask()
		{
			_tasks[_currentTaskIndex].OnTaskCompleted += OnCurrentTaskCompletedEventHandler;
			_tasks[_currentTaskIndex].OnStepChanged += OnStepOfCurrentTaskChanged;
			_tasks[_currentTaskIndex].Activate();
		}

		private void OnCurrentTaskCompletedEventHandler()
		{
			_tasks[_currentTaskIndex].OnTaskCompleted -= OnCurrentTaskCompletedEventHandler;
			_tasks[_currentTaskIndex].OnStepChanged -= OnStepOfCurrentTaskChanged;

			_currentTaskIndex++;

			if (_currentTaskIndex >= _tasks.Length)
			{
				OnComplete?.Invoke();
			}
			else
			{
				ActivateCurrentTask();
			}
		}

		private void OnStepOfCurrentTaskChanged(int stepIndex)
		{
			OnProgressChanged?.Invoke(_currentTaskIndex, stepIndex);
		}
	}
}