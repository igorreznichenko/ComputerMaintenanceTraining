using Oculus.Interaction;
using System;
using UnityEngine;
using ComputerMaintenanceTraining.Extensions;

namespace ComputerMaintenanceTraining.MotherboardComponents
{
	public class BatteryKeeper : MonoBehaviour
	{
		[SerializeField]
		private Transform _pivot;

		[SerializeField]
		private PointableUnityEventWrapper _batteryKeeperUnityEventWrapper;

		private float _openDegree = 20;

		public event Action OnBatteryHolderOpenned;
		public event Action OnBatteryHolderClosed;

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
			_batteryKeeperUnityEventWrapper.WhenSelect.AddListener(OnSelectEventHandler);
		}

		private void UnsubscribeEvents()
		{
			_batteryKeeperUnityEventWrapper.WhenSelect.RemoveListener(OnSelectEventHandler);
		}

		private void OnSelectEventHandler(PointerEvent arg)
		{
			Open();
		}

		public void Open()
		{
			_pivot.SetLocalZRotation(_openDegree);
			OnBatteryHolderOpenned?.Invoke();
		}

		public void Close()
		{
			_pivot.SetLocalZRotation(0);
			OnBatteryHolderClosed?.Invoke();
		}
	}
}