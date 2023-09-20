using ComputerMaintenanceTraining.Enums;
using ComputerMaintenanceTraining.PlaceholderLogic;
using Oculus.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ComputerMaintenanceTraining.AssemblyObjects.Detachables
{
	public class DetachedObject : AssemblyObject, IPlaceholderObject
	{
		[SerializeField]
		private DetachedObjectPlace _current = null;

		[SerializeField]
		private PointableUnityEventWrapper _pointableUnityEventWrapper;

		private List<DetachedObjectPlace> _candidates = new List<DetachedObjectPlace>();

		private DetachedObjectState _detachedObjectState;

		private DetachedObjectState DetachedObjectState
		{
			get { return _detachedObjectState; }
			set
			{
				if (_detachedObjectState != value)
				{
					_detachedObjectState = value;
					OnDetachedObjectStateChanged?.Invoke(value);
				}
			}
		}

		private event Action<DetachedObjectState> OnDetachedObjectStateChanged = default;

		#region placeholder logic

		private bool _canBePlacedToPlaceholder = false;


		public bool CanBePlacedToPlaceholder
		{
			get { return _canBePlacedToPlaceholder; }
			set
			{
				if (_canBePlacedToPlaceholder != value)
				{
					_canBePlacedToPlaceholder = value;
					OnPlacableStateChanged?.Invoke(this);
				}
			}
		}

		public event Action<IPlaceholderObject> OnPlacableStateChanged;
		public event Action<PlaceholderPlace> OnPlaceholderPlaceChanged;

		#endregion

		private void OnEnable()
		{
			SubscribeEvents();
		}

		private void OnDisable()
		{
			UnsubscribeEvents();
		}

		protected virtual void Start()
		{
			if (_current != null)
			{
				SetToDetachedPlace(_current);
			}
		}

		private void SubscribeEvents()
		{
			_pointableUnityEventWrapper.WhenSelect.AddListener(OnSelectEventHandler);
			_pointableUnityEventWrapper.WhenUnselect.AddListener(OnDeselectEventHandler);
		}

		private void UnsubscribeEvents()
		{
			_pointableUnityEventWrapper.WhenSelect.RemoveListener(OnSelectEventHandler);
			_pointableUnityEventWrapper.WhenUnselect.RemoveListener(OnDeselectEventHandler);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out DetachedObjectPlace place))
			{
				if (place.TargetObject == AssemblyObjectType && place.CanAttachObject)
				{
					_candidates.Add(place);
					DetachedObjectState = DetachedObjectState.Hover;
				}
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent(out DetachedObjectPlace place))
			{
				if (place.TargetObject == AssemblyObjectType && place.CanAttachObject)
				{
					_candidates.Remove(place);

					if (_candidates.Count == 0)
					{
						DetachedObjectState = DetachedObjectState.Detached;
					}
				}
			}
		}

		private void OnDeselectEventHandler(PointerEvent pointerEvent)
		{

			if (DetachedObjectState == DetachedObjectState.Hover)
			{
				DetachedObjectPlace firstCandidate = _candidates.First();
				SetToDetachedPlace(firstCandidate);
				return;
			}

			CanBePlacedToPlaceholder = true;

		}

		private void OnSelectEventHandler(PointerEvent pointerEvent)
		{
			CanBePlacedToPlaceholder = false;

			if (DetachedObjectState == DetachedObjectState.Attached)
			{
				DetachedObjectState = DetachedObjectState.Hover;
				ReleaseCurrentPlace();
			}
		}

		public void OnPlaceholderPlaceChangedEventHandler(PlaceholderPlace placeholderPlace)
		{
			OnPlaceholderPlaceChanged?.Invoke(placeholderPlace);
		}

		protected virtual void SetToDetachedPlace(DetachedObjectPlace detachedObjectPlace)
		{
			_current = detachedObjectPlace;
			_current.SetDetachedObject(this);
			DetachedObjectState = DetachedObjectState.Attached;
		}

		protected virtual void ReleaseCurrentPlace()
		{
			_current.Release();
			_current = null;
		}
	}
}