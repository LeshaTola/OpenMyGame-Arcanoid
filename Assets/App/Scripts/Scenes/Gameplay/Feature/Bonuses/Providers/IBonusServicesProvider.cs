using Features.Saves.Gameplay.DTO.Bonuses;

namespace Scenes.Gameplay.Feature.Bonuses.Provider
{
	public interface IBonusServicesProvider
	{
		BonusServiceState GetBonusServiceState();
		void SetBonusServiceState(BonusServiceState state);

		void Cleanup();
	}
}
