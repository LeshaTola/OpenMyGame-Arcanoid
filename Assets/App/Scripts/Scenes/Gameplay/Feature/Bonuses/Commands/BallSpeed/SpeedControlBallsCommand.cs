using Scenes.Gameplay.Feature.Player.Ball.Services;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.BallSpeed
{
	public class SpeedControlBallsCommand : BonusCommand
	{
		[SerializeField] private float multiplier;

		private IBallService ballService;
		private float prevSpeedMultiplier;

		public SpeedControlBallsCommand(IBallService ballService)
		{
			this.ballService = ballService;
		}

		public override void Clone(IBonusCommand command)
		{
			base.Clone(command);

			SpeedControlBallsCommand concreteCommand = ((SpeedControlBallsCommand)command);
			multiplier = concreteCommand.multiplier;
		}

		public override void StartBonus()
		{
			base.StartBonus();
			prevSpeedMultiplier = ballService.SpeedMultiplier;
			ballService.ChangeBallsSpeed(multiplier);
		}

		public override void StopBonus()
		{
			ballService.ChangeBallsSpeed(prevSpeedMultiplier);
		}
	}
}
