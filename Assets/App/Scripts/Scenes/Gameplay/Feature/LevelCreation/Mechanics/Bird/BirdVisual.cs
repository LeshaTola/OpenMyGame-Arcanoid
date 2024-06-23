using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Scenes.Gameplay.Feature.LevelCreation.Mechanics.Bird
{
	public class BirdVisual : MonoBehaviour
	{
		[SerializeField] private float ApplyDamageAnimationTime;
		[SerializeField] private float DestroyAnimationTime;

		private Tween tween;

		public async UniTask ApplyDamageAnimation()
		{
			Cleanup();
			tween = transform.DOScale(Vector2.one / 2, ApplyDamageAnimationTime / 2).SetLoops(2, LoopType.Yoyo);
			tween.SetEase(Ease.InSine);
			await tween.AsyncWaitForCompletion();
		}

		public async UniTask DestroyAnimation()
		{
			Cleanup();
			tween = transform.DOScale(Vector2.zero, DestroyAnimationTime);
			tween.SetEase(Ease.InQuint);
			await tween.AsyncWaitForCompletion();
		}

		public void ResetVisual()
		{
			transform.localScale = Vector2.one;
		}

		public void Cleanup()
		{
			if (tween.IsActive())
			{
				tween.Kill();
			}
		}
	}
}

