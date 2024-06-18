using Features.StateMachine;
using Module.AI.Actions;
using Module.AI.Resolver;
using Scenes.Gameplay.Feature.Autopilot.Factories;
using Scenes.Gameplay.Feature.Player.PlayerInput;

namespace Scenes.Gameplay.Feature.Autopilot.Services
{
	public class AutopilotService : IAutopilotService, IUpdatable
	{
		private IInput input;
		private IActionResolver actionResolver;

		public AutopilotService(IInput input,
			IActionResolver actionResolver,
			IAutopilotActionsFactory actionsFactory)
		{
			this.input = input;
			this.actionResolver = actionResolver;

			actionResolver.Init(actionsFactory.GetAllActions());
		}

		public bool IsActive { get; private set; }

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
				IAction action = actionResolver.GetBestAction();
				action?.Execute();
			}
		}

	}
}