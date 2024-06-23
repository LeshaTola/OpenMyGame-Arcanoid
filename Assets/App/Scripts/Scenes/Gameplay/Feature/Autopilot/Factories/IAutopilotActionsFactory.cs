using Module.AI.Actions;
using Module.AI.Considerations;
using System;
using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.Autopilot.Factories
{
	public interface IAutopilotActionsFactory
	{
		IAction GetAction(Type type);
		List<IAction> GetAllActions();
		IConsideration GetConsideration(IConsideration consideration);
	}
}