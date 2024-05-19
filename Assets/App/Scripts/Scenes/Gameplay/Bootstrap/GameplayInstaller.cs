using Features.FileProvider;
using Module.TimeProvider;
using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.LevelCreation;
using Scenes.Gameplay.Feature.LevelCreation.Configs;
using Scenes.Gameplay.Feature.LevelCreation.LevelInfoProviders;
using Scenes.Gameplay.Feature.Player.PlayerInput;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class GameplayInstaller : MonoInstaller
	{
		[SerializeField] private Camera mainCamera;

		[Header("Level Creation")]
		[SerializeField] private LevelGenerator levelGenerator;
		[SerializeField] private BlocksDictionary blocksDictionary;
		[SerializeField] private Block blockTemplate;
		[SerializeField] private Transform container;

		public override void InstallBindings()
		{
			BindLevelGenerator();
			BindFileProvider();
			BindLevelInfoProvider();
			BindBlockFactory();
			BindTimeProvider();
			BindInput();
		}

		private void BindLevelGenerator()
		{
			Container.BindInstance(levelGenerator);//TODO move to interface. Remove from scene
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
