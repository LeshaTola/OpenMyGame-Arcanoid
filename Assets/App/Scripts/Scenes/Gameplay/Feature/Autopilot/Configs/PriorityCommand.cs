using Scenes.Gameplay.Feature.Bonuses.Configs;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Autopilot.Configs
{
	[Serializable]
	public class PriorityCommand
	{
		[ValueDropdown(nameof(GetValues))]
		[SerializeField] public string BonusId;
		public int Priority;

		private BonusesDatabase bonusesDatabase;

		public void SetBonusesDatabase(BonusesDatabase db)
		{
			bonusesDatabase = db;
		}

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
