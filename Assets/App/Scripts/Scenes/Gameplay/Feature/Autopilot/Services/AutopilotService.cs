using Features.StateMachine;
using Scenes.Gameplay.Feature.Autopilot.Factories;
using Scenes.Gameplay.Feature.Autopilot.Services.Entities;
using Scenes.Gameplay.Feature.Bonuses.Services;
using Scenes.Gameplay.Feature.Bonuses.Services.Bonuses;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.Feature.Player.Ball;
using Scenes.Gameplay.Feature.Player.Ball.Services;
using Scenes.Gameplay.Feature.Player.PlayerInput;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Autopilot.Services
{
	public class AutopilotService : IAutopilotService, IUpdatable
	{
		private Plate plate;
		private IInput input;
		private IBonusService bonusService;
		private IBallService ballService;
		private IBonusCommandService commandService;
		private IAutopilotStrategyFactory autopilotStrategyFactory;


		public AutopilotService(Plate plate,
								IInput input,
								IBonusService bonusService,
								IBallService ballService,
								IBonusCommandService commandService,
								IAutopilotStrategyFactory autopilotStrategyFactory)
		{
			this.plate = plate;
			this.input = input;
			this.bonusService = bonusService;
			this.ballService = ballService;
			this.commandService = commandService;
			this.autopilotStrategyFactory = autopilotStrategyFactory;
		}

		public List<AutopilotTarget> Targets { get; private set; }
		public bool IsActive { get; private set; } = false;

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
				plate.Movement.Move(GetPriorityDirection());
			}
		}

		private Vector2 GetPriorityDirection()
		{
			float MinY = ballService.Balls.Min(x => x.transform.position.y);
			List<Ball> ball = ballService.Balls.Where(x => x.transform.position.y == MinY).ToList();

			return new Vector2(ball[0].transform.position.x, plate.transform.position.y);
		}
	}
}