using ComputerMaintenanceTraining.MotherboardComponents;
using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress.Conditions
{
	public class RAMKeeperOpened : Condition
	{
		[SerializeField]
		private RAMKeeper _target;

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
			_target.OnRAMKeeperOpened += OnRAMKeeperOpenedEventHandler;
			_target.OnRAMKeeperClosed += OnRAMKeeperClosedEventHandler;
		}

		private void UnsubscribeEvents()
		{
			_target.OnRAMKeeperOpened -= OnRAMKeeperOpenedEventHandler;
			_target.OnRAMKeeperClosed -= OnRAMKeeperClosedEventHandler;
		}

		private void OnRAMKeeperOpenedEventHandler()
		{
			Satisfied = true;
		}

		private void OnRAMKeeperClosedEventHandler()
		{
			Satisfied = false;
		}
	}
}