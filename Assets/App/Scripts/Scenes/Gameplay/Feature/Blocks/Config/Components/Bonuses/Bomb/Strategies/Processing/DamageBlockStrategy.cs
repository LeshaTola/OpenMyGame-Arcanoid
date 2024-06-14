using Cysharp.Threading.Tasks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Partcles;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb.Strategies.Processing
{
	public class DamageBlockStrategy : BombProcessingStrategy
	{
		[SerializeField] private int damage;
		public override async UniTask Process(List<Block> blocks)
		{
			foreach (var block in blocks)
			{
				if (block == null)
				{
					continue;
				}

				if (block.Config.TryGetComponent(out HealthComponent healthComponent))
				{
					SpawnParticlesComponent.Init(block);
					SpawnParticlesComponent.SpawnParticle(block.transform.position, Vector2.zero, block.SizeMultiplier);
					healthComponent.ReduceHealth(damage);
				}
				await UniTask.Delay(System.TimeSpan.FromSeconds(pauseBetweenExplosions));
			}
		}
	}
}
