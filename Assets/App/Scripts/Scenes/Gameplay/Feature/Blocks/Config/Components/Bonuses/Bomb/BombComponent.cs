using Cysharp.Threading.Tasks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb
{
	public class BombComponent : General.Component
	{
		[SerializeField] private int damage;
		[SerializeField] private float pauseBetweenExplosions;
		[SerializeField] private ParticleSystem explosionParticles;

		public override void Execute()
		{
			ExecuteAsync();
		}

		private async void ExecuteAsync()
		{
			List<Block> blocksToDamage;

			blocksToDamage = GetBlocksOnRadius(Block.MatrixPosition);
			await DamageBlocksAsync(blocksToDamage);
		}

		private List<Block> GetBlocksOnRadius(Vector2Int center)
		{
			List<Block> blocksToDamage = new();

			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
					if (i == 0 && j == 0)
					{
						continue;
					}

					Vector2Int pos = new(center.x + i, center.y + j);
					if (Block.Neighbors.TryGetValue(pos, out Block block))
					{
						blocksToDamage.Add(block);
					}
				}
			}

			return blocksToDamage;
		}

		private async UniTask DamageBlocksAsync(List<Block> blocksToDamage)
		{
			foreach (var block in blocksToDamage)
			{
				if (block == null)
				{
					continue;
				}

				HealthComponent healthComponent = block.Config.GetComponent<HealthComponent>();
				if (healthComponent == null)
				{
					return;
				}
				healthComponent.ReduceHealth(damage);
				SpawnExplosion(block);
				await UniTask.Delay(TimeSpan.FromSeconds(pauseBetweenExplosions));
			}
		}

		private void SpawnExplosion(Block blockToDamage)
		{
			var newExplosion = GameObject.Instantiate(explosionParticles);//TODO swap with objectPool
			newExplosion.transform.position = blockToDamage.transform.position;
			newExplosion.Play();
		}
	}
}
