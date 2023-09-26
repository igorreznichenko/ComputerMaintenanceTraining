using DG.Tweening;
using UnityEngine;

namespace ComputerMaintenanceTraining.Animations
{
	public class CPUThermalPasteFillAnimation : MonoBehaviour
	{
		[SerializeField]
		private Transform _thermalPasteVisual;

		[SerializeField]
		private Vector3 _targetSize;

		[SerializeField]
		private float _animationTime;

		private Tween _animation = null;

		[ContextMenu("Play")]
		public void Play()
		{
			_animation?.Kill();

			_thermalPasteVisual.localScale = Vector3.zero;

			_thermalPasteVisual.gameObject.SetActive(true);

			_animation = _thermalPasteVisual.DOScale(_targetSize, _animationTime);
		}
	}
}