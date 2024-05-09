using App.Scripts.Scenes.Gameplay.Feature.Blocks.Config;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.LevelCreation
{
	[CreateAssetMenu(fileName = "BlocksDictionary", menuName = "Dictionaries/Blocks")]
	public class BlocksDictionary : SerializedScriptableObject
	{
		[SerializeField] private Dictionary<int, BlockConfig> blocks = new();

		public Dictionary<int, BlockConfig> Blocks { get => blocks; }
	}
}
