using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ComputerMaintenanceTraining.UI.VFX
{
	public class GraphicCycleFade : MonoBehaviour
	{
		[SerializeField]
		private float _fadeTime;

		[SerializeField]
		private Graphic _graphic;

		[SerializeField]
		private Ease _ease;

		private Sequence _fadeSequence = null;

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
			Color color = _graphic.color;

			color.a = 0;

			_fadeSequence?.Kill();

			_fadeSequence = DOTween.Sequence();

			_graphic.color = color;

			float semiTime = _fadeTime / 2;

			_fadeSequence.Append(_graphic.DOFade(1, semiTime).SetEase(_ease));
			_fadeSequence.Append(_graphic.DOFade(0, semiTime).SetEase(_ease));
			_fadeSequence.SetLoops(-1);
		}

		private void Stop()
		{
			_fadeSequence?.Kill();
		}
	}
}