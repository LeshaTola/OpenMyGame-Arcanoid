using Scenes.Gameplay.Feature.Bonuses.Commands;

namespace Scenes.Gameplay.Feature.Bonuses.UI
{
	public interface IBonusesUI
	{
		void AddTimer(IBonusCommand command, BonusTimerUI timer);
		BonusTimerUI RemoveTimer(IBonusCommand command);
		void UpdateTimer(IBonusCommand command);
	}
}