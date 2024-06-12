using Cysharp.Threading.Tasks;
using Module.ObjectPool.KeyPools;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb.Strategies;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb
{
	public class BombComponent : General.Component
	{
		[SerializeField] private float pauseBetweenExplosions;
		[SerializeField] private IBombStrategy bombStrategy;
		[SerializeField] private ParticlesDatabase particlesDatabase;
		[ValueDropdown("@particlesDatabase.GetKeys()")]
		[SerializeField] private string particleKey;

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
			var newExplosion = blockToDamage.KeyPool.Get(particleKey);
			newExplosion.transform.position = blockToDamage.transform.position;
			newExplosion.Particle.Play();
		}
	}
}
