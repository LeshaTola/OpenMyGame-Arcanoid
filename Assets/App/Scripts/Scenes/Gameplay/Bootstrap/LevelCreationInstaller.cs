using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Bonuses.Configs;
using Scenes.Gameplay.Feature.LevelCreation;
using Scenes.Gameplay.Feature.LevelCreation.Configs;
using Scenes.Gameplay.Feature.LevelCreation.LevelInfoProviders;
using Scenes.Gameplay.Feature.LevelCreation.Mechanics;
using Scenes.Gameplay.Feature.LevelCreation.Mechanics.Controllers;
using Scenes.Gameplay.Feature.LevelCreation.Mechanics.Factories;
using Scenes.Gameplay.Feature.LevelCreation.Saves;
using Scenes.Gameplay.Feature.LevelCreation.Services;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class LevelCreationInstaller : MonoInstaller
	{
		[Header("Level Creation")]
		[SerializeField] private LevelConfig levelConfig;
		[SerializeField] private TextAsset defaultLevelInfo;
		[SerializeField][SerializeReference] private List<ILevelMechanics> levelMechanics;
		[SerializeField] private BlocksDictionary blocksDictionary;
		[SerializeField] private BonusesDatabase bonusesDatabase;
		[SerializeField] private Block blockTemplate;
		[SerializeField] private Transform container;

		public override void InstallBindings()
		{
			BindLevelMechanicsFactory();
			BindLevelMechanicsController();


			BindLevelInfoProvider();
			BindBlockFactory();
			BindLevelGenerator();
			BindLevelService();
			Container.Bind<ILevelSavingService>().To<LevelSavingService>().AsSingle();
		}

		private void BindLevelMechanicsFactory()
		{
			Container.Bind<ILevelMechanicsFactory>()
				.To<LevelMechanicsFactory>()
				.AsSingle();
		}

		private void BindLevelMechanicsController()
		{
			Container.Bind<ILevelMechanicsController>()
				.To<LevelMechanicsController>()
				.AsSingle();
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

		private void BindLevelGenerator()
		{
			Container.Bind<ILevelGenerator>()
				.To<LevelGenerator>()
				.AsSingle()
				.WithArguments(levelConfig, bonusesDatabase);
		}

		private void BindLevelService()
		{
			Container.Bind<ILevelService>()
				.To<LevelService>()
				.AsSingle()
				.WithArguments(defaultLevelInfo, levelMechanics);
		}
	}
}
