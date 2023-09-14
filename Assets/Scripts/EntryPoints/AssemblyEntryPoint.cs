using ComputerMaintenanceTraining.TaskProgress;
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

		private void Awake()
		{
			_fader.FadeOut(() =>
			{
				_tasksController.StartExecution();
			});
		}
	}
}