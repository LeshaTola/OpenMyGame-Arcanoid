using App.Scripts.Scenes.Gameplay.Feature.Blocks;
using App.Scripts.Scenes.Gameplay.Feature.Blocks.Config;
using App.Scripts.Scenes.Gameplay.Feature.Field;
using App.Scripts.Scenes.Gameplay.Feature.LevelCreation.Configs;
using Newtonsoft.Json;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.LevelCreation
{
	public class LevelGenerator : MonoBehaviour
	{
		[SerializeField] private FieldController fieldController;
		[SerializeField] private LevelConfig levelConfig;
		[SerializeField] private BlocksDictionary blocksDictionary;
		[SerializeField] private Block blockTemplate;

		public void GenerateLevel(LevelInfo levelInfo)
		{
			GameField gameField = fieldController.GetGameFieldRect();
			float blockWidth = GetBlockWidth(levelInfo, gameField);

			for (int i = 0; i < levelInfo.Height; i++)
			{
				for (int j = 0; j < levelInfo.Width; j++)
				{
					Block block = GetPreparedBlock(levelInfo, blockWidth, i, j);

					Vector2 newPosition = GetNewBlockPosition(gameField, i, j, block);
					block.transform.position = newPosition;
				}
			}
		}

		private Block GetPreparedBlock(LevelInfo levelInfo, float blockWidth, int i, int j)
		{
			Block block = Instantiate(blockTemplate, transform);

			BlockConfig blockConfig =blocksDictionary.Blocks[ levelInfo.BlocksMatrix[j, i]];
			block.Init(blockConfig);
			block.ResizeBlock(blockWidth);
			return block;
		}

		private Vector2 GetNewBlockPosition(GameField gameField, int i, int j, Block block)
		{
			float horizontalOffset = gameField.MinX + block.Width / 2 + (block.Width + levelConfig.Spacing) * j;
			float verticalOffset = gameField.MaxY - block.Height / 2 - (block.Height + levelConfig.Spacing) * i;
			return new Vector2(horizontalOffset, verticalOffset) ;
		}

		private float GetBlockWidth(LevelInfo levelInfo, GameField gameField)
		{
			return gameField.Width / levelInfo.Width - levelConfig.Spacing;
		}
	}
}