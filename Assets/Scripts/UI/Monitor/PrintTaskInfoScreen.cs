using ComputerMaintenanceTraining.Configs;
using ComputerMaintenanceTraining.Extensions;
using ComputerMaintenanceTraining.TaskProgress;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ComputerMaintenanceTraining.UI.Monitor
{
	public class PrintTaskInfoScreen : MonitorScreen
	{
		[SerializeField]
		private GameObject _tasksControllerObject;

		[SerializeField]
		private TaskDescription _taskDescription;

		[SerializeField]
		private TMP_Text _outTaskText;

		[SerializeField]
		private TMP_Text _outDescriptionText;

		[SerializeField]
		private float _printSymbolTime;

		private Coroutine _printCoroutine = null;

		private ITasksController _tasksController;

		private void Awake()
		{
			_tasksController = _tasksControllerObject.GetComponent<ITasksController>();
		}

		private void OnEnable()
		{
			SubscribeEvents();
			RefreshDescription();
		}

		private void OnDisable()
		{
			UnsubscribeEvents();
		}

		private void SubscribeEvents()
		{
			_tasksController.OnProgressChanged += OnProgressChangedEventHandler;
			_tasksController.OnComplete += OnTasksCompleteEventHandler;
		}

		private void UnsubscribeEvents()
		{
			_tasksController.OnProgressChanged -= OnProgressChangedEventHandler;
			_tasksController.OnComplete -= OnTasksCompleteEventHandler;
		}

		private void RefreshDescription()
		{
			int taskDescriptionIndex = _tasksController.TaskIndex;
			if (taskDescriptionIndex >= 0)
			{
				int stepDescriptionIndex = _tasksController.StepIndex;

				if (stepDescriptionIndex >= 0)
				{
					PrintTaskAndStepDescriptionAtIndex(taskDescriptionIndex, stepDescriptionIndex);
				}
			}
		}

		private void OnProgressChangedEventHandler(int taskIndex, int stepIndex)
		{
			PrintTaskAndStepDescriptionAtIndex(taskIndex, stepIndex);
		}

		private void OnTasksCompleteEventHandler()
		{
			_monitorUIController.SwitchScreen<FinishScreen>();
		}

		private void PrintTaskAndStepDescriptionAtIndex(int taskIndex, int stepIndex)
		{
			this.KillCoroutine(ref _printCoroutine);

			string taskDescription = _taskDescription.GetTaskInfo(taskIndex);
			string stepDescription = _taskDescription.GetStepInfo(taskIndex, stepIndex);

			_printCoroutine = StartCoroutine(PrintTaskAndStepCoroutine(taskDescription, stepDescription));
		}

		private IEnumerator PrintTaskAndStepCoroutine(string taskDescription, string stepDesciption)
		{
			if (_outTaskText.text != taskDescription)
			{
				yield return _outTaskText.PrintTextBySymbolsCoroutine(taskDescription, _printSymbolTime);
			}

			if (_outDescriptionText.text != stepDesciption)
			{
				yield return _outDescriptionText.PrintTextBySymbolsCoroutine(stepDesciption, _printSymbolTime);
			}
		}
	}
}