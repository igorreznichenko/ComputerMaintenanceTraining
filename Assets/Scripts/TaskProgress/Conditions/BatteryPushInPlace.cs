using ComputerMaintenanceTraining.MotherboardComponents;
using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress.Conditions
{
	public class BatteryPushInPlace : Condition
	{
		[SerializeField]
		private BatteryPlace _batteryPlace;

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
			_batteryPlace.OnBatteryInPlace += OnBatteryInPlace;
			_batteryPlace.OnBatteryOutFromPlace += OnBatteryOutFromPlace;
		}

		private void UnsubscribeEvents()
		{
			_batteryPlace.OnBatteryInPlace -= OnBatteryInPlace;
			_batteryPlace.OnBatteryOutFromPlace -= OnBatteryOutFromPlace;
		}

		private void OnBatteryInPlace()
		{
			Satisfied = true;
		}

		private void OnBatteryOutFromPlace()
		{
			Satisfied = false;
		}
	}
}