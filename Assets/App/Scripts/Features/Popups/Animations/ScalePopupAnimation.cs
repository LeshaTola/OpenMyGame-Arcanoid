using DG.Tweening;
using Module.PopupLogic.General;
using System;
using UnityEngine;

namespace Features.Popups.Animations
{
	public class ScalePopupAnimation : MonoBehaviour, IPopupAnimation
	{
		[SerializeField] private float animationTime;
		private Tween tweenAnimation;

		public void Hide(Action onComplete = null)
		{
			CleanUp();
			tweenAnimation = transform.DOScale(Vector2.zero, animationTime).SetEase(Ease.InBack);
			tweenAnimation.onComplete += () => onComplete?.Invoke();
		}

		public void Show(Action onComplete = null)
		{
			CleanUp();
			tweenAnimation = transform.DOScale(Vector2.one, animationTime).SetEase(Ease.OutBack);
			tweenAnimation.onComplete += () => onComplete?.Invoke();
		}

		private void CleanUp()
		{
			if (tweenAnimation != null)
			{
				tweenAnimation.Kill();
			}
		}
	}
}
