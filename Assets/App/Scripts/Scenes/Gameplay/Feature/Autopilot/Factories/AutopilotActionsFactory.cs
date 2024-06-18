using Module.AI.Actions;
using Module.AI.Considerations;
using Scenes.Gameplay.Feature.Autopilot.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Scenes.Gameplay.Feature.Autopilot.Factories
{
	public class AutopilotActionsFactory : IAutopilotActionsFactory
	{
		private DiContainer diContainer;
		private ActionsDatabase actionsDatabase;

		private Dictionary<Type, IAction> cashedValues = new();

		public AutopilotActionsFactory(DiContainer diContainer,
								  ActionsDatabase actionsDatabase)
		{
			this.diContainer = diContainer;
			this.actionsDatabase = actionsDatabase;
		}

		public List<IAction> GetAllActions()
		{
			List<IAction> actions = new List<IAction>();
			foreach (var originalAction in actionsDatabase.Actions)
			{
				actions.Add(GetAction(originalAction.GetType()));
			}
			return actions;
		}

		public IAction GetAction(Type type)
		{
			if (cashedValues.ContainsKey(type))
			{
				return cashedValues[type];
			}
			else
			{
				var original = GetOriginalAction(type);

				var action = (IAction)diContainer.Instantiate(type);
				foreach (var originalConsideration in original.Considerations)
				{
					action.Considerations.Add(GetConsideration(originalConsideration));
				}

				cashedValues.Add(type, action);
				return action;
			}
		}

		public IConsideration GetConsideration(IConsideration consideration)
		{
			return (IConsideration)diContainer.Instantiate(consideration.GetType(),
												  new object[] { consideration.Config });
		}

		private IAction GetOriginalAction(Type type)
		{
			return actionsDatabase.Actions.FirstOrDefault(x => x.GetType().Equals(type));
		}
	}
}