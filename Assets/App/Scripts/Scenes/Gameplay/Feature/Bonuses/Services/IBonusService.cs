using Scenes.Gameplay.Feature.Bonuses.Commands;
using System;

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
		void Cleanup();
	}
}