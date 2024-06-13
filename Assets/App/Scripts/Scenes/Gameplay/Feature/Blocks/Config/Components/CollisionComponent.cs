using Scenes.Gameplay.Feature.Blocks.Config.Components.General;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Partcles;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components
{
	public class CollisionComponent : General.Component
	{
		[SerializeField] private List<IComponent> collisionComponents;
		[SerializeField] private SpawnParticlesComponent spawnParticlesComponent;
		public Collision2D Collision2D { get; set; }
		public List<IComponent> CollisionComponents { get => collisionComponents; }

		public override void Init(Block block)
		{
			base.Init(block);
			spawnParticlesComponent.Init(block);
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
			spawnParticlesComponent.SpawnParticle(Collision2D.contacts[0].point, Collision2D.contacts[0].normal * -1);
		}
	}
}