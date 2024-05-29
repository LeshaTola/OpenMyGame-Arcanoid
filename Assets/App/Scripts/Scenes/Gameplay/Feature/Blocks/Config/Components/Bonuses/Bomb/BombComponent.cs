using Cysharp.Threading.Tasks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.General;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using System;
using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb
{
	public class BombComponent : Component
	{
		[UnityEngine.SerializeField] private int radius;
		[UnityEngine.SerializeField] private int damage;
		[UnityEngine.SerializeField] private float pauseBetweenExplosions;

		public override void Execute()
		{
			ExecuteAsync();
		}

		private async void ExecuteAsync()
		{
			int currentRadius = 1;

			List<Block> blocksToDamage;
			while (currentRadius <= radius)
			{
				blocksToDamage = GetBlocksOnRadius(Block.MatrixPosition, currentRadius);
				DamageBlocks(blocksToDamage);
				currentRadius++;
				await UniTask.Delay(TimeSpan.FromSeconds(pauseBetweenExplosions));
			}
		}

		private List<Block> GetBlocksOnRadius(UnityEngine.Vector2Int center, int radius)
		{
			List<Block> blocksToDamage = new();

			for (int x = -radius; x <= radius; x++)
			{
				for (int y = -radius; y <= radius; y++)
				{
					if (UnityEngine.Mathf.Abs(x) + UnityEngine.Mathf.Abs(y) != radius)
					{
						continue;
					}

					UnityEngine.Vector2Int pos = new(center.x + x, center.y + y);
					if (Block.Neighbors.TryGetValue(pos, out Block block))
					{
						blocksToDamage.Add(block);
					}
				}
			}
			return blocksToDamage;
		}

		private void DamageBlocks(List<Block> blocksToDamage)
		{
			foreach (var block in blocksToDamage)
			{
				HealthComponent healthComponent = block.Config.GetComponent<HealthComponent>();
				if (healthComponent == null)
				{
					return;
				}
				healthComponent.ReduceHealth(damage);
			}
		}
	}
}
