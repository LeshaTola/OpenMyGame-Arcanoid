using Features.FileProvider;
using Features.Saves;
using Scenes.Gameplay.Feature.LevelCreation.LevelInfoProviders;
using Scenes.Gameplay.Feature.LevelCreation.Mechanics;
using Scenes.Gameplay.Feature.LevelCreation.Mechanics.Controllers;
using Scenes.PackSelection.Feature.Packs;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Scenes.Gameplay.Feature.LevelCreation.Services
{
	public class LevelService : ILevelService
	{
		private ILevelGenerator levelGenerator;
		private IPackProvider packProvider;
		private IFileProvider fileProvider;
		private ILevelInfoProvider levelInfoProvider;
		private ILevelMechanicsController levelMechanicsController;
		private TextAsset defaultLevelInfo;
		private List<LevelMechanics> levelMechanics;

		public LevelService(ILevelGenerator levelGenerator,
					  IPackProvider packProvider,
					  IFileProvider fileProvider,
					  ILevelInfoProvider levelInfoProvider,
					  ILevelMechanicsController levelMechanicsController,
					  TextAsset defaultLevelInfo,
					  List<LevelMechanics> levelMechanics)
		{
			this.levelGenerator = levelGenerator;
			this.packProvider = packProvider;
			this.fileProvider = fileProvider;
			this.levelInfoProvider = levelInfoProvider;
			this.levelMechanicsController = levelMechanicsController;
			this.defaultLevelInfo = defaultLevelInfo;
			this.levelMechanics = levelMechanics;
		}

		public void SetupLevel()
		{
			var currentPack = packProvider.CurrentPack;
			SavedPackData savedPackData = packProvider.SavedPackData;
			if (currentPack == null || savedPackData == null)
			{
				levelGenerator.GenerateLevel(levelInfoProvider.GetLevelInfo(defaultLevelInfo.text));
				levelMechanicsController.CleanUp();
				levelMechanicsController.StartLevelMechanics(levelMechanics);
				return;
			}

			var currentLevel = currentPack.LevelSettings[savedPackData.CurrentLevel];
			levelMechanicsController.CleanUp();
			levelMechanicsController.StartLevelMechanics(currentLevel.LevelMechanics);

			string path = Path.Combine(currentPack.RelativeLevelsPath, currentLevel.LevelName);
			GenerateLevel(path);
		}

		private void GenerateLevel(string path)
		{
			TextAsset levelFile = fileProvider.GetTextAsset(path);
			levelGenerator.GenerateLevel(levelInfoProvider.GetLevelInfo(levelFile.text));
		}
	}
}
