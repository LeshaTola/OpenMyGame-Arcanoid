using Features.FileProvider;
using Features.Saves;
using Features.StateMachine.States;
using Features.UI.SceneTransitions;
using Scenes.Gameplay.Feature.Health;
using Scenes.Gameplay.Feature.LevelCreation;
using Scenes.Gameplay.Feature.LevelCreation.LevelInfoProviders;
using Scenes.Gameplay.Feature.UI;
using Scenes.PackSelection.Feature.Packs;
using Scenes.PackSelection.Feature.Packs.Configs;
using System.IO;
using UnityEngine;

namespace Scenes.Gameplay.StateMachine.States
{
	public class InitialState : State
	{
		private IHealthController healthController;
		private ISceneTransition sceneTransition;
		private TextAsset defaultLevelInfo;
		private ILevelGenerator levelGenerator;
		private IPackProvider packProvider;
		private IFileProvider fileProvider;
		private ILevelInfoProvider levelInfoProvider;
		private IPackInfoUI packInfoUI;

		private bool firstPlay = true;

		public InitialState(IHealthController healthController,
					  ISceneTransition sceneTransition,
					  TextAsset defaultLevelInfo,
					  ILevelGenerator levelGenerator,
					  IPackProvider packProvider,
					  IFileProvider fileProvider,
					  ILevelInfoProvider levelInfoProvider,
					  IPackInfoUI packInfoUI)
		{
			this.healthController = healthController;
			this.sceneTransition = sceneTransition;
			this.defaultLevelInfo = defaultLevelInfo;
			this.levelGenerator = levelGenerator;
			this.packProvider = packProvider;
			this.fileProvider = fileProvider;
			this.levelInfoProvider = levelInfoProvider;
			this.packInfoUI = packInfoUI;
		}

		public override void Enter()
		{
			base.Enter();
			SetupLevel();
			healthController.ResetHealth();

			StateMachine.ChangeState<ResetState>();
		}

		public override void Exit()
		{
			base.Exit();
			if (firstPlay)
			{
				sceneTransition.PlayOff();
				firstPlay = false;
			}
		}

		private void SetupLevel()
		{
			var currentPack = packProvider.CurrentPack;
			SavedPackData savedPackData = packProvider.SavedPackData;
			if (currentPack == null || savedPackData == null)
			{
				levelGenerator.GenerateLevel(levelInfoProvider.GetLevelInfo(defaultLevelInfo.text));
				return;
			}

			string path = Path.Combine(currentPack.RelativeLevelsPath, currentPack.LevelNames[savedPackData.CurrentLevel]);
			GenerateLevel(path);
			SetupUi(currentPack, savedPackData);
		}

		private void SetupUi(Pack currentPack, SavedPackData savedPackData)
		{
			packInfoUI.Init(currentPack.Sprite, savedPackData.CurrentLevel, currentPack.MaxLevel);
		}

		private void GenerateLevel(string path)
		{
			TextAsset levelFile = fileProvider.GetTextAsset(path);
			levelGenerator.GenerateLevel(levelInfoProvider.GetLevelInfo(levelFile.text));
		}
	}
}
