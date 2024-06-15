using Scenes.Gameplay.Feature.Player;

namespace Scenes.Gameplay.Feature.Autopilot.Strategies
{
	public class StickyPlatformStrategy : IAutopilotStrategy
	{
		private Plate plate;

		public StickyPlatformStrategy(Plate plate)
		{
			this.plate = plate;
		}

		public void Execute()
		{
			plate.PushBalls();
		}
	}
}
