using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ComputerMaintenanceTraining.VisualEffects
{
	public class Fader : MonoBehaviour
	{
		[SerializeField]
		private Graphic _target;

		[SerializeField]
		private float _fadeTime;

		public void FadeIn(Action callback = null)
		{
			SetGraphicColorAlpha(0);
			_target.DOFade(1, _fadeTime).OnComplete(() => callback?.Invoke());
		}

		public void FadeOut(Action callback = null)
		{
			SetGraphicColorAlpha(1);
			_target.DOFade(0, _fadeTime).OnComplete(() => callback?.Invoke());
		}

		private void SetGraphicColorAlpha(float value)
		{
			Color current = _target.color;
			current.a = value;
			_target.color = current;
		}
	}
}