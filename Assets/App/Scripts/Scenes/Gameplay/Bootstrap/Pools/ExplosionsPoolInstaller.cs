using Module.ObjectPool;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class ExplosionsPoolInstaller : MonoInstaller
	{
		[SerializeField] private int preloadCount;
		[SerializeField] private PooledParticle particleTemplate;
		[SerializeField] private Transform container;

		public override void InstallBindings()
		{
			BindExplosionsParticlePool();
		}

		private void BindExplosionsParticlePool()
		{
			Container.Bind<IPool<PooledParticle>>()
				.To<MonoBehObjectPool<PooledParticle>>()
				.AsSingle()
				.WithArguments(particleTemplate, preloadCount, container);
		}
	}
}
