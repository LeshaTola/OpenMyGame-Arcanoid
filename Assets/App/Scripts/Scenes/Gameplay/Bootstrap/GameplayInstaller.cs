using Features.FileProvider;
using Module.ObjectPool;
using Module.TimeProvider;
using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.Health;
using Scenes.Gameplay.Feature.Health.Configs;
using Scenes.Gameplay.Feature.LevelCreation;
using Scenes.Gameplay.Feature.LevelCreation.Configs;
using Scenes.Gameplay.Feature.LevelCreation.LevelInfoProviders;
using Scenes.Gameplay.Feature.Player.Ball;
using Scenes.Gameplay.Feature.Player.Ball.Services;
using Scenes.Gameplay.Feature.Player.PlayerInput;
using Scenes.Gameplay.Feature.Progress;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class GameplayInstaller : MonoInstaller
	{
		[SerializeField] private Camera mainCamera;

		[Header("Level Creation")]
		[SerializeField] private LevelConfig levelConfig;
		[SerializeField] private BlocksDictionary blocksDictionary;
		[SerializeField] private Block blockTemplate;
		[SerializeField] private Transform container;

		[Header("Balls")]
		[SerializeField] private int ballCount;
		[SerializeField] private Ball ballTemplate;
		[SerializeField] private Transform ballsContainer;

		[Header("Other")]
		[SerializeField] private int winProgress = 100;
		[SerializeField] private HealthConfig config;


		public override void InstallBindings()
		{
			BindBallService();
			BindProgressController();
			BindHealthController();
			BindBoundaryValidator();
			BindBallsPool();
			BindLevelGenerator();
			BindFileProvider();
			BindLevelInfoProvider();
			BindBlockFactory();
			BindTimeProvider();
			BindInput();
		}

		private void BindBallService()
		{
			Container.Bind<IBallService>().To<BallService>().AsSingle();
		}

		private void BindBoundaryValidator()
		{
			Container.BindInterfacesAndSelfTo<BoundaryValidator>().AsSingle();
		}

		private void BindHealthController()
		{
			Container.BindInterfacesAndSelfTo<HealthController>().AsSingle().WithArguments(config);
		}

		private void BindProgressController()
		{
			Container.Bind<IProgressController>().To<ProgressController>().AsSingle().WithArguments(winProgress);
		}

		private void BindBallsPool()
		{
			Container.Bind<IPool<Ball>>()
				.To<MonoBehObjectPool<Ball>>()
				.AsSingle()
				.WithArguments(ballTemplate, ballCount, ballsContainer);
		}

		private void BindLevelGenerator()
		{
			Container.Bind<ILevelGenerator>()
				.To<LevelGenerator>()
				.AsSingle()
				.WithArguments(levelConfig);
		}

		private void BindFileProvider()
		{
			Container.Bind<IFileProvider>().To<ResourcesFileProvider>().AsSingle();
		}

		private void BindLevelInfoProvider()
		{
			Container.Bind<ILevelInfoProvider>().To<LevelInfoProvider>().AsSingle();
		}

		private void BindBlockFactory()
		{
			Container.Bind<IBlockFactory>()
				.To<BlockFactory>()
				.AsSingle()
				.WithArguments(blocksDictionary, blockTemplate, container);
		}

		private void BindInput()
		{
			Container.BindInterfacesTo<MouseInput>()
				.AsSingle()
				.WithArguments(mainCamera);
		}

		private void BindTimeProvider()
		{
			Container.Bind<ITimeProvider>()
				.To<GameplayTimeProvider>()
				.AsSingle();
		}
	}
}
