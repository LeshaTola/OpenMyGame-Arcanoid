using Features.StateMachine;
using Scenes.Gameplay.Feature.Autopilot.Configs;
using Scenes.Gameplay.Feature.Autopilot.Factories;
using Scenes.Gameplay.Feature.Autopilot.Services.Entities;
using Scenes.Gameplay.Feature.Bonuses;
using Scenes.Gameplay.Feature.Bonuses.Services;
using Scenes.Gameplay.Feature.Bonuses.Services.Bonuses;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.Feature.Player.Ball.Services;
using Scenes.Gameplay.Feature.Player.PlayerInput;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Autopilot.Services
{
	public class AutopilotService : IAutopilotService, IUpdatable
	{
		private AutopilotConfig config;

		private Plate plate;
		private IInput input;
		private IBallService ballService;
		private IBonusService bonusService;
		private IBonusCommandService commandService;
		private IAutopilotStrategyFactory autopilotStrategyFactory;

		private float plateYPosition;
		private float autopilotTargetLinePosition;

		public AutopilotService(Plate plate,
								IInput input,
								AutopilotConfig config,
								IBallService ballService,
								IBonusService bonusService,
								IBonusCommandService commandService,
								IAutopilotStrategyFactory autopilotStrategyFactory)
		{
			this.plate = plate;
			this.input = input;
			this.config = config;
			this.ballService = ballService;
			this.bonusService = bonusService;
			this.commandService = commandService;
			this.autopilotStrategyFactory = autopilotStrategyFactory;

			plateYPosition = plate.transform.position.y + config.ManeuverDistance;
			autopilotTargetLinePosition = plate.transform.position.y + config.AutopilotTargetDistance;
		}

		public List<AutopilotTarget> Targets { get; private set; }
		public bool IsActive { get; private set; } = false;
		public float HalfPlateWidth => plate.BoxCollider.size.x / 2;

		public void ActivateAutopilot()
		{
			IsActive = true;
			input.IsActive = false;
		}

		public void DeactivateAutopilot()
		{
			IsActive = false;
			input.IsActive = true;
		}

		public void Update()
		{
			if (IsActive)
			{
				foreach (var command in commandService.BonusCommands)
				{
					autopilotStrategyFactory.GetAutopilotStrategy(command.Id)?.Execute();
				}
				plate.Movement.Move(plate.transform.position + GetPriorityDirection());
			}
		}

		private Vector3 GetPriorityDirection()
		{
			float ballDirection = GetBallDirection();
			float bonusDirection = GetBonusDirection();
			float resultDirection = ballDirection + bonusDirection;

			/*if (Mathf.Abs(resultDirection) > config.DeadZone)
			{
				return new Vector3(resultDirection, 0, 0);
			}
			return Vector3.zero;*//*
			Debug.Log(resultDirection);*/
			return new Vector3(resultDirection, 0, 0);
		}

		private float GetBonusDirection()
		{
			int count = bonusService.Bonuses.Count();
			if (count <= 0)
			{
				return 0;
			}
			float direction = 0;
			float bonusYPosition;
			foreach (var bonus in bonusService.Bonuses)
			{
				bonusYPosition = bonus.transform.position.y;
				if (bonusYPosition < plateYPosition)
				{
					continue;
				}

				int bonusPriority = GetBonusPriority(bonus);

				float distanceMultiplier = GetDistanceMultiplier(bonus.transform.position);
				direction += (bonus.transform.position.x - plate.transform.position.x)
					* distanceMultiplier
					* bonusPriority;
			}
			return direction / count;
		}

		private float GetBallDirection()
		{
			int count = ballService.Balls.Count();
			if (count <= 0)
			{
				return 0;
			}

			float direction = 0;
			float ballYPosition;
			foreach (var ball in ballService.Balls)
			{
				ballYPosition = ball.transform.position.y;
				if (ballYPosition > autopilotTargetLinePosition || ballYPosition < plateYPosition)
				{
					continue;
				}

				float distanceMultiplier = GetDistanceMultiplier(ball.transform.position);
				direction += (ball.transform.position.x - plate.transform.position.x)
					* distanceMultiplier
					* config.AutopilotPriorityConfig.BallPriority;
			}
			return direction / count;
		}

		private float GetDistanceMultiplier(Vector2 position)
		{
			float verticalDistance = GetVerticalDistance(position.y);
			float horizontalDistance = GetHorizontalDistanceMultiplier(position.x);
			float distanceMultiplier = (config.AutopilotTargetDistance - verticalDistance) / config.AutopilotTargetDistance / horizontalDistance;
			return distanceMultiplier;
		}

		private float GetVerticalDistance(float yPosition)
		{
			return Mathf.Abs(plateYPosition - yPosition);
		}

		private float GetHorizontalDistanceMultiplier(float xPosition)
		{
			float xPlatePosition = plate.transform.position.x;
			float leftPoint = xPlatePosition - HalfPlateWidth;
			float rightPoint = xPlatePosition + HalfPlateWidth;

			if (xPosition > leftPoint && xPosition < rightPoint)
			{
				return 1;
			}
			if (xPosition > xPlatePosition)
			{
				return Mathf.Abs(rightPoint - xPosition);
			}
			return Mathf.Abs(leftPoint - xPosition);
		}

		private int GetBonusPriority(Bonus bonus)
		{
			return config.AutopilotPriorityConfig.Priorities
					.First(x => x.BonusId.Equals(bonus.BonusCommand.Id))
					.Priority;
		}
	}
}