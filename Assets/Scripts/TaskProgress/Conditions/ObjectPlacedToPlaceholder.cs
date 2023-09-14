using ComputerMaintenanceTraining.AssemblyObjects.Detachables;
using ComputerMaintenanceTraining.PlaceholderLogic;
using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress.Conditions
{
	public class ObjectPlacedToPlaceholder : Condition
	{
		[SerializeField]
		private GameObject _placeholderObject;

		private IPlaceholderObject _target;

		private void Awake()
		{
			_target = _placeholderObject.GetComponent<IPlaceholderObject>();
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
			_target.OnPlaceholderPlaceChanged += OnPlaceholderPlaceChangedEventHandler;
		}

		private void UnsubscribeEvents()
		{
			_target.OnPlaceholderPlaceChanged -= OnPlaceholderPlaceChangedEventHandler;
		}

		private void OnPlaceholderPlaceChangedEventHandler(PlaceholderPlace place)
		{
			if (place != null)
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