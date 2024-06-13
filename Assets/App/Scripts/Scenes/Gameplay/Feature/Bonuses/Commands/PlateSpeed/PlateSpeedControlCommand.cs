using Scenes.Gameplay.Feature.Player;
using System;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.PlateSize
{
	[Serializable]
	public class PlateSpeedControlBonusConfig : BonusConfig
	{
		[SerializeField, Min(0)] private float multiplier;

		public float Multiplier { get => multiplier; }
	}

	public class PlateSpeedControlCommand : BonusCommand
	{
		[SerializeField] private PlateSpeedControlBonusConfig config;

		private Plate plate;
		private float prevMultiplier;

		public PlateSpeedControlCommand(Plate plate,
								PlateSpeedControlBonusConfig config)
		{
			this.plate = plate;
			this.config = config;
		}

		public override BonusConfig Config => config;

		public override void StartBonus()
		{
			base.StartBonus();
			prevMultiplier = plate.SpeedMultiplier;
			plate.SpeedMultiplier = config.Multiplier;
		}

		public override void StopBonus()
		{
			plate.SpeedMultiplier = prevMultiplier;
		}
	}
}
