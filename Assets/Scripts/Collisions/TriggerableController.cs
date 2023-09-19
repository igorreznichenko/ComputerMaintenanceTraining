using UnityEngine;

namespace ComputerMaintenanceTraining.Collisions
{
	public class TriggerableController : MonoBehaviour
	{
		[SerializeField]
		private GameObject _targetTriggerable;

		private ITriggerable _trigerrable;

		private void Awake()
		{
			_trigerrable = _targetTriggerable.GetComponent<ITriggerable>();
		}

		private void OnTriggerEnter(Collider other)
		{
			_trigerrable.TriggerEnter(other);
		}

		private void OnTriggerExit(Collider other)
		{
			_trigerrable.TriggerExit(other);
		}
	}
}