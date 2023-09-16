using ComputerMaintenanceTraining.MotherboardComponents;
using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress.Conditions
{
	public class RAMPushedInPlace : Condition
	{
		[SerializeField]
		private RAMRotatedPivot _target;

		private void OnEnable()
		{
			SubscribeEvents();
		}

		private void OnDisable()
		{
			UnsubscribeEvents();
		}

		private void SubscribeEvents()
		{
			_target.OnPushedInPlace += OnRAMPushedInPlace;
			_target.OnPushedOutPlace += OnRAMPushedOutPlace;
		}

		private void UnsubscribeEvents()
		{
			_target.OnPushedInPlace -= OnRAMPushedInPlace;
			_target.OnPushedOutPlace -= OnRAMPushedOutPlace;
		}

		private void OnRAMPushedInPlace()
		{
			Satisfied = true;
		}

		private void OnRAMPushedOutPlace()
		{
			Satisfied = false;
		}
	}
}