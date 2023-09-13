using ComputerMaintenanceTraining.Enums;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
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
		private HandGrabInteractable _handGrabInteractable;

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
			_handGrabInteractable.WhenStateChanged += OnHandGrabInteractableStateChanged;
		}

		private void UnsubscribeEvents()
		{
			_handGrabInteractable.WhenStateChanged -= OnHandGrabInteractableStateChanged;
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

		private void OnHandGrabInteractableStateChanged(InteractableStateChangeArgs args)
		{

			if (args.NewState == InteractableState.Select && args.PreviousState != InteractableState.Select)
			{
				if (_detachedObjectState == DetachedObjectState.Attached)
				{
					_detachedObjectState = DetachedObjectState.Hover;
					_current.Release();
					_current = null;
				}
			}
			else if (args.NewState != InteractableState.Select && args.NewState == InteractableState.Select)
			{
				if (_detachedObjectState == DetachedObjectState.Hover)
				{
					_current = _candidates.First();
					_current.SetDetachedObject(this);
					_detachedObjectState = DetachedObjectState.Attached;
				}
			}
		}
	}
}