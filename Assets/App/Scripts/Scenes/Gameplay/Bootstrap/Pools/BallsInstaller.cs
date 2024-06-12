using Module.ObjectPool;
using Scenes.Gameplay.Feature.Player.Ball;
using Scenes.Gameplay.Feature.Player.Ball.Providers.CollisionParticles;
using Scenes.Gameplay.Feature.Player.Ball.Services;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class BallsInstaller : MonoInstaller
	{
		[Header("Balls")]
		[SerializeField] private int ballCount;
		[SerializeField] private Ball ballTemplate;
		[SerializeField] private Transform ballsContainer;

		[SerializeField] private CollisionToParticlesDatabase collisionToParticlesDatabase;

		public override void InstallBindings()
		{
			BindBallsPool();
			BindBallService();
			Container.Bind<ICollisionParticlesProvider>()
				.To<CollisionParticlesProvider>()
				.AsSingle()
				.WithArguments(collisionToParticlesDatabase);

		}

		private void BindBallsPool()
		{
			Container.Bind<IPool<Ball>>()
				.To<MonoBehObjectPool<Ball>>()
				.AsSingle()
				.WithArguments(ballTemplate, ballCount, ballsContainer);
		}

		private void BindBallService()
		{
			Container.Bind<IBallService>().To<BallService>().AsSingle();
		}
	}
}
