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
		private Transform _syringeHandle;

		[SerializeField]
		private Transform _syringeHandlePushIn;

		private bool _hasThermalPaste = true;

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