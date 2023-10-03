using System;

namespace ComputerMaintenanceTraining.TaskProgress
{
	public interface ITasksController
	{
		public event Action OnComplete;

		public int TaskIndex { get; }

		public int StepIndex { get; }

		public event Action<int, int> OnProgressChanged;

		public void StartExecution();
	}
}