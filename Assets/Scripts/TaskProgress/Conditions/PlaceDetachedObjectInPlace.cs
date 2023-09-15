using ComputerMaintenanceTraining.AssemblyObjects.Detachables;
using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress.Conditions
{
	public class PlaceDetachedObjectInPlace : Condition
	{
		[SerializeField]
		private DetachedObjectPlace _detachedObjectPlace;

		[SerializeField]
		private DetachedObject _target;

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
			_detachedObjectPlace.OnDetachedObjectChanged += OnDetachedObjectChangedEventHandler;
		}

		private void UnsubscribeEvents()
		{
			_detachedObjectPlace.OnDetachedObjectChanged -= OnDetachedObjectChangedEventHandler;
		}

		private void OnDetachedObjectChangedEventHandler(DetachedObject detachedObject)
		{
			if (detachedObject == _target && !Satisfied)
			{
				Satisfied = true;
			}
			else if (detachedObject != _target && Satisfied)
			{
				Satisfied = false;
			}
		}
	}
}