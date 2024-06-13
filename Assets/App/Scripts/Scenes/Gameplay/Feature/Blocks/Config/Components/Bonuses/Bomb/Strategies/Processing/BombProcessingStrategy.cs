using Cysharp.Threading.Tasks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Partcles;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb.Strategies.Processing
{
	public abstract class BombProcessingStrategy : IBlockProcessingStrategy
	{
		[SerializeField] protected float pauseBetweenExplosions;

		[SerializeField] private SpawnParticlesComponent spawnParticlesComponent;

		public SpawnParticlesComponent SpawnParticlesComponent { get => spawnParticlesComponent; }

		public abstract UniTask Process(List<Block> blocks);
	}
}
