using Scenes.Gameplay.Feature.Bonuses.Commands;

namespace Scenes.Gameplay.Feature.Bonuses.Services
{
	public interface IBonusService
	{
		void StartBonus(IBonusCommand bonusCommand);
		void UpdateBonus();
		void StopBonus(IBonusCommand bonusCommand);
		void Cleanup();
	}
}