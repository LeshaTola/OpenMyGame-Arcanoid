using Scenes.Gameplay.Feature.Autopilot.Configs;
using Scenes.Gameplay.Feature.Autopilot.Strategies;
using System;
using System.Collections.Generic;
using Zenject;

namespace Scenes.Gameplay.Feature.Autopilot.Factories
{
	public class AutopilotStrategyFactory : IAutopilotStrategyFactory
	{
		private DiContainer diContainer;
		private AutopilotBonusStrategyDatabase strategyDatabase;

		private Dictionary<Type, IAutopilotStrategy> cashedValues = new();

		public AutopilotStrategyFactory(DiContainer diContainer,
								  AutopilotBonusStrategyDatabase strategyDatabase)
		{
			this.diContainer = diContainer;
			this.strategyDatabase = strategyDatabase;
		}

		public IAutopilotStrategy GetAutopilotStrategy(string id)
		{
			if (strategyDatabase.Strategies.TryGetValue(id, out var autopilotStrategy))
			{
				return GetAutopilotStrategy(autopilotStrategy.GetType());
			}
			return null;
		}

		private IAutopilotStrategy GetAutopilotStrategy(Type type)
		{
			if (cashedValues.ContainsKey(type))
			{
				return cashedValues[type];
			}
			else
			{
				var strategy = (IAutopilotStrategy)diContainer.Instantiate(type);
				cashedValues.Add(type, strategy);
				return strategy;
			}
		}
	}
}