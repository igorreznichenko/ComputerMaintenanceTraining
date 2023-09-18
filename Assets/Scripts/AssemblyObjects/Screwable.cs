using ComputerMaintenanceTraining.Extensions;
using System;
using UnityEngine;

namespace ComputerMaintenanceTraining.AssemblyObjects
{
	public class Screwable : AssemblyObject
	{
		[SerializeField]
		private Transform _screwIn;

		[SerializeField]
		private Transform _screwOut;

		[SerializeField]
		public Transform _screwToolPlace;

		[SerializeField]
		private float _screwSpeed;

		[SerializeField]
		private Transform _modelPivot;

		public event Action OnScrewedIn = default;
		public event Action OnScrewedOut = default;
		public event Action OnTransitedToScrew = default;

		public float ScrewSpeed
		{
			get { return _screwSpeed; }
		}

		public Transform ScrewToolPlace
		{
			get
			{
				return _screwToolPlace;
			}
		}

		public Transform ModelPivot
		{
			get
			{
				return _modelPivot;
			}
		}

		public bool IsScrewedIn
		{
			get { return _modelPivot.localPosition.y <= _screwIn.localPosition.y; }
		}

		public bool IsScrewedOut
		{
			get { return _modelPivot.localPosition.y >= _screwOut.localPosition.y; }
		}

		public void HandleScrew(float screwValue)
		{
			bool isScrewedInBefore = IsScrewedIn;
			bool isScrewedOutBefore = IsScrewedOut;

			_modelPivot.SetLocalRotationForAxis(_modelPivot.localRotation.y + screwValue, Enums.Axis.Y);
			_modelPivot.position += _modelPivot.up * screwValue;

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