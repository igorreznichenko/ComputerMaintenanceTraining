using UnityEngine;

namespace ComputerMaintenanceTraining.Collisions
{
	public interface ITriggerEnterHandler
	{
		public void TriggerEnter(Collider other);
	}
}