using ComputerMaintenanceTraining.AssemblyObjects.Detachables;
using ComputerMaintenanceTraining.Extensions;
using System;
using UnityEngine;

namespace ComputerMaintenanceTraining.AssemblyObjects
{
	public class Screwable : MonoBehaviour
	{
		[SerializeField]
		private Transform _pivot;

		[SerializeField]
		public Transform _screwToolPlace;

		[SerializeField]
		private float _screwSpeed;

		private ScrewableDetachedObjectPlace _currentPlace;

		public event Action OnScrewedIn = default;
		public event Action OnScrewedOut = default;
		public event Action OnTransitedToScrew = default;

		public float ScrewSpeed
		{
			get { return _screwSpeed; }
		}

		public Transform Pivot
		{
			get { return _pivot; }
		}

		public Transform ScrewToolPlace
		{
			get
			{
				return _screwToolPlace;
			}
		}

		public bool IsScrewedIn
		{
			get { return _pivot.localPosition.y <= _currentPlace.ScrewIn.localPosition.y; }
		}

		public bool IsScrewedOut
		{
			get { return _pivot.localPosition.y >= _currentPlace.ScrewOut.localPosition.y; }
		}

		public void SetScrewableDetachedObjectPlace(ScrewableDetachedObjectPlace detachedObjectPlace)
		{
			_currentPlace = detachedObjectPlace;
		}

		public void HandleScrew(float screwValue)
		{
			bool isScrewedInBefore = IsScrewedIn;
			bool isScrewedOutBefore = IsScrewedOut;

			_pivot.SetLocalRotationForAxis(_pivot.localRotation.y + screwValue, Enums.Axis.Y);
			_pivot.position += _pivot.up * screwValue;

			if (IsScrewedIn && !isScrewedInBefore)
			{
				SetInScrewedInPosition();
				OnScrewedIn?.Invoke();
			}
			else if (IsScrewedOut && !isScrewedOutBefore)
			{
				SetInScrewedOutPosition();
				OnScrewedOut?.Invoke();
			}
			else if (isScrewedInBefore || isScrewedOutBefore)
			{
				OnTransitedToScrew?.Invoke();
			}
		}

		private void SetInScrewedInPosition()
		{
			_pivot.position = _currentPlace.ScrewIn.position;
		}

		private void SetInScrewedOutPosition()
		{
			_pivot.position = _currentPlace.ScrewOut.position;
		}
	}
}