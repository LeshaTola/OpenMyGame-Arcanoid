using Cysharp.Threading.Tasks;
using Module.ObjectPool.KeyPools;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb.Strategies.Processing
{
	public abstract class BombProcessingStrategy : IBlockProcessingStrategy
	{
		[SerializeField] protected float pauseBetweenExplosions;

		[SerializeField] private ParticlesDatabase particlesDatabase;
		[ShowIf("@particlesDatabase!=null")]
		[ValueDropdown("@particlesDatabase.GetKeys()")]
		[SerializeField] private string particleKey;

		public abstract UniTask Process(List<Block> blocks);

		public void SpawnExplosion(Block blockToDamage)
		{
			var newExplosion = blockToDamage.KeyPool.Get(particleKey);
			newExplosion.transform.position = blockToDamage.transform.position;
			newExplosion.Particle.Play();
		}
	}
}
