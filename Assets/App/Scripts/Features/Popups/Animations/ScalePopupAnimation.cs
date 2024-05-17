﻿using DG.Tweening;
using Module.PopupLogic.General;
using System;
using UnityEngine;

namespace Features.Popups.Animations
{
	public class ScalePopupAnimation : MonoBehaviour, IPopupAnimation
	{
		[SerializeField] private float animationTime;
		private Tween tweenAnimation;

		public void Hide(Action onComplete)
		{
			CleanUp();
			tweenAnimation = transform.DOScale(Vector2.zero, animationTime);
			tweenAnimation.onComplete += () => onComplete?.Invoke();
		}

		public void Show(Action onComplete)
		{
			CleanUp();
			tweenAnimation = transform.DOScale(Vector2.one, animationTime);
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
