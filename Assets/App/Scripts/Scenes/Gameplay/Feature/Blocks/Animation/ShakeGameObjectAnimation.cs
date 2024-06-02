using DG.Tweening;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Animation
{
	public class ShakeGameObjectAnimation : IAnimation
	{
		[SerializeField] private float duration = 0.5f;
		[SerializeField] private float strength = 1.0f;
		[SerializeField] private int vibrato = 5;
		[SerializeField] private float randomness = 90f;

		private Tween tween;

		public void PlayAnimation(GameObject gameObject)
		{
			CleanUp();
			tween = gameObject.transform.DOShakePosition(duration,
								   new Vector3(strength, 0, 0),
								   vibrato,
								   randomness,
								   randomnessMode: ShakeRandomnessMode.Harmonic);

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
