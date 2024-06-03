using DG.Tweening;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Animation
{
	public interface IAnimation
	{
		Tween Tween { get; }

		void CleanUp();
		void PlayAnimation(GameObject gameObject);
	}
}
