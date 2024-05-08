using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Module.LevelCreation
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

		[ValueDropdown("@blocksDictionary.IDs")]
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
				cellColor = blocksDictionary.Blocks[value];
			}

			EditorGUI.DrawRect(rect.Padding(1), cellColor);

			return value;
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