using Scenes.Gameplay.Feature.Blocks.Config.Components.General;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components
{
	public class CollisionComponent : General.Component
	{
		[SerializeField] private List<IComponent> collisionComponents;

		public GameObject CollisionGameObject { get; set; }
		public List<IComponent> CollisionComponents { get => collisionComponents; }

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