using Cysharp.Threading.Tasks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb.Strategies;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb
{
	public class BombComponent : General.Component
	{
		[SerializeField] private float pauseBetweenExplosions;
		[SerializeField] private IBombStrategy bombStrategy;

		public override void Execute()
		{
			base.Execute();

			bombStrategy.Init(Block);
			List<List<Block>> blocksToDestroyLists = bombStrategy.GetBlocksToDestroy();
			if (blocksToDestroyLists == null || blocksToDestroyLists.Count <= 0)
			{
				return;
			}

			foreach (var list in blocksToDestroyLists)
			{
				DestroyBlocksAsync(list);
			}
		}

		private async void DestroyBlocksAsync(List<Block> blocksToDestroy)
		{
			foreach (var block in blocksToDestroy)
			{
				if (block == null)
				{
					continue;
				}

				if (block.Config.TryGetComponent(out HealthComponent healthComponent))
				{
					SpawnExplosion(block);
					healthComponent.Kill();
				}
				await UniTask.Delay(System.TimeSpan.FromSeconds(pauseBetweenExplosions));
			}
		}

		private void SpawnExplosion(Block blockToDamage)
		{
			var newExplosion = blockToDamage.KeyPool.Get("explosion");
			newExplosion.transform.position = blockToDamage.transform.position;
			newExplosion.Particle.Play();
		}

	}

	public struct Line
	{
		public Vector2Int Direction;
		[Min(-1)] public int iterations;
	}
}
