using Scenes.Gameplay.Feature.Autopilot.Strategies;
using Scenes.Gameplay.Feature.Bonuses.Configs;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Autopilot.Configs
{
	[CreateAssetMenu(fileName = "AutopilotBonusStrategyDatabase", menuName = "Dictionaries/Autopilot/Strategies")]
	public class AutopilotBonusStrategyDatabase : SerializedScriptableObject
	{
		[SerializeField] private BonusesDatabase bonusesDatabase;
		[ValueDropdown(nameof(GetValues))]
		[SerializeField] private string bonusId;

		[Button]
		public void Add()
		{
			Strategies.Add(bonusId, null);
		}

		[DictionaryDrawerSettings(KeyLabel = "Command Id", ValueLabel = "Strategy", IsReadOnly = true)]
		[SerializeField] private Dictionary<string, IAutopilotStrategy> strategies;

		public Dictionary<string, IAutopilotStrategy> Strategies { get => strategies; }

		private IEnumerable<string> GetValues()
		{
			if (bonusesDatabase == null)
			{
				return null;
			}
			return new List<string>(bonusesDatabase.Bonuses.Keys);
		}
	}
}
