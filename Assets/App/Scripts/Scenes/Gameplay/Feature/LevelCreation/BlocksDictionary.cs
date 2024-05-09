using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.LevelCreation
{
	[CreateAssetMenu(fileName = "BlocksDictionary", menuName = "Dictionaries/Blocks")]
	public class BlocksDictionary : SerializedScriptableObject
	{
		[SerializeField] private Dictionary<int, Color> blocks = new();

		public Dictionary<int, Color> Blocks { get => blocks; }
	}
}
