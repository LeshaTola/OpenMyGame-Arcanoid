using Scenes.Gameplay.Feature.Bonuses.UI;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Factories.BonusTimerFactories
{
	public class BonusTimerFactory : IBonusTimerFactory
	{
		private BonusTimerUI bonusTimerTemplate;

		public BonusTimerFactory(BonusTimerUI bonusTimerTemplate)
		{
			this.bonusTimerTemplate = bonusTimerTemplate;
		}

		public IBonusTimerUI GetBonusTimerUI()
		{
			return GameObject.Instantiate(bonusTimerTemplate);
		}
	}
}
