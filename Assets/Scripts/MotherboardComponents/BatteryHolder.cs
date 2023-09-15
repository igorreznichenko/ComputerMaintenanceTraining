using UnityEngine;

namespace ComputerMaintenanceTraining.MotherboardComponents
{
	public class BatteryHolder : MonoBehaviour
	{
		[SerializeField]
		private BatteryKeeper _batteryKeeper;

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
			_batteryKeeper.OnBatteryHolderOpenned += OnBatteryHolderOpennedEventHandler;
			_batteryPlace.OnBatteryInPlace += OnBatterInPlaceEventHandler;
		}

		private void UnsubscribeEvents()
		{
			_batteryKeeper.OnBatteryHolderOpenned -= OnBatteryHolderOpennedEventHandler;
			_batteryPlace.OnBatteryInPlace -= OnBatterInPlaceEventHandler;
		}

		private void OnBatteryHolderOpennedEventHandler()
		{
			_batteryPlace.BatteryOutPlace();
		}

		private void OnBatterInPlaceEventHandler()
		{
			_batteryKeeper.Close();
		}
	}
}