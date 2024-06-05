using Features.Saves.Gameplay;
using Scenes.Gameplay.Feature.Bonuses.Commands;
using System;
using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.Bonuses.Services
{
	public interface IBonusService
	{
		event Action<IBonusCommand> OnBonusStart;
		event Action<IBonusCommand> OnBonusUpdate;
		event Action<IBonusCommand> OnBonusStop;

		void StartBonus(IBonusCommand bonusCommand);
		void UpdateBonus();
		void StopBonus(IBonusCommand bonusCommand);
		void CleanupActiveBonuses();
		void CleanupFallingBonuses();
		IBonusCommand GetBonusCommand(string id);
		Bonus GetBonus(string id);
		List<ActiveBonus> GetActiveBonuses();
		List<BonusPosition> GetBonusesPositions();
	}
}