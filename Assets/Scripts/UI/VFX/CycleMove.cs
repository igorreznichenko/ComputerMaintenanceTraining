using DG.Tweening;
using UnityEngine;

namespace ComputerMaintenanceTraining.UI.VFX
{
	public class CycleMove : MonoBehaviour
	{
		[SerializeField]
		private Transform _target;

		[SerializeField]
		private Transform[] _points;

		[SerializeField]
		private float _moveTime;

		[SerializeField]
		private Ease _ease;

		private Sequence _moveSequence = null;

		private void OnEnable()
		{
			Play();
		}

		private void OnDisable()
		{
			Stop();
		}

		private void Play()
		{
			_moveSequence?.Kill();

			_target.position = _points[0].position;

			float semiTime = _moveTime / _points.Length;

			_moveSequence = DOTween.Sequence();

			for (int i = 1; i < _points.Length; i++)
			{
				_moveSequence.Append(_target.DOMove(_points[i].position, semiTime).SetEase(_ease));
			}

			_moveSequence.Append(_target.DOMove(_points[0].position, semiTime).SetEase(_ease));

			_moveSequence.SetLoops(-1);
		}

		private void Stop()
		{
			_moveSequence?.Kill();
		}
	}
}