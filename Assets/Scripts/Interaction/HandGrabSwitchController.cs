using Oculus.Interaction.HandGrab;
using UnityEngine;

namespace ComputerMaintenanceTraining.Interaction
{
	public class HandGrabSwitchController : MonoBehaviour
	{
		[SerializeField]
		private HandGrabInteractable _leftHandGrabInteractable;

		[SerializeField]
		private HandGrabInteractable _rightHandGrabInteractable;

		public void ActivateGrab()
		{
			SetActiveGrab(true);
		}

		public void DeactivateGrab()
		{
			SetActiveGrab(false);
		}

		private void SetActiveGrab(bool isActive)
		{
			_leftHandGrabInteractable.enabled = isActive;
			_rightHandGrabInteractable.enabled = isActive;
		}
	}
}