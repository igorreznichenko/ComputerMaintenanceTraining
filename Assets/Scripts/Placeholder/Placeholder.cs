using ComputerMaintenanceTraining.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ComputerMaintenanceTraining.PlaceholderLogic
{
	public class Placeholder : MonoBehaviour
	{
		[SerializeField]
		private PlaceholderPlace[] _places;

		List<IPlaceholderObject> _candidates = new List<IPlaceholderObject>();

		private void OnTriggerEnter(Collider other)
		{
			if(other.TryGetComponent(out IPlaceholderObject placeholderObject))
			{
				if (placeholderObject.CanBePlacedToPlaceholder)
				{
					SetObjectToPlaceholder(placeholderObject);
				}
				else
				{
					_candidates.Add(placeholderObject);
					placeholderObject.OnPlacableStateChanged += OnPlacableStateChangedEventHandler;
				}
			}
		}
		private void OnTriggerExit(Collider other)
		{
			if(other.TryGetComponent(out IPlaceholderObject placeholderObject))
			{
				_candidates.Remove(placeholderObject);
				placeholderObject.OnPlacableStateChanged -= OnPlacableStateChangedEventHandler;
			}
		}

		private void OnPlacableStateChangedEventHandler(IPlaceholderObject placeholderObject)
		{
			if (placeholderObject.CanBePlacedToPlaceholder)
			{
				SetObjectToPlaceholder(placeholderObject);
				_candidates.Remove(placeholderObject);
			}
		}

		private void SetObjectToPlaceholder(IPlaceholderObject placeholderObject)
		{
			PlaceholderPlace place = GetAvailablePlace(placeholderObject.AssemblyObjectType);

			place.SetObject(placeholderObject);
		}


		private PlaceholderPlace GetAvailablePlace(AssemblyObjectType assemblyObjectType)
		{
			PlaceholderPlace result = _places.Where(x => x.Target == assemblyObjectType && !x.IsBusy).First();

			return result;
		}
	}
}