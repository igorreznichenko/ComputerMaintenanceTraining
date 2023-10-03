using UnityEngine;

namespace ComputerMaintenanceTraining.Configs
{
	[CreateAssetMenu(fileName = "TasksDescription", menuName = "Custom/TasksDescription", order = 0)]
	public class TaskDescription : ScriptableObject
	{
		[SerializeField]
		private TaskInfo[] _taskInfo;

		public TaskInfo[] TaskInfo
		{
			get { return _taskInfo; }
		}

		public string GetTaskInfo(int task)
		{
			return _taskInfo[task].TaskDescription;
		}

		public string GetStepInfo(int task, int step)
		{
			return _taskInfo[task].StepDescriptions[step];
		}
	}
}