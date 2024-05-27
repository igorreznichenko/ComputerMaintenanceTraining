using UnityEngine;

namespace ComputerMaintenanceTraining.VisualEffects
{
	public class OutlineHighlightController : HighlightController
	{
		[SerializeField]
		private Outline _outline;

		public override void StartHighlight(Color color)
		{
			_outline.enabled = true;
			_outline.OutlineColor = color;
		}

		public override void StopHighlight()
		{
			_outline.enabled = false;
		}
	}
}