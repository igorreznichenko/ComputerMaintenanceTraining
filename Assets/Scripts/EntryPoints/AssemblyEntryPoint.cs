using ComputerMaintenanceTraining.TaskProgress;
using ComputerMaintenanceTraining.UI;
using ComputerMaintenanceTraining.UI.Monitor;
using ComputerMaintenanceTraining.VisualEffects;
using UnityEngine;

namespace ComputerMaintenanceTraining.EntryPoints
{
	public class AssemblyEntryPoint : MonoBehaviour
	{
		[SerializeField]
		private Fader _fader;

		[SerializeField]
		private TasksController _tasksController;

		[SerializeField]
		private MonitorUIController _monitorUIController;

		private void Awake()
		{
			_monitorUIController.Initialize();

			_fader.FadeOut(() =>
			{
				_monitorUIController.SwitchScreen<PrintTaskInfoScreen>();
				_tasksController.StartExecution();
			});
		}
	}
}