using ComputerMaintenanceTraining.Enums;
using ComputerMaintenanceTraining.VisualEffects;
using UnityEditor;
using UnityEngine;

namespace ComputerMaintenanceTraining.AssemblyObjects.Detachables
{
	public class DetachedObjectHighlightVisual : MonoBehaviour
	{
		[SerializeField]
		private Color _highlightColor;

		[SerializeField]
		private HighlightController _highlightController;

		[SerializeField]
		private DetachedObject _target;


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
			_target.OnDetachedObjectStateChanged += OnDetachedObjectStateChanged;
		}

		private void UnsubscribeEvents()
		{
			_target.OnDetachedObjectStateChanged -= OnDetachedObjectStateChanged;
		}

		private void OnDetachedObjectStateChanged(DetachedObjectState detachedObjectState)
		{
			if(detachedObjectState == DetachedObjectState.Hover)
			{
				_highlightController.StartHighlight(_highlightColor);
			}
			else
			{
				_highlightController.StopHighlight();
			}
		}
	}
}