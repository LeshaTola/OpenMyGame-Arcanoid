using Cysharp.Threading.Tasks;
using DG.Tweening;
using Features.Popups.Languages;
using Module.Localization.Localizers;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Popups.Animations.Animator
{
	public class GeneralPopupAnimator : IPopupAnimator
	{
		private TMProLocalizer header;
		private List<PopupButton> buttons;
		private float eachAnimationDuration;

		public void Setup(TMProLocalizer header, List<PopupButton> buttons, float eachAnimationDuration)
		{
			this.header = header;
			this.buttons = buttons;
			this.eachAnimationDuration = eachAnimationDuration;
		}

		public async UniTask ShowAnimation()
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(header.transform.DOScale(Vector2.one, eachAnimationDuration).SetEase(Ease.OutBack));
			foreach (var button in buttons)
			{
				sequence.Append(button.transform.DOScale(Vector2.one, eachAnimationDuration).SetEase(Ease.OutBack));
			}
			await sequence.AsyncWaitForCompletion();
		}


		public async UniTask HideAnimation()
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(header.transform.DOScale(Vector2.zero, eachAnimationDuration).SetEase(Ease.InBack));
			foreach (var button in buttons)
			{
				sequence.Append(button.transform.DOScale(Vector2.zero, eachAnimationDuration).SetEase(Ease.InBack));
			}

			await sequence.AsyncWaitForCompletion();
		}

		public void ResetAnimation()
		{
			header.transform.localScale = Vector2.zero;
			foreach (var button in buttons)
			{
				button.transform.localScale = Vector2.zero;
			}
		}
	}
}
