using Cysharp.Threading.Tasks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb.Strategies.Processing
{
	public class KillBlockStrategy : BombProcessingStrategy
	{
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
					healthComponent.Kill();
				}
				await UniTask.Delay(System.TimeSpan.FromSeconds(pauseBetweenExplosions));
			}
		}
	}
}
