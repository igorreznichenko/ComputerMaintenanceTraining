using ComputerMaintenanceTraining.Animations;
using System;
using UnityEngine;

namespace ComputerMaintenanceTraining.Interaction
{
	public class CPU : MonoBehaviour
	{
		[SerializeField]
		private CPUThermalPasteFillAnimation _thermalPasteAppearingAnimation;

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
					OnHasThermalPasteStatechanged?.Invoke(value);
				}
			}
		}

		public event Action<bool> OnHasThermalPasteStatechanged;

		public void ApplyThermalPaste()
		{
			_hasThermalPaste = true;
			_thermalPasteAppearingAnimation.Play();
		}
	}
}