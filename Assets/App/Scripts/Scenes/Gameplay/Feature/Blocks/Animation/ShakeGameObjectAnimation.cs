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

		public Tween Tween { get; private set; }

		public void PlayAnimation(GameObject gameObject)
		{
			CleanUp();
			Tween = gameObject.transform.DOShakePosition(duration,
								   new Vector3(strength, 0, 0),
								   vibrato,
								   randomness,
								   randomnessMode: ShakeRandomnessMode.Harmonic);
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
