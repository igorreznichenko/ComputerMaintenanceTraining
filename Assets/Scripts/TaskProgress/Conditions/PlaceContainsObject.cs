using ComputerMaintenanceTraining.AssemblyObjects.Detachables;
using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress.Conditions
{
	public class PlaceContainsObject : Condition
	{
		[SerializeField]
		private DetachedObjectPlace _target;

		private void Start()
		{
			CheckSatisfaction();
		}

		private void OnEnable()
		{
			SubscribeConditions();
		}

		private void OnDisable()
		{
			UnsubscribeConditions();
		}

		private void SubscribeConditions()
		{
			_target.OnDetachedObjectChanged += OnDetachedObjectChangedEventHandler;
		}

		private void UnsubscribeConditions()
		{
			_target.OnDetachedObjectChanged -= OnDetachedObjectChangedEventHandler;
		}

		private void OnDetachedObjectChangedEventHandler(DetachedObject detachedObject)
		{
			CheckSatisfaction();
		}

		private void CheckSatisfaction()
		{
			if (_target.ContainsObject)
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