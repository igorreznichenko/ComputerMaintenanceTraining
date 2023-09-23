using DG.Tweening;
using UnityEngine;

namespace ComputerMaintenanceTraining.Animations
{
	public class CPUThermalPasteFillAnimation : MonoBehaviour
	{
		[SerializeField]
		private Transform _thermalPaste;

		[SerializeField]
		private Vector3 _targetSize;

		[SerializeField]
		private float _animationTime;

		private Tween _animation = null;

		public void Play()
		{
			_animation?.Kill();

			_thermalPaste.localScale = Vector3.zero;

			_animation = _thermalPaste.DOScale(_targetSize, _animationTime);
		}
	}
}