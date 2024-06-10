using Cysharp.Threading.Tasks;
using Features.Saves;
using Features.Saves.Gameplay.DTOs.Level;
using Module.Saves.Structs;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
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

		private LevelInfo levelInfo;

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

		public async Task SetupLevelFromPackAsync(Pack currentPack, SavedPackData savedPackData)
		{
			levelMechanicsController.Cleanup();
			var currentLevel = currentPack.LevelSettings[savedPackData.CurrentLevel];
			string path = Path.Combine(currentPack.RelativeLevelsPath, currentLevel.LevelName);
			levelInfo = levelInfoProvider.GetLevelInfoByPath(path);
			await levelGenerator.GenerateLevelAsync(levelInfo);

			levelMechanicsController.StartLevelMechanics(currentLevel.LevelMechanics);
		}

		public async UniTask SetupDefaultLevelAsync()
		{
			levelMechanicsController.Cleanup();
			levelInfo = levelInfoProvider.GetLevelInfo(defaultLevelInfo.text);
			await levelGenerator.GenerateLevelAsync(levelInfo);
			levelMechanicsController.StartLevelMechanics(levelMechanics);
		}

		public void TurnOffColliders()
		{
			foreach (var block in levelGenerator.Blocks.Values)
			{
				block.BoxCollider.isTrigger = true;
			}
		}

		public void TurnOnColliders()
		{
			foreach (var block in levelGenerator.Blocks.Values)
			{
				block.BoxCollider.isTrigger = false;
			}
		}

		public LevelState GetLevelState()
		{
			return new LevelState()
			{
				levelInfo = new LevelInfo()
				{
					Height = levelInfo.Height,
					Width = levelInfo.Width,
					BlocksMatrix = GetBlocksMatrix(),
					BonusesMatrix = levelInfo.BonusesMatrix,
				},
				//blockHealth = GetBlocksHealth()
			};
		}

		public async UniTask SetLevelStateAsync(LevelState levelState)
		{
			levelInfo = levelState.levelInfo;
			await levelGenerator.GenerateLevelAsync(levelInfo);
		}

		private Dictionary<JsonVector2, int> GetBlocksHealth()
		{
			Dictionary<JsonVector2, int> blocksHealth = new();
			foreach (var key in levelGenerator.Blocks.Keys)
			{
				if (levelGenerator.Blocks[key].Config.TryGetComponent(out HealthComponent healthComponent))
				{
					blocksHealth.Add(new(key), healthComponent.Health);
				}
			}
			return blocksHealth;
		}

		private int[,] GetBlocksMatrix()
		{
			int[,] matrix = new int[levelInfo.Width, levelInfo.Height];

			for (int i = 0; i < levelInfo.Width; i++)
			{
				for (int j = 0; j < levelInfo.Height; j++)
				{
					matrix[i, j] = -1;
				}
			}

			foreach (var matrixPosition in levelGenerator.Blocks.Keys)
			{
				matrix[matrixPosition.x, matrixPosition.y] = levelInfo.BlocksMatrix[matrixPosition.x, matrixPosition.y];
			}

			return matrix;
		}
	}
}
