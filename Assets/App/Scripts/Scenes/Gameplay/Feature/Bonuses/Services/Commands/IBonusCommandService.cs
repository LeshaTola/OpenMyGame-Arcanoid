using Features.Saves.Gameplay.DTO.Bonuses;
using Scenes.Gameplay.Feature.Bonuses.Commands;
using System;
using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.Bonuses.Services
{
	public interface IBonusCommandService
	{
		List<IBonusCommand> BonusCommands { get; }

		event Action<IBonusCommand> OnBonusStart;
		event Action<IBonusCommand> OnBonusUpdate;
		event Action<IBonusCommand> OnBonusStop;

		void StartBonus(IBonusCommand bonusCommand);
		void UpdateBonus();
		void StopBonus(IBonusCommand bonusCommand);

		List<BonusCommandData> GetBonusesCommandsData();
		void SetBonusesCommandsData(List<BonusCommandData> bonusCommands);

		void Cleanup();
		IBonusCommand GetBonusCommand(string id);
	}
}