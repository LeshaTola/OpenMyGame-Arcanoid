using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.PackSelection.Feature.Packs.Configs
{
	[CreateAssetMenu(fileName = "Pack", menuName = "Configs/Pack")]
	public class Pack : ScriptableObject
	{
		[SerializeField] private Sprite sprite;
		[SerializeField] private string packName;
		[SerializeField] private string relativeLevelsPath;

		[FoldoutGroup("Level")]
		[SerializeField] private int currentLevel;
		[FoldoutGroup("Level")]
		[InlineProperty]
		[SerializeField] private List<string> levelNames;
		[SerializeField] private bool isOpened;

		public Sprite Sprite { get => sprite; }
		public string Name { get => packName; }
		public string RelativeLevelsPath { get => relativeLevelsPath; }
		public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
		public int MaxLevel { get => levelNames.Count - 1; }
		public List<string> LevelNames { get => levelNames; }
		public bool IsOpened { get => isOpened; set => isOpened = value; }
	}
}