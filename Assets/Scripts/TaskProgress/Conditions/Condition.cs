using System;
using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress.Conditions
{
	public class Condition : MonoBehaviour
	{
		private bool _satisfied = false;

		public bool Satisfied
		{
			get { return _satisfied; }

			protected set
			{
				if (_satisfied != value)
				{
					_satisfied = value;
					OnSatisfyStateChanged?.Invoke(value);
				}
			}
		}

		public event Action<bool> OnSatisfyStateChanged = default;
	}
}