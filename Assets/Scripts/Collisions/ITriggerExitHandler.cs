using UnityEngine;

namespace ComputerMaintenanceTraining.Collisions
{
	public interface ITriggerExitHandler
	{
		public void TriggerExit(Collider other);
	}
}