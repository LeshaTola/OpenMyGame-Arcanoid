using Scenes.Gameplay.Feature.Player.Ball.Services;
using System;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.BallSpeed
{
	[Serializable]
	public class BallsSpeedControlBonusConfig : BonusConfig
	{
		[SerializeField] private float multiplier;

		public float Multiplier { get => multiplier; }
	}

	public class SpeedControlBallsCommand : BonusCommand
	{
		[SerializeField] private BallsSpeedControlBonusConfig config;

		private IBallService ballService;
		private float prevSpeedMultiplier;

		public SpeedControlBallsCommand(IBallService ballService, BallsSpeedControlBonusConfig config)
		{
			this.ballService = ballService;
			this.config = config;
		}

		public override BonusConfig Config => config;

		public override void StartBonus()
		{
			base.StartBonus();
			prevSpeedMultiplier = ballService.SpeedMultiplier;
			ballService.ChangeBallsSpeed(config.Multiplier);
		}

		public override void StopBonus()
		{
			ballService.ChangeBallsSpeed(prevSpeedMultiplier);
		}
	}
}
