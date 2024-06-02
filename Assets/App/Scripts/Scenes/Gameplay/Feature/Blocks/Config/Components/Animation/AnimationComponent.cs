using Scenes.Gameplay.Feature.Blocks.Animation;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Animation
{
	public class AnimationComponent : General.Component
	{
		[SerializeField, SerializeReference] private IAnimation blockAnimation;

		public override void Execute()
		{
			base.Execute();
			blockAnimation.PlayAnimation(Block.gameObject);
		}
	}
}
