using Module.ObjectPool;
using Module.ObjectPool.KeyPools;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Ball.Providers.CollisionParticles
{
	public class CollisionParticlesProvider : ICollisionParticlesProvider
	{
		private KeyPool<PooledParticle> particles;
		private CollisionToParticlesDatabase collisionToParticlesDatabase;

		public CollisionParticlesProvider(KeyPool<PooledParticle> particles,
			CollisionToParticlesDatabase collisionToParticlesDatabase)
		{
			this.particles = particles;
			this.collisionToParticlesDatabase = collisionToParticlesDatabase;
		}

		public void SpawnParticle(Collision2D collision)
		{
			foreach (var objectToParticle in collisionToParticlesDatabase.ObjectToParticle)
			{
				if (collision.gameObject.TryGetComponent(objectToParticle.Key.GetType(), out Component component))
				{
					var particle = particles.Get(objectToParticle.Value);
					particle.transform.position = collision.contacts[0].point;
					particle.transform.up = collision.contacts[0].normal;
					particle.Particle.Play();
				}
			}
		}
	}
}
