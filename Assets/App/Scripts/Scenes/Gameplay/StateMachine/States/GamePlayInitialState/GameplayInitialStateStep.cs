using Features.FileProvider;
using Features.StateMachine.States.General;
using Scenes.Gameplay.Feature.LevelCreation;
using Scenes.Gameplay.Feature.LevelCreation.LevelInfoProviders;
using Scenes.Gameplay.Feature.UI;
using Scenes.PackSelection.Feature.Packs;
using System.IO;
using UnityEngine;

namespace Scenes.Gameplay.StateMachine.States.GamePlayInitialState
{
	public class GameplayInitialStateStep : StateStep
	{
		private TextAsset defaultLevelInfo;
		private LevelGenerator levelGenerator;
		private IPackProvider packProvider;
		private IFileProvider fileProvider;
		private ILevelInfoProvider levelInfoProvider;
		private PackInfoUI packInfoUI;

		public GameplayInitialStateStep(TextAsset defaultLevelInfo,
								  LevelGenerator levelGenerator,
								  IPackProvider packProvider,
								  IFileProvider fileProvider,
								  ILevelInfoProvider levelInfoProvider,
								  PackInfoUI packInfoUI)
		{
			this.defaultLevelInfo = defaultLevelInfo;
			this.levelGenerator = levelGenerator;
			this.packProvider = packProvider;
			this.fileProvider = fileProvider;
			this.levelInfoProvider = levelInfoProvider;
			this.packInfoUI = packInfoUI;
		}

		public override void Enter()
		{
			var currentPack = packProvider.CurrentPack;
			if (currentPack == null)
			{
				levelGenerator.GenerateLevel(levelInfoProvider.GetLevelInfo(defaultLevelInfo.text));
				return;
			}

			string path = Path.Combine(currentPack.RelativeLevelsPath, currentPack.LevelNames[currentPack.CurrentLevel]);
			TextAsset levelFile = fileProvider.GetTextAsset(path);
			levelGenerator.GenerateLevel(levelInfoProvider.GetLevelInfo(levelFile.text));

			packInfoUI.Init(currentPack.Sprite, currentPack.CurrentLevel, currentPack.MaxLevel);
		}
	}
}
