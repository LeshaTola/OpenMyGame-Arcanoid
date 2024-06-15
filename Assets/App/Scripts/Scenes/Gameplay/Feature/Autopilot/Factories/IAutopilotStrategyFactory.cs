using Scenes.Gameplay.Feature.Autopilot.Strategies;

namespace Scenes.Gameplay.Feature.Autopilot.Factories
{
	public interface IAutopilotStrategyFactory
	{
		IAutopilotStrategy GetAutopilotStrategy(string id);
	}
}