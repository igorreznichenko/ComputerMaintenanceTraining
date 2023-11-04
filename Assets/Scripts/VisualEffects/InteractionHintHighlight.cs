using Oculus.Interaction;
using UnityEngine;

namespace ComputerMaintenanceTraining.VisualEffects
{
	public class InteractionHintHighlight : MonoBehaviour
	{
		[SerializeField]
		private PointableUnityEventWrapper _pointableUnityEventWrapper;

		[SerializeField]
		private HighlightController _highlightController;

		[SerializeField]
		private Color _highlightColor = Color.blue;

		protected bool IsActive = false;

		private void OnEnable()
		{
			SubscribeEvents();
		}

		private void OnDisable()
		{
			UnsubscribeEvents();
		}

		private void SubscribeEvents()
		{
			_pointableUnityEventWrapper.WhenSelect.AddListener(OnSelectEventHandler);
		}

		private void UnsubscribeEvents()
		{
			_pointableUnityEventWrapper.WhenSelect.AddListener(OnSelectEventHandler);
		}

		private void OnSelectEventHandler(PointerEvent pointerEvent)
		{
			Deactivate();
		}

		public void Activate()
		{
			_highlightController.StartHighlight(_highlightColor);
			IsActive = true;
		}

		protected virtual void Deactivate()
		{
			_highlightController.StopHighlight();
			IsActive = false;
		}
	}
}