using ComputerMaintenanceTraining.Enums;
using Oculus.Interaction;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ComputerMaintenanceTraining.AssemblyObjects.Detachables
{
	public class DetachedObject : AssemblyObject
	{
		[SerializeField]
		private DetachedObjectPlace _current = null;

		[SerializeField]
		private PointableUnityEventWrapper _pointableUnityEventWrapper;

		private List<DetachedObjectPlace> _candidates = new List<DetachedObjectPlace>();

		private DetachedObjectState _detachedObjectState;

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
					_detachedObjectState = DetachedObjectState.Hover;
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

					if (_candidates.Count == 0)
					{
						_detachedObjectState = DetachedObjectState.Detached;
					}
				}
			}
		}

		private void OnDeselectEventHandler(PointerEvent pointerEvent)
		{
			if (_detachedObjectState == DetachedObjectState.Hover)
			{
				_current = _candidates.First();
				_current.SetDetachedObject(this);
				_detachedObjectState = DetachedObjectState.Attached;
			}
		}

		private void OnSelectEventHandler(PointerEvent pointerEvent)
		{
			if (_detachedObjectState == DetachedObjectState.Attached)
			{
				_detachedObjectState = DetachedObjectState.Hover;
				_current.Release();
				_current = null;
			}
		}
	}
}