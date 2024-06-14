using Scenes.Gameplay.Feature.Bonuses.Commands;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Configs
{
	[CreateAssetMenu(fileName = "BonusesDatabase", menuName = "Dictionaries/Bonuses")]
	public class BonusesDatabase : SerializedScriptableObject
	{

		[DictionaryDrawerSettings(KeyLabel = "Bonus Id", ValueLabel = "Bonus Command")]
		[SerializeField] private Dictionary<string, IBonusCommand> bonuses;

		public Dictionary<string, IBonusCommand> Bonuses { get => bonuses; }
	}
}
