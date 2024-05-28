using Scenes.Gameplay.Feature.Bonuses.Commands;

namespace Scenes.Gameplay.Feature.Bonuses.Factories
{
	public interface IBonusCommandsFactory
	{
		IBonusCommand GetBonusCommand(string bonusId);
	}
}
