using Cysharp.Threading.Tasks;
using DG.Tweening;
using Module.PopupLogic.General;
using UnityEngine;

namespace Features.Popups.Animations
{
	public class ScalePopupAnimation : MonoBehaviour, IPopupAnimation
	{
		[SerializeField] private float animationTime;
		private Tween tweenAnimation;

		public async UniTask Hide()
		{
			CleanUp();
			tweenAnimation = transform.DOScale(Vector2.zero, animationTime).SetEase(Ease.InBack);
			await tweenAnimation.AsyncWaitForCompletion();
		}

		public async UniTask Show()
		{
			CleanUp();
			tweenAnimation = transform.DOScale(Vector2.one, animationTime).SetEase(Ease.OutBack);
			await tweenAnimation.AsyncWaitForCompletion();
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
