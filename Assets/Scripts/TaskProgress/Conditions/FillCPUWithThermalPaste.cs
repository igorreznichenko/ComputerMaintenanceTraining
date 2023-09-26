using ComputerMaintenanceTraining.Interaction;
using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress.Conditions
{
	public class FillCPUWithThermalPaste : Condition
	{
		[SerializeField]
		private CPU _target;

		private void OnEnable()
		{
			SubscribeEvents();
		}

		private void OnDisable()
		{
			UnsubscribeEvents();
		}

		private void Start()
		{
			Satisfied = _target.HasThermalPaste;
		}

		private void SubscribeEvents()
		{
			_target.OnHasThermalPasteStateChanged += OnCPUHasThermalPasteStateChangedEventHandler;
		}

		private void UnsubscribeEvents()
		{
			_target.OnHasThermalPasteStateChanged -= OnCPUHasThermalPasteStateChangedEventHandler;
		}

		private void OnCPUHasThermalPasteStateChangedEventHandler(bool hasThermalPaste)
		{
			Satisfied = hasThermalPaste;
		}
	}
}