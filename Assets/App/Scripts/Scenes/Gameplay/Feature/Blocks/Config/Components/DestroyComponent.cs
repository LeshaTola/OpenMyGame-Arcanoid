using DG.Tweening;
using Scenes.Gameplay.Feature.Blocks.Animation;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components
{
	public class DestroyComponent : General.Component
	{
		[SerializeField] private IAnimation animation;

		public override void Execute()
		{
			if (animation == null)
			{
				DestroyBlock();
				return;
			}

			animation.PlayAnimation(Block.gameObject);
			animation.Tween.onComplete += DestroyBlock;
		}

		private void DestroyBlock()
		{
			Block.transform.DOKill();
			GameObject.Destroy(Block.gameObject);
		}
	}
}
