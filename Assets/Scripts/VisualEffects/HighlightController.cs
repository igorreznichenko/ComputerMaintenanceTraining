using UnityEngine;

namespace ComputerMaintenanceTraining.VisualEffects
{
	public abstract class HighlightController: MonoBehaviour
	{
		public abstract void StartHighlight(Color color);

		public abstract void StopHighlight();
	}
}