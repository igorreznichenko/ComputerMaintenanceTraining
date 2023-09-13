using Oculus.Interaction.HandGrab;
using UnityEngine;

namespace ComputerMaintenanceTraining.Interaction
{
	public class HandGrabSwitchController : MonoBehaviour
    {
        [SerializeField]
        private HandGrabInteractable _handGrabInteractable;

        public void ActivateGrab()
        {
            _handGrabInteractable.enabled = true;
        }   
        
        public void DeactivateGrab()
        {
            _handGrabInteractable.enabled = false;
        }
    }
}