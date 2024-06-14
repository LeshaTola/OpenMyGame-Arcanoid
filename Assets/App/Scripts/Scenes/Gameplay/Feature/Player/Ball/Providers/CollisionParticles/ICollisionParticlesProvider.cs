using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Ball.Providers.CollisionParticles
{
	public interface ICollisionParticlesProvider
	{
		void SpawnParticle(Collision2D collision);
	}
}
