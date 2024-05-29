using Newtonsoft.Json;
using Scenes.Gameplay.Feature.Bonuses.Configs;
using Scenes.Gameplay.Feature.LevelCreation;
using Scenes.Gameplay.Feature.LevelCreation.Configs;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Scenes.Gameplay.Features.LevelCreation.Editor
{
	public class LevelCreationEditorWindow : OdinEditorWindow
	{
		#region Create/Size
		[HorizontalGroup("Create/Size")]
		[SerializeField] private int height;

		[HorizontalGroup("Create/Size")]
		[SerializeField] private int width;

		[Space(8)]
		[SerializeField] private BlocksDictionary blocksDictionary;
		[SerializeField] private BonusesDatabase bonusesDatabase;

		[VerticalGroup("Create")]
		[ShowIf("@blocksDictionary != null && bonusesDatabase != null")]
		[Button(ButtonSizes.Small)]
		public void CreateMatrix()
		{
			blocksMatrix = new int[width, height];
			bonusesMatrix = new string[width, height];
		}
		#endregion

		#region Matrix

		[FoldoutGroup("Blocks")]
		[ValueDropdown(nameof(GetIds))]
		[SerializeField] private int value;

		[FoldoutGroup("Blocks")]
		[ShowIf("@blocksMatrix != null")]
		[TableMatrix(DrawElementMethod = nameof(DrawCustomElement))]
		[SerializeField] private int[,] blocksMatrix;

		private int DrawCustomElement(Rect rect, int value)
		{
			if (Event.current.button == 0 && (Event.current.type == EventType.MouseDrag || Event.current.type == EventType.MouseDown)
				&& rect.Contains(Event.current.mousePosition))
			{
				value = this.value;
				GUI.changed = true;
				Event.current.Use();
			}


			if (Event.current.button == 1 && (Event.current.type == EventType.MouseDrag || Event.current.type == EventType.MouseDown)
				&& rect.Contains(Event.current.mousePosition))
			{
				value = -1;
				GUI.changed = true;
				Event.current.Use();
			}

			Color cellColor;
			if (blocksDictionary.Blocks.ContainsKey(value))
			{
				cellColor = blocksDictionary.Blocks[value].Color;
				EditorGUI.DrawRect(rect.Padding(1), cellColor);
			}

			return value;
		}

		public List<int> GetIds()
		{
			if (blocksDictionary == null)
			{
				return null;
			}

			var ids = new List<int>(blocksDictionary.Blocks.Keys);
			ids.Sort();
			return ids;
		}
		#endregion

		#region BonusMatrix

		[FoldoutGroup("Bonuses")]
		[ValueDropdown(nameof(GetBonusesId))]
		[SerializeField] private string bonusValue = "";

		[FoldoutGroup("Bonuses")]
		[ShowIf("@bonusesMatrix != null")]
		[TableMatrix(DrawElementMethod = nameof(DrawCustomBonusElement))]
		[SerializeField] private string[,] bonusesMatrix;

		private string DrawCustomBonusElement(Rect rect, string value)
		{
			if (Event.current.button == 0 && (Event.current.type == EventType.MouseDrag || Event.current.type == EventType.MouseDown)
				&& rect.Contains(Event.current.mousePosition))
			{
				value = bonusValue;
				GUI.changed = true;
				Event.current.Use();
			}

			if (Event.current.button == 1 && (Event.current.type == EventType.MouseDrag || Event.current.type == EventType.MouseDown)
				&& rect.Contains(Event.current.mousePosition))
			{
				value = "none";
				GUI.changed = true;
				Event.current.Use();
			}

			if (value != null && bonusesDatabase.Bonuses.ContainsKey(value))
			{
				GUI.DrawTexture(rect, bonusesDatabase.Bonuses[value].BlockSprite?.texture);
			}
			return value;
		}

		private List<string> GetBonusesId()
		{
			if (bonusesDatabase == null)
			{
				return null;
			}

			return new List<string>(bonusesDatabase.Bonuses.Keys);
		}
		#endregion

		#region SaveLoad
		[Space(8)]
		[SerializeField] private TextAsset file;
		[HorizontalGroup("SaveLoad")]
		[Button(ButtonSizes.Small)]
		public void Save()
		{
			var path = AssetDatabase.GetAssetPath(file);

			LevelInfo levelInfo = new LevelInfo()
			{
				Height = height,
				Width = width,
				BlocksMatrix = blocksMatrix,
				BonusesMatrix = bonusesMatrix
			};

			string json = JsonConvert.SerializeObject(levelInfo, Formatting.Indented);
			File.WriteAllText(path, json);
			AssetDatabase.Refresh();
		}

		[HorizontalGroup("SaveLoad")]
		[ShowIf("@blocksDictionary != null && bonusesDatabase != null")]
		[Button(ButtonSizes.Small)]
		public void Load()
		{
			var path = AssetDatabase.GetAssetPath(file);
			string json = File.ReadAllText(path);
			LevelInfo levelInfo = JsonConvert.DeserializeObject<LevelInfo>(json);

			height = levelInfo.Height;
			width = levelInfo.Width;
			blocksMatrix = levelInfo.BlocksMatrix;
			bonusesMatrix = levelInfo.BonusesMatrix;
		}
		#endregion

		[MenuItem("My Tools/Level Creation")]
		private static void OpenWindow()
		{
			GetWindow<LevelCreationEditorWindow>().Show();
		}
	}
}