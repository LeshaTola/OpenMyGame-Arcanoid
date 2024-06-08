using Features.Saves.Gameplay.DTO.Bonuses;
using Scenes.Gameplay.Feature.Bonuses.Services;
using Scenes.Gameplay.Feature.Bonuses.Services.Bonuses;

namespace Scenes.Gameplay.Feature.Bonuses.Provider
{
	public class BonusServicesProvider : IBonusServicesProvider
	{
		private IBonusService bonusService;
		private IBonusCommandService bonusCommandService;

		public BonusServiceState GetBonusServiceState()
		{
			return new BonusServiceState()
			{
				BonusesCommandsData = bonusCommandService.GetBonusesCommandsData(),
				BonusData = bonusService.GetBonusesData(),
			};
		}

		public void SetBonusServiceState(BonusServiceState state)
		{
			bonusService.SetBonusData(state.BonusData);
			bonusCommandService.SetBonusesCommandsData(state.BonusesCommandsData);
		}

		public void Cleanup()
		{
			bonusService.Cleanup();
			bonusCommandService.Cleanup();
		}
	}
}
