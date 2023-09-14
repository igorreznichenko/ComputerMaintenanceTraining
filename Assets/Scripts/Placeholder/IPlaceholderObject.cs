using ComputerMaintenanceTraining.AssemblyObjects;
using System;
using UnityEngine;

namespace ComputerMaintenanceTraining.PlaceholderLogic
{
	public interface IPlaceholderObject : IAssemblyObject
	{
		public Transform Pivot
		{
			get;
		}

		public bool CanBePlacedToPlaceholder
		{
			get;
		}

		public event Action<IPlaceholderObject> OnPlacableStateChanged;

		public event Action<PlaceholderPlace> OnPlaceholderPlaceChanged;

		public void OnPlaceholderPlaceChangedEventHandler(PlaceholderPlace placeholderPlace);
	}
}