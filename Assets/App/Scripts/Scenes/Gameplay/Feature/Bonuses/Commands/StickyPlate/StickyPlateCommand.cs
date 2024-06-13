using Scenes.Gameplay.Feature.Player;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.StickyPlate
{
	public class StickyPlateCommand : BonusCommand
	{
		[SerializeField] private BonusConfig config;

		private Plate plate;

		public StickyPlateCommand(Plate plate, BonusConfig config)
		{
			this.plate = plate;
			this.config = config;
		}

		public override BonusConfig Config => config;

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
