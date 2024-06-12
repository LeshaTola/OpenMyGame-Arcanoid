using Module.ObjectPool;
using Module.ObjectPool.KeyPools;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class ParticlesKeyPoolInstaller : MonoInstaller
	{
		[SerializeField] private ParticlesDatabase particlesDatabase;
		[SerializeField] private Transform container;

		public override void InstallBindings()
		{
			BindExplosionsParticlePool();
		}

		private void BindExplosionsParticlePool()
		{
			Container.Bind<KeyPool<PooledParticle>>().AsSingle().WithArguments(particlesDatabase.Particles, container);
		}
	}
}
