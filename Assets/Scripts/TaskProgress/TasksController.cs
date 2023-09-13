using System;
using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress
{
	public class TasksController : MonoBehaviour
	{
		[SerializeField]
		private Task[] _tasks;

		private int _currentTaskIndex = 0;

		public event Action OnComplete;

		public void StartExecution()
		{
			ActivateCurrentTask();
		}

		private void ActivateCurrentTask()
		{
			_tasks[_currentTaskIndex].OnTaskCompleted += OnCurrentTaskCompletedEventHandler;
			_tasks[_currentTaskIndex].Activate();

		}

		private void OnCurrentTaskCompletedEventHandler()
		{
			_tasks[_currentTaskIndex].OnTaskCompleted -= OnCurrentTaskCompletedEventHandler;

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
	}
}