using Oculus.Interaction;
using System;
using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress.Conditions
{
	public class PointableSelectionCondition : Condition
	{
		[SerializeField]
		private PointableUnityEventWrapper _pointableUnityEventWrapper;

		private void OnEnable()
		{
			SubscribeEvents();
		}

		private void OnDisable()
		{
			UnsubscribeEvents();
		}

		private void UnsubscribeEvents()
		{
			_pointableUnityEventWrapper.WhenSelect.RemoveListener(OnSelectEventHandler);
			_pointableUnityEventWrapper.WhenUnselect.RemoveListener(OnUnselectEventHandler);
		}

		private void SubscribeEvents()
		{
			_pointableUnityEventWrapper.WhenSelect.AddListener(OnSelectEventHandler);
			_pointableUnityEventWrapper.WhenUnselect.AddListener(OnUnselectEventHandler);
		}

		private void OnSelectEventHandler(PointerEvent args)
		{
			Satisfied = true;
		}

		private void OnUnselectEventHandler(PointerEvent args)
		{
			Satisfied = false;
		}
	}
}