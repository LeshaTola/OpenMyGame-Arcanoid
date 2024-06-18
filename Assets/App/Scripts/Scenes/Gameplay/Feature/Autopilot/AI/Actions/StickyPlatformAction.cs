using Module.AI.Actions;
using Scenes.Gameplay.Feature.Player;
namespace Scenes.Gameplay.Feature.AI.Actions
{
	public class StickyPlatformAction : Action
	{
		private Plate plate;

		public StickyPlatformAction(Plate plate)
		{
			this.plate = plate;
		}

		public override void Execute()
		{
			plate.PushBalls();
		}
	}
}
