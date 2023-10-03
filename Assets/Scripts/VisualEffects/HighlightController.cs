using UnityEngine;

namespace ComputerMaintenanceTraining.VisualEffects
{
	public abstract class HighlightController
	{
		public abstract void StartHighlight(Color color);

		public abstract void StopHighlight();
	}
}