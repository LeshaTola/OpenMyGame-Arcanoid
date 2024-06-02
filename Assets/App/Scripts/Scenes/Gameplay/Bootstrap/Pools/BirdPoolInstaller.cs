using Module.ObjectPool;
using Scenes.Gameplay.Feature.LevelCreation.Mechanics.Bird;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class BirdPoolInstaller : MonoInstaller
	{
		[SerializeField] private Bird birdTemplate;
		[SerializeField] private int preloadCount;
		[SerializeField] private Transform birdContainer;
		[SerializeField] private BirdConfig birdConfig;

		public override void InstallBindings()
		{
			BindBirdMechanics();
			BindBirdsPool();
		}

		private void BindBirdMechanics()
		{
			Container.Bind<BirdLevelMechanics>().AsSingle().WithArguments(birdConfig);
		}

		private void BindBirdsPool()
		{
			Container.Bind<IPool<Bird>>()
				.To<MonoBehObjectPool<Bird>>()
				.AsSingle().WithArguments(birdTemplate, preloadCount, birdContainer);
		}
	}
}
