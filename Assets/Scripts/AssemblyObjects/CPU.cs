using ComputerMaintenanceTraining.Animations;
using System;
using UnityEngine;

namespace ComputerMaintenanceTraining.Interaction
{
	public class CPU : MonoBehaviour
	{
		[SerializeField]
		private CPUThermalPasteFillAnimation _cpuThermalPasteFillAnimation;

		private bool _hasThermalPaste = false;

		public bool HasThermalPaste
		{
			get
			{
				return _hasThermalPaste;

			}

			set
			{
				if (_hasThermalPaste != value)
				{
					_hasThermalPaste = value;
					OnHasThermalPasteStateChanged?.Invoke(value);
				}
			}
		}

		public event Action<bool> OnHasThermalPasteStateChanged;

		public void ApplyThermalPaste()
		{
			HasThermalPaste = true;
			_cpuThermalPasteFillAnimation.Play();
		}
	}
}