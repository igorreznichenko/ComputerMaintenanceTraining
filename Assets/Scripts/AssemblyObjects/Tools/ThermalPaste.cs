using ComputerMaintenanceTraining.Collisions;
using ComputerMaintenanceTraining.Interaction;
using ComputerMaintenanceTraining.PlaceholderLogic;
using Oculus.Interaction;
using System;
using UnityEngine;

namespace ComputerMaintenanceTraining.AssemblyObjects
{
	public class ThermalPaste : AssemblyObject, IPlaceholderObject, ITriggerable
	{
		[SerializeField]
		private PointableUnityEventWrapper _pointableUnityEventWrapper;

		[SerializeField]
		private Transform _syringeHandle;

		[SerializeField]
		private Transform _syringeHandlePushIn;

		private bool _canBePlacedToPlaceholder = false;

		public bool CanBePlacedToPlaceholder
		{
			get { return _canBePlacedToPlaceholder; }

			private set
			{
				if (_canBePlacedToPlaceholder != value)
				{
					_canBePlacedToPlaceholder = value;
					OnPlacableStateChanged?.Invoke(this);
				}
			}
		}

		private bool _hasThermalPaste = true;

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
			_pointableUnityEventWrapper.WhenUnselect.AddListener(OnUnselectEventHandler);
		}

		private void UnsubscribeEvents()
		{
			_pointableUnityEventWrapper.WhenSelect.AddListener(OnSelectEventHandler);
			_pointableUnityEventWrapper.WhenUnselect.AddListener(OnUnselectEventHandler);
		}

		private void OnSelectEventHandler(PointerEvent pointerEvent)
		{
			CanBePlacedToPlaceholder = false;
		}

		private void OnUnselectEventHandler(PointerEvent pointerEvent)
		{
			CanBePlacedToPlaceholder = true;
		}

		public void OnPlaceholderPlaceChangedEventHandler(PlaceholderPlace placeholderPlace)
		{
			OnPlaceholderPlaceChanged?.Invoke(placeholderPlace);
		}

		public void TriggerEnter(Collider other)
		{
			if(other.TryGetComponent(out CPU cpu) && !cpu.HasThermalPaste && _hasThermalPaste)
			{
				UseThermalPaste(cpu);
			}
		}

		private void UseThermalPaste(CPU cpu)
		{
			_hasThermalPaste = false;
			_syringeHandle.position = _syringeHandlePushIn.position;
			cpu.ApplyThermalPaste();
		}

		public void TriggerExit(Collider other) { }
	}
}