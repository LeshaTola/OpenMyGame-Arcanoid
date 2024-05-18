using Module.TimeProvider;
using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.LevelCreation;
using Scenes.Gameplay.Feature.LevelCreation.Configs;
using Scenes.Gameplay.Feature.Player.PlayerInput;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class GameplayInstaller : MonoInstaller
	{
		[SerializeField] private Camera mainCamera;

		[Header("Level Creation")]
		[SerializeField] private BlocksDictionary blocksDictionary;
		[SerializeField] private Block blockTemplate;
		[SerializeField] private Transform container;

		public override void InstallBindings()
		{
			BindBlockFactory();
			BindTimeProvider();
			BindInput();
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
