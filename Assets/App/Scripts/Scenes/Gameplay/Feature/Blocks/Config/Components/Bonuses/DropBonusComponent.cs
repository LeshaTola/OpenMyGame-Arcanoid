using Scenes.Gameplay.Feature.Blocks.Config.Components.General;
using Scenes.Gameplay.Feature.Bonuses.Configs;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Score
{
	public class DropBonusComponent : Component
	{
		[UnityEngine.SerializeField] private BonusesDatabase bonusesDatabase;

		[ValueDropdown(@"GetBonusesIds")]
		[UnityEngine.SerializeField] private string bonusId;

		public DropBonusComponent()
		{
		}

		public DropBonusComponent(string bonusId)
		{
			this.bonusId = bonusId;
		}

		public string BonusId { get => bonusId; }

		private List<string> GetBonusesIds()
		{
			if (bonusesDatabase == null)
			{
				return null;
			}

			return new List<string>(bonusesDatabase.Bonuses.Keys);
		}
	}
}
