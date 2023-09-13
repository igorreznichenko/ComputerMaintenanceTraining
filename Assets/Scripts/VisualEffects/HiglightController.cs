using UnityEngine;

namespace ComputerMaintenanceTraining.VisualEffects
{
	public abstract class HiglightController
	{
		[SerializeField]
		private Color _highlightColor;

		public abstract void StartHighlight();

		public abstract void StopHighlight();
	}
}