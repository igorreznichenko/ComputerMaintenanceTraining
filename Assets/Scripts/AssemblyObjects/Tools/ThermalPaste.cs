using ComputerMaintenanceTraining.Collisions;
using ComputerMaintenanceTraining.Interaction;
using ComputerMaintenanceTraining.PlaceholderLogic;
using Oculus.Interaction;
using System;
using UnityEngine;

namespace ComputerMaintenanceTraining.AssemblyObjects
{
	public class ThermalPaste : AssemblyObject, ITriggerEnterHandler
	{
		[SerializeField]
		private PointableUnityEventWrapper _pointableUnityEventWrapper;

		[SerializeField]
		private Transform _syringeHandle;

		[SerializeField]
		private Transform _syringeHandlePushIn;

		private bool _hasThermalPaste = true;

		public event Action<IPlaceholderObject> OnPlacableStateChanged;
		public event Action<PlaceholderPlace> OnPlaceholderPlaceChanged;

		public void OnPlaceholderPlaceChangedEventHandler(PlaceholderPlace placeholderPlace)
		{
			OnPlaceholderPlaceChanged?.Invoke(placeholderPlace);
		}

		public void TriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out CPU cpu) && !cpu.HasThermalPaste && _hasThermalPaste)
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
	}
}