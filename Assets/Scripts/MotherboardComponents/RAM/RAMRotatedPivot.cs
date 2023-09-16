using ComputerMaintenanceTraining.Enums;
using ComputerMaintenanceTraining.Extensions;
using Oculus.Interaction;
using System;
using UnityEngine;

namespace ComputerMaintenanceTraining.MotherboardComponents
{
	public class RAMRotatedPivot : MonoBehaviour
	{
		[SerializeField]
		private Transform _pivot;

		[SerializeField]
		private PointableUnityEventWrapper _pointableUnityEventWraper;

		public event Action OnPushedInPlace;
		public event Action OnPushedOutPlace;

		private float _pushedOutXRotation = 1;

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
			_pointableUnityEventWraper.WhenSelect.AddListener(OnSelectionEventHandler);
		}

		private void UnsubscribeEvents()
		{

		}

		private void OnSelectionEventHandler(PointerEvent pointerEvent)
		{
			PushIn();
		}

		public void PushIn()
		{
			_pivot.SetLocalRotationForAxis(0, Axis.X);
			OnPushedInPlace?.Invoke();
		}

		public void PushOut()
		{
			_pivot.SetLocalRotationForAxis(_pushedOutXRotation, Axis.X);
			OnPushedOutPlace?.Invoke();
		}
	}
}