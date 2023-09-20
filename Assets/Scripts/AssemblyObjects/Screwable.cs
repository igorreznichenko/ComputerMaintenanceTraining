using ComputerMaintenanceTraining.AssemblyObjects.Detachables;
using ComputerMaintenanceTraining.Enums;
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

		[SerializeField]
		private bool _allowScrewable;

		public bool AllowScrewable
		{
			get
			{
				return _allowScrewable;
			}

			set
			{
				_allowScrewable = value;
			}
		}

		private ScrewableDetachedObjectPlace _currentPlace;

		public event Action OnScrewedIn = default;
		public event Action OnScrewedOut = default;
		public event Action OnTransitedToScrew = default;

		public bool IsScrewable
		{
			get { return _currentPlace != null && _allowScrewable; }
		}

		public float ScrewSpeed
		{
			get { return _screwSpeed; }
		}

		public Transform Pivot
		{
			get { return _pivot; }
		}

		public Transform PlacePivot
		{
			get { return _currentPlace.Pivot; }
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

			_pivot.SetLocalRotationForAxis(_pivot.localRotation.y + screwValue, Axis.Y);
			_pivot.position += _pivot.up * screwValue * _screwSpeed * Time.deltaTime;

			float yClamp = Mathf.Clamp(_pivot.localPosition.y, _currentPlace.ScrewIn.localPosition.y, _currentPlace.ScrewOut.localPosition.y);

			_pivot.SetLocalPositionForAxis(yClamp, Axis.Y);

			if (IsScrewedIn && !isScrewedInBefore)
			{
				OnScrewedIn?.Invoke();
			}
			else if (IsScrewedOut && !isScrewedOutBefore)
			{
				OnScrewedOut?.Invoke();
			}
			else if (isScrewedInBefore || isScrewedOutBefore)
			{
				OnTransitedToScrew?.Invoke();
			}
		}
	}
}