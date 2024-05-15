using Scenes.Gameplay.Feature.Blocks.Config.Components.General;
using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components
{
	public class CollisionComponent : Component
	{
		[UnityEngine.SerializeField] private List<IComponent> collisionComponents;

		public override void Init(Block block)
		{
			base.Init(block);
			foreach (var component in collisionComponents)
			{
				component.Init(block);
			}
		}

		override public void Execute()
		{
			foreach (var component in collisionComponents)
			{
				component.Execute();
			}
		}
	}
}