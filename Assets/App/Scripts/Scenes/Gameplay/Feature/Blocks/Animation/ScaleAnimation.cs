using DG.Tweening;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Animation
{
	public class ScaleAnimation : IAnimation
	{
		[SerializeField] private float duration = 0.5f;
		[SerializeField] private Vector3 startScale = Vector3.one;
		[SerializeField] private Vector3 targetScale = Vector3.zero;

		public Tween Tween { get; private set; }

		public void PlayAnimation(GameObject gameObject)
		{
			Tween = gameObject.transform.DOScale(targetScale, duration).From(startScale).SetEase(Ease.InOutBack);
		}

		public void CleanUp()
		{
			if (Tween != null)
			{
				Tween.Complete();
				Tween.Kill();
			}
		}
	}
}
