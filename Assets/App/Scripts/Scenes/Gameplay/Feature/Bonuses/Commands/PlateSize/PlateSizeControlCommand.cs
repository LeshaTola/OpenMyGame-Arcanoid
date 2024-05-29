﻿using Scenes.Gameplay.Feature.Player;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.PlateSize
{
	public class PlateSizeControlCommand : BonusCommand
	{
		[SerializeField, Min(0)] private float multiplier;

		private Plate plate;

		public PlateSizeControlCommand(Plate plate)
		{
			this.plate = plate;
		}

		public override void Clone(IBonusCommand command)
		{
			base.Clone(command);

			PlateSizeControlCommand concreteCommand = (PlateSizeControlCommand)command;
			multiplier = concreteCommand.multiplier;
		}

		public override void StartBonus()
		{
			base.StartBonus();
			plate.ChangeWidth(multiplier);
		}

		public override void StopBonus()
		{
			plate.ResetWidth();
		}
	}
}