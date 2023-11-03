using UnityEngine;

namespace ComputerMaintenanceTraining.Collisions
{
	public class TriggerableController : MonoBehaviour
	{
		[SerializeField]
		private GameObject _targetTriggerable;

		private ITriggerEnterHandler _triggerEnterHandler = null;
		private ITriggerExitHandler _triggerExitHandler = null;

		private void Awake()
		{
			_triggerEnterHandler = _targetTriggerable.GetComponent<ITriggerEnterHandler>();
			_triggerExitHandler = _targetTriggerable.GetComponent<ITriggerExitHandler>();
		}

		private void OnTriggerEnter(Collider other)
		{
			_triggerEnterHandler?.TriggerEnter(other);
		}

		private void OnTriggerExit(Collider other)
		{
			_triggerExitHandler?.TriggerExit(other);
		}
	}
}