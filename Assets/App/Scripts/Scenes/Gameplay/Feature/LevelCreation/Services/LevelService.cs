using Cysharp.Threading.Tasks;
using Features.Saves;
using Features.Saves.Gameplay.DTOs.Level;
using Scenes.Gameplay.Feature.LevelCreation.LevelInfoProviders;
using Scenes.Gameplay.Feature.LevelCreation.Mechanics;
using Scenes.Gameplay.Feature.LevelCreation.Mechanics.Controllers;
using Scenes.PackSelection.Feature.Packs.Configs;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Scenes.Gameplay.Feature.LevelCreation.Services
{
	public class LevelService : ILevelService
	{
		private ILevelGenerator levelGenerator;
		private ILevelInfoProvider levelInfoProvider;
		private ILevelMechanicsController levelMechanicsController;
		private TextAsset defaultLevelInfo;
		private List<LevelMechanics> levelMechanics;

		public LevelService(ILevelGenerator levelGenerator,
					  ILevelInfoProvider levelInfoProvider,
					  ILevelMechanicsController levelMechanicsController,
					  TextAsset defaultLevelInfo,
					  List<LevelMechanics> levelMechanics)
		{
			this.levelGenerator = levelGenerator;
			this.levelInfoProvider = levelInfoProvider;
			this.levelMechanicsController = levelMechanicsController;
			this.defaultLevelInfo = defaultLevelInfo;
			this.levelMechanics = levelMechanics;
		}

		public async UniTask LoadLevelAsync(LevelState levelState)
		{

		}

		public async Task SetupLevelFromPackAsync(Pack currentPack, SavedPackData savedPackData)
		{
			levelMechanicsController.Cleanup();
			var currentLevel = currentPack.LevelSettings[savedPackData.CurrentLevel];
			string path = Path.Combine(currentPack.RelativeLevelsPath, currentLevel.LevelName);
			await levelGenerator.GenerateLevelAsync(levelInfoProvider.GetLevelInfoByPath(path));

			levelMechanicsController.StartLevelMechanics(currentLevel.LevelMechanics);
		}

		public async UniTask SetupDefaultLevelAsync()
		{
			levelMechanicsController.Cleanup();
			await levelGenerator.GenerateLevelAsync(levelInfoProvider.GetLevelInfo(defaultLevelInfo.text));
			levelMechanicsController.StartLevelMechanics(levelMechanics);
		}
	}
}
