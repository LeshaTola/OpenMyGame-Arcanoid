using Cysharp.Threading.Tasks;
using Features.Saves;
using Features.Saves.Gameplay.DTOs.Level;
using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using Scenes.Gameplay.Feature.LevelCreation.LevelInfoProviders;
using Scenes.Gameplay.Feature.LevelCreation.Mechanics;
using Scenes.Gameplay.Feature.LevelCreation.Mechanics.Controllers;
using Scenes.PackSelection.Feature.Packs.Configs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
		private List<ILevelMechanics> levelMechanics;

		private LevelInfo levelInfo;

		public LevelService(ILevelGenerator levelGenerator,
					  ILevelInfoProvider levelInfoProvider,
					  ILevelMechanicsController levelMechanicsController,
					  TextAsset defaultLevelInfo,
					  List<ILevelMechanics> levelMechanics)
		{
			this.levelGenerator = levelGenerator;
			this.levelInfoProvider = levelInfoProvider;
			this.levelMechanicsController = levelMechanicsController;
			this.defaultLevelInfo = defaultLevelInfo;
			this.levelMechanics = levelMechanics;
		}

		public List<Block> Blocks { get => levelGenerator.Blocks.Values.ToList(); }

		public async Task SetupLevelFromPackAsync(Pack currentPack, SavedPackData savedPackData)
		{
			levelMechanicsController.Cleanup();
			var currentLevel = currentPack.LevelSettings[savedPackData.CurrentLevel];
			string path = Path.Combine(currentPack.RelativeLevelsPath, currentLevel.LevelName);
			levelInfo = levelInfoProvider.GetLevelInfoByPath(path);
			await levelGenerator.GenerateLevelAsync(levelInfo);

			levelMechanicsController.SetupLevelMechanics(currentLevel.LevelMechanics);
		}

		public async UniTask SetupDefaultLevelAsync()
		{
			levelMechanicsController.Cleanup();
			levelInfo = levelInfoProvider.GetLevelInfo(defaultLevelInfo.text);
			await levelGenerator.GenerateLevelAsync(levelInfo);
			levelMechanicsController.SetupLevelMechanics(levelMechanics);
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
				BlocksData = GetBlocksData(),
				levelMechanicsControllerState = levelMechanicsController.GetState()
			};
		}

		public async UniTask SetLevelStateAsync(LevelState levelState)
		{
			levelMechanicsController.SetState(levelState.levelMechanicsControllerState);

			levelInfo = levelState.levelInfo;
			await levelGenerator.GenerateLevelAsync(levelInfo);
			foreach (var blockData in levelState.BlocksData)
			{
				Vector2Int key = new(blockData.Position.X, blockData.Position.Y);
				if (levelGenerator.Blocks[key].Config.TryGetComponent(out HealthComponent healthComponent))
				{
					healthComponent.SetHealth(blockData.Health);
				}
			}
		}

		private List<BlockData> GetBlocksData()
		{
			List<BlockData> blocksData = new();
			foreach (var key in levelGenerator.Blocks.Keys)
			{
				if (levelGenerator.Blocks[key].Config.TryGetComponent(out HealthComponent healthComponent))
				{
					blocksData.Add(new()
					{
						Position = new(key),
						Health = healthComponent.Health
					});
				}
			}
			return blocksData;
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
