using DG.Tweening;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Player
{
	public class PlateVisual : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer spriteRenderer;
		[SerializeField] private float punchingOffset = 0.1f;
		[SerializeField] private float punchingAnimationTime = 0.4f;

		private float defaultWidth;

		public void Init()
		{
			defaultWidth = spriteRenderer.size.x;
		}

		public void PlayPunchingAnimation(Vector2 direction)
		{
			Sequence punchingSequence = DOTween.Sequence();

			punchingSequence.Append(transform.DOLocalMove(transform.localPosition
											+ (Vector3)direction.normalized
											* punchingOffset, punchingAnimationTime).SetEase(Ease.OutQuad));

			punchingSequence.Append(transform.DOLocalMove(Vector2.zero, punchingAnimationTime).SetEase(Ease.OutQuad));
		}

		public void ChangeWidth(float multiplier, float duration = 0)
		{
			AnimateWidth(defaultWidth, defaultWidth * multiplier, duration);
		}

		public void ResetWidth(float duration = 0)
		{
			AnimateWidth(spriteRenderer.size.x, defaultWidth, duration);
		}

		private void AnimateWidth(float from, float to, float duration = 0)
		{
			DOVirtual.Float(from, to, duration, value =>
			{
				spriteRenderer.size = new Vector2(value, spriteRenderer.size.y);
			});
		}
	}
}
