using ComputerMaintenanceTraining.MotherboardComponents;
using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress.Conditions
{
	public class OpenBatteryKeeper : Condition
	{
		[SerializeField]
		private BatteryKeeper _batteryKeeper;

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
			_batteryKeeper.OnBatteryHolderOpenned += OnBatteryKeeperOpenedEventHandler;
			_batteryKeeper.OnBatteryHolderClosed += OnBatteryKeeperClosedEventHandler;
		}

		private void UnsubscribeEvents()
		{
			_batteryKeeper.OnBatteryHolderOpenned -= OnBatteryKeeperOpenedEventHandler;
			_batteryKeeper.OnBatteryHolderClosed -= OnBatteryKeeperClosedEventHandler;
		}

		private void OnBatteryKeeperOpenedEventHandler()
		{
			Satisfied = true;
		}

		private void OnBatteryKeeperClosedEventHandler()
		{
			Satisfied = false;
		}
	}
}