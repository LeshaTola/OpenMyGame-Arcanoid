using Newtonsoft.Json;
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

		[VerticalGroup("Create")]
		[Button(ButtonSizes.Small)]
		public void CreateMatrix()
		{
			blocksMatrix = new int[width, height];
		}
		#endregion

		#region Matrix
		[Space(8)]
		[SerializeField] private BlocksDictionary blocksDictionary;

		[ValueDropdown(nameof(GetIds))]
		[SerializeField] private int value;

		[ShowIf("@ blocksMatrix != null")]
		[TableMatrix(DrawElementMethod = nameof(DrawCustomElement))]
		[SerializeField] private int[,] blocksMatrix;

		private int DrawCustomElement(Rect rect, int value)
		{
			if (Event.current.type == EventType.MouseDrag
				&& rect.Contains(Event.current.mousePosition))
			{
				value = this.value;
				GUI.changed = true;
				Event.current.Use();
			}

			Color cellColor = Color.black;
			if (blocksDictionary.Blocks.ContainsKey(value))
			{
				cellColor = blocksDictionary.Blocks[value].Color;
			}

			EditorGUI.DrawRect(rect.Padding(1), cellColor);

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
			};

			string json = JsonConvert.SerializeObject(levelInfo, Formatting.Indented);
			File.WriteAllText(path, json);
			AssetDatabase.Refresh();
		}

		[HorizontalGroup("SaveLoad")]
		[Button(ButtonSizes.Small)]
		public void Load()
		{
			var path = AssetDatabase.GetAssetPath(file);
			string json = File.ReadAllText(path);
			LevelInfo levelInfo = JsonConvert.DeserializeObject<LevelInfo>(json);

			height = levelInfo.Height;
			width = levelInfo.Width;
			blocksMatrix = levelInfo.BlocksMatrix;
		}
		#endregion

		[MenuItem("My Tools/Level Creation")]
		private static void OpenWindow()
		{
			GetWindow<LevelCreationEditorWindow>().Show();
		}
	}
}