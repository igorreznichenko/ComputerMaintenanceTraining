using ComputerMaintenanceTraining.Enums;
using ComputerMaintenanceTraining.Extensions;
using Oculus.Interaction;
using System;
using UnityEngine;

namespace ComputerMaintenanceTraining.MotherboardComponents
{
	public class RAMKeeper : MonoBehaviour
	{
		[SerializeField]
		private Transform _pivot;

		[SerializeField]
		private PointableUnityEventWrapper _pointableUnityEventWrapper;

		private float _openedXRotation = -19;

		public event Action OnRAMKeeperOpened = default;
		public event Action OnRAMKeeperClosed = default;

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
			_pointableUnityEventWrapper.WhenSelect.AddListener(OnSelectionEventHandler);
		}

		private void UnsubscribeEvents()
		{
			_pointableUnityEventWrapper.WhenSelect.RemoveListener(OnSelectionEventHandler);
		}

		private void OnSelectionEventHandler(PointerEvent pointerEvent)
		{
			Open();
		}

		public void Open()
		{
			_pivot.SetLocalRotationForAxis(_openedXRotation, Axis.X);
			OnRAMKeeperOpened?.Invoke();
		}

		public void Close()
		{
			_pivot.SetLocalRotationForAxis(0, Axis.X);
			OnRAMKeeperClosed?.Invoke();
		}
	}
}