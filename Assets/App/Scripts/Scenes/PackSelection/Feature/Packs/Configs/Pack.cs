using System.Collections.Generic;
using UnityEngine;

namespace Scenes.PackSelection.Feature.Packs.Configs
{
	[CreateAssetMenu(fileName = "Pack", menuName = "Configs/Pack")]
	public class Pack : ScriptableObject
	{
		[SerializeField] private string id;
		[SerializeField] private Sprite sprite;
		[SerializeField] private string packName;
		[SerializeField] private string relativeLevelsPath;
		[SerializeField] private List<string> levelNames;

		public string Id { get => id; }
		public Sprite Sprite { get => sprite; }
		public string Name { get => packName; }
		public string RelativeLevelsPath { get => relativeLevelsPath; }
		public int MaxLevel { get => levelNames.Count - 1; }
		public List<string> LevelNames { get => levelNames; }
	}
}