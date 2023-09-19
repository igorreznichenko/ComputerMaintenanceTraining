using UnityEngine;

namespace ComputerMaintenanceTraining.Collisions
{
	public interface ITriggerable
	{
		public void TriggerEnter(Collider other);

		public void TriggerExit(Collider other);
	}
}