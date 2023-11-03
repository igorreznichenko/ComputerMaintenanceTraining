using ComputerMaintenanceTraining.AssemblyObjects;
using ComputerMaintenanceTraining.Enums;
using Oculus.Interaction;
using System;
using UnityEngine;

namespace ComputerMaintenanceTraining.PlaceholderLogic
{
	public class PlaceholderObject : MonoBehaviour, IPlaceholderObject
	{
		[SerializeField]
		private AssemblyObject _target;

		[SerializeField]
		private PointableUnityEventWrapper _pointableUnityEventWrapper;

		public AssemblyObjectType AssemblyObjectType
		{
			get { return _target.AssemblyObjectType; }
		}

		public Transform Pivot
		{
			get { return _target.Pivot; }
		}

		private bool _canBePlacedToPlaceholder = false;

		public bool CanBePlacedToPlaceholder
		{
			get { return _canBePlacedToPlaceholder; }
			set
			{
				if (_canBePlacedToPlaceholder != value)
				{
					_canBePlacedToPlaceholder = value;
					OnPlacableStateChanged?.Invoke(this);
				}
			}
		}

		public event Action<IPlaceholderObject> OnPlacableStateChanged;
		public event Action<PlaceholderPlace> OnPlaceholderPlaceChanged;

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
			_pointableUnityEventWrapper.WhenSelect.AddListener(OnSelectEventHandler);
			_pointableUnityEventWrapper.WhenUnselect.AddListener(OnDeselectEventHandler);
		}

		private void UnsubscribeEvents()
		{
			_pointableUnityEventWrapper.WhenSelect.RemoveListener(OnSelectEventHandler);
			_pointableUnityEventWrapper.WhenUnselect.RemoveListener(OnDeselectEventHandler);
		}

		private void OnDeselectEventHandler(PointerEvent arg0)
		{
			CanBePlacedToPlaceholder = true;
		}

		private void OnSelectEventHandler(PointerEvent arg0)
		{
			CanBePlacedToPlaceholder = false;
		}

		public void OnPlaceholderPlaceChangedEventHandler(PlaceholderPlace placeholderPlace)
		{
			OnPlaceholderPlaceChanged?.Invoke(placeholderPlace);
		}
	}
}