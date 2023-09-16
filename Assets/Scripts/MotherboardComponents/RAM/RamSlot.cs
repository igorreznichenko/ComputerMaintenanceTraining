using UnityEngine;

namespace ComputerMaintenanceTraining.MotherboardComponents
{
	public class RamSlot : MonoBehaviour
	{
		[SerializeField]
		private RAMKeeper _ramKeeper;

		[SerializeField]
		private RAMRotatedPivot _rotatedPivot;

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
			_ramKeeper.OnRAMKeeperOpened += OnRAMKeeperOpenedEventHandler;
			_rotatedPivot.OnPushedInPlace += OnRAMPushedInPlaceEventHandler;
		}

		private void UnsubscribeEvents()
		{
			_ramKeeper.OnRAMKeeperOpened -= OnRAMKeeperOpenedEventHandler;
			_rotatedPivot.OnPushedInPlace -= OnRAMPushedInPlaceEventHandler;
		}

		private void OnRAMKeeperOpenedEventHandler()
		{
			_rotatedPivot.PushOut();
		}

		private void OnRAMPushedInPlaceEventHandler()
		{
			_ramKeeper.Close();
		}
	}
}