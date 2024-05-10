using App.Scripts.Module.MinMaxValue;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.Blocks.Config
{
	[CreateAssetMenu(fileName = "CracksDictionary", menuName = "Dictionaries/Cracks")]
	public class CracksDictionary : SerializedScriptableObject
	{
		[DictionaryDrawerSettings(KeyLabel = "Health", ValueLabel = "Crack")]
		[SerializeField] private Dictionary<MinMaxInt, Sprite> cracks;

		public Dictionary<MinMaxInt, Sprite> Cracks { get => cracks; }
	}
}
