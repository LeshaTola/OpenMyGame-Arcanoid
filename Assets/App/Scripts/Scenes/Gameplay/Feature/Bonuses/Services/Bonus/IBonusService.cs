using Features.Saves.Gameplay;
using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.Bonuses.Services.Bonuses
{
	public interface IBonusService
	{
		List<BonusPosition> GetBonusesData();
		void SetBonusData(List<BonusPosition> bonusPositions);

		void Cleanup();
	}
}
