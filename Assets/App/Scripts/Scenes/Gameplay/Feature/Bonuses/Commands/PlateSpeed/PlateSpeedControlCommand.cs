using Scenes.Gameplay.Feature.Player;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.PlateSize
{
	public class PlateSpeedControlCommand : BonusCommand
	{
		[SerializeField, Min(0)] private float multiplier;

		private Plate plate;
		private float prevMultiplier;

		public PlateSpeedControlCommand(Plate plate)
		{
			this.plate = plate;
		}

		public override void Clone(IBonusCommand command)
		{
			base.Clone(command);

			var concreteCommand = (PlateSpeedControlCommand)command;
			multiplier = concreteCommand.multiplier;
		}

		public override void StartBonus()
		{
			base.StartBonus();
			prevMultiplier = plate.SpeedMultiplier;
			plate.SpeedMultiplier = multiplier;
		}

		public override void StopBonus()
		{
			plate.SpeedMultiplier = prevMultiplier;
		}
	}
}
