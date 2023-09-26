using ComputerMaintenanceTraining.AssemblyObjects.Detachables;
using ComputerMaintenanceTraining.Enums;
using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress.Conditions
{
	public class AttachDetachedObject : Condition
	{
		[SerializeField]
		private DetachedObject _detachedObject;

		private void Start()
		{
			CheckCondition(_detachedObject.DetachedObjectState);
		}

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
			_detachedObject.OnDetachedObjectStateChanged += OnDetachedObjectStateChangedEventHandler;
		}

		private void UnsubscribeEvents()
		{
			_detachedObject.OnDetachedObjectStateChanged -= OnDetachedObjectStateChangedEventHandler;
		}

		private void OnDetachedObjectStateChangedEventHandler(DetachedObjectState detachedObjectState)
		{
			CheckCondition(detachedObjectState);
		}

		private void CheckCondition(DetachedObjectState detachedObjectState)
		{
			if (detachedObjectState == DetachedObjectState.Attached)
			{
				Satisfied = true;
			}
			else
			{
				Satisfied = false;
			}
		}
	}
}