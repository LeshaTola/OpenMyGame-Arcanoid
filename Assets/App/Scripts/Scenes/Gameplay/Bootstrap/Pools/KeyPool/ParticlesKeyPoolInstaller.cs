using Module.ObjectPool;
using Module.ObjectPool.KeyPools;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class ParticlesKeyPoolInstaller : MonoInstaller
	{
		[SerializeField] private List<KeyPoolObject<PooledParticle>> particles;

		public override void InstallBindings()
		{
			BindExplosionsParticlePool();
		}

		private void BindExplosionsParticlePool()
		{
			Container.Bind<KeyPool<PooledParticle>>().AsSingle().WithArguments(particles);
		}
	}
}
