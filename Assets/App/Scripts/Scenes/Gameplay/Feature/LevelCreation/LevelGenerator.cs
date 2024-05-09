using App.Scripts.Scenes.Gameplay.Feature.Blocks;
using App.Scripts.Scenes.Gameplay.Feature.Field;
using Newtonsoft.Json;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.LevelCreation
{
	public class LevelGenerator : MonoBehaviour
	{
		[SerializeField] private TextAsset fileLevelInfo;// TODO Move it to separate reader
		[SerializeField] private FieldController fieldController;
		[SerializeField] private BlocksDictionary blocksDictionary;
		//[SerializeField] private LevelConfig levelConfig;
		[SerializeField] private float spacing;
		[SerializeField] private Block blockTemplate;

		private void Awake()
		{
			CreateLevel(GetLevelInfo());
		}

		public void CreateLevel(LevelInfo levelInfo)
		{
			GameField gameField = fieldController.GetGameFieldRect();
			float blockWidth = gameField.Width / levelInfo.Width - spacing;

			for (int i = 0; i < levelInfo.Height; i++)
			{
				for (int j = 0; j < levelInfo.Width; j++)
				{
					Block block = Instantiate(blockTemplate, transform);
					block.ResizeBlock(blockWidth);
					block.transform.position =
						new Vector2(
							gameField.MinX + block.Width / 2 + (block.Width + spacing) * j,
							gameField.MaxY - block.Height / 2 - (block.Height + spacing) * i);
				}
			}

		}

		private LevelInfo GetLevelInfo()
		{
			string json = fileLevelInfo.text;
			LevelInfo levelInfo = JsonConvert.DeserializeObject<LevelInfo>(json);
			return levelInfo;
		}

	}
}