using System;
using UnityEngine;

namespace ComputerMaintenanceTraining.Configs
{
	[Serializable]
	public class TaskInfo
	{
		[SerializeField]
		private string _taskDescription;

		[SerializeField]
		private string[] _stepDescriptions;

		public string TaskDescription
		{
			get { return _taskDescription; }
		}

		public string[] StepDescriptions
		{
			get { return _stepDescriptions; }
		}
	}
}