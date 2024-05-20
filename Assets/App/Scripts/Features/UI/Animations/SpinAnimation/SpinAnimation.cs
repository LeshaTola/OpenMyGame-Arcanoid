using DG.Tweening;
using System;
using UnityEngine;

namespace Features.UI.Animations.SpinAnimation
{
	public class SpinAnimation : MonoBehaviour
	{
		[SerializeField] private float animationTime;
		[SerializeField] private int loops;

		private Tween tween;

		public void StartAnimation(Action action = null)
		{
			CleanUp();
			tween = transform.DORotate(new Vector3(0, 0, -360), animationTime, RotateMode.FastBeyond360);
			tween.SetEase(Ease.Linear);
			tween.onComplete += () => action?.Invoke();
			tween.SetLoops(loops);
		}

		public void StopAnimation()
		{
			CleanUp();
		}

		private void CleanUp()
		{
			if (tween != null)
			{
				tween.Kill();
			}
		}
	}
}
