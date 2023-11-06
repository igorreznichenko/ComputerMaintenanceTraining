using ComputerMaintenanceTraining.Enums;
using ComputerMaintenanceTraining.Initialization;
using Oculus.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ComputerMaintenanceTraining.AssemblyObjects.Detachables
{
	public class DetachedObject : AssemblyObject, IInitializable
	{
		[SerializeField]
		private DetachedObjectPlace _current = null;

		[SerializeField]
		private PointableUnityEventWrapper _pointableUnityEventWrapper;

		private HashSet<DetachedObjectPlace> _candidates = new HashSet<DetachedObjectPlace>();

		private DetachedObjectPlace _bestCandidate = null;

		private DetachedObjectState _detachedObjectState;

		public DetachedObjectState DetachedObjectState
		{
			get { return _detachedObjectState; }
			private set
			{
				if (_detachedObjectState != value)
				{
					_detachedObjectState = value;
					OnDetachedObjectStateChanged?.Invoke(value);
				}
			}
		}

		public event Action<DetachedObjectState> OnDetachedObjectStateChanged = default;

		private void OnEnable()
		{
			SubscribeEvents();
		}

		private void OnDisable()
		{
			UnsubscribeEvents();
		}

		public virtual void Initialize()
		{
			if (_current != null)
			{
				_candidates.Add(_current);
				_bestCandidate = _current;
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
				if (place.TargetObject == AssemblyObjectType)
				{
					_candidates.Add(place);
					ProcessState();
				}
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent(out DetachedObjectPlace place))
			{
				if (place.TargetObject == AssemblyObjectType)
				{
					_candidates.Remove(place);
					ProcessState();
				}
			}
		}

		private void Update()
		{
			ProcessState();
		}

		private void ProcessState()
		{
			ProcessBestCandidate();

			switch ((DetachedObjectState)
)
			{
				case DetachedObjectState.Detached:
					ProcessDetachedState();
					break;
				case DetachedObjectState.Hover:
					ProcessHoverState();
					break;
				case DetachedObjectState.Attached:
					ProcessAttachState();
					break;
			}
		}

		private void ProcessDetachedState()
		{
			if (_current != null)
			{
				DetachedObjectState = DetachedObjectState.Attached;
			}
			else if (_bestCandidate != null)
			{
				DetachedObjectState = DetachedObjectState.Hover;
			}
		}

		private void ProcessHoverState()
		{
			if (_bestCandidate == null)
			{
				DetachedObjectState = DetachedObjectState.Detached;
			}
			else if (_current != null)
			{
				DetachedObjectState = DetachedObjectState.Attached;
			}
		}

		private void ProcessAttachState()
		{
			if (_current == null)
			{
				if (_bestCandidate != null)
				{
					DetachedObjectState = DetachedObjectState.Hover;
				}
				else
				{
					DetachedObjectState = DetachedObjectState.Detached;
				}
			}
		}

		private void ProcessBestCandidate()
		{
			if (_current != null)
			{
				_bestCandidate = _current;
			}
			else
			{
				if (_candidates.Count > 0)
				{
					_bestCandidate = _candidates.FirstOrDefault(x => x.CanAttachObject);
				}
				else
				{
					_bestCandidate = null;
				}
			}
		}

		private void OnDeselectEventHandler(PointerEvent pointerEvent)
		{

			if (DetachedObjectState == DetachedObjectState.Hover)
			{
				SetToDetachedPlace(_bestCandidate);
				return;
			}
		}

		private void OnSelectEventHandler(PointerEvent pointerEvent)
		{
			if (DetachedObjectState == DetachedObjectState.Attached)
			{
				ReleaseCurrentPlace();
			}
		}

		protected virtual void SetToDetachedPlace(DetachedObjectPlace detachedObjectPlace)
		{
			_current = detachedObjectPlace;
			_current.SetDetachedObject(this);
			ProcessState();
		}

		protected virtual void ReleaseCurrentPlace()
		{
			_current.Release();
			_current = null;
			ProcessState();
		}
	}
}