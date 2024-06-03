using Scenes.Gameplay.Feature.Player;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.StickyPlate
{
	public class StickyPlateCommand : BonusCommand
	{
		private Plate plate;

		public StickyPlateCommand(Plate plate)
		{
			this.plate = plate;
		}

		public override void StartBonus()
		{
			base.StartBonus();
			plate.IsSticky = true;
		}

		public override void StopBonus()
		{
			base.StopBonus();
			plate.IsSticky = false;
			plate.PushBalls();
		}
	}
}
