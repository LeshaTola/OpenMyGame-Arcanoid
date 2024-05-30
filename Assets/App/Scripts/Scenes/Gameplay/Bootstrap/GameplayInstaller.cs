using Features.FileProvider;
using Module.TimeProvider;
using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Bonuses.Configs;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.Health;
using Scenes.Gameplay.Feature.Health.Configs;
using Scenes.Gameplay.Feature.LevelCreation;
using Scenes.Gameplay.Feature.LevelCreation.Configs;
using Scenes.Gameplay.Feature.LevelCreation.LevelInfoProviders;
using Scenes.Gameplay.Feature.LevelCreation.Providers.Level;
using Scenes.Gameplay.Feature.Player.PlayerInput;
using Scenes.Gameplay.Feature.Progress;
using Scenes.Gameplay.Feature.Reset.Services;
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
		[SerializeField] private BonusesDatabase bonusesDatabase;
		[SerializeField] private Block blockTemplate;
		[SerializeField] private Transform container;
		[SerializeField] private ResetService resetService;

		[Header("Progress")]
		[SerializeField] private int winProgress = 100;

		[Header("Health")]
		[SerializeField] private HealthConfig config;

		public override void InstallBindings()
		{
			RouterInstaller.Install(Container);

			BindResetService();
			BindProgressController();
			BindHealthController();
			BindBoundaryValidator();

			BindFileProvider();
			BindLevelInfoProvider();
			BindBlockFactory();
			BindLevelGenerator();
			BindLevelProvider();

			BindTimeProvider();
			BindInput();
		}

		private void BindLevelProvider()
		{
			Container.Bind<ILevelProvider>()
				.To<LevelProvider>()
				.AsSingle();
		}

		private void BindResetService()
		{
			Container.Bind<IResetService>().FromInstance(resetService).AsSingle();
		}

		private void BindBoundaryValidator()
		{
			Container.BindInterfacesAndSelfTo<BoundaryValidator>().AsSingle();
		}

		private void BindHealthController()
		{
			Container.BindInterfacesAndSelfTo<HealthController>()
				.AsSingle()
				.WithArguments(config);
		}

		private void BindProgressController()
		{
			Container.Bind<IProgressController>()
				.To<ProgressController>()
				.AsSingle()
				.WithArguments(winProgress);
		}

		private void BindLevelGenerator()
		{
			Container.Bind<ILevelGenerator>()
				.To<LevelGenerator>()
				.AsSingle()
				.WithArguments(levelConfig, bonusesDatabase);
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
