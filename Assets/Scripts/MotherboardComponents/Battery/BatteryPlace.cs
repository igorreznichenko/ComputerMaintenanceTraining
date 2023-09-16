using ComputerMaintenanceTraining.Enums;
using ComputerMaintenanceTraining.Extensions;
using Oculus.Interaction;
using System;
using UnityEngine;

namespace ComputerMaintenanceTraining.MotherboardComponents
{
	public class BatteryPlace : MonoBehaviour
	{
		[SerializeField]
		private Transform _pivot;

		[SerializeField]
		private PointableUnityEventWrapper _batteryKeeperUnityEventWrapper;

		private float _openDegree = -15;

		public event Action OnBatteryOutFromPlace;
		public event Action OnBatteryInPlace;

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
			BatteryInPlace();
		}

		public void BatteryOutPlace()
		{
			_pivot.SetLocalRotationForAxis(_openDegree, Axis.Z);
			OnBatteryOutFromPlace?.Invoke();
		}

		public void BatteryInPlace()
		{
			_pivot.SetLocalRotationForAxis(0, Axis.Z);
			OnBatteryInPlace?.Invoke();
		}
	}
}