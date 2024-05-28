using Scenes.Gameplay.Feature.Bonuses.Commands;
using Scenes.Gameplay.Feature.Bonuses.Configs;
using System;
using Zenject;

namespace Scenes.Gameplay.Feature.Bonuses.Factories
{
	public class BonusCommandsFactory : IBonusCommandsFactory
	{
		private BonusesDatabase bonusesDatabase;
		private DiContainer diContainer;

		public BonusCommandsFactory(BonusesDatabase bonusesDatabase, DiContainer diContainer)
		{
			this.bonusesDatabase = bonusesDatabase;
			this.diContainer = diContainer;
		}

		public IBonusCommand GetBonusCommand(string bonusId)
		{
			var originalBonus = bonusesDatabase.Bonuses[bonusId];
			Type bonusType = originalBonus.GetType();
			var bonusCommand = (IBonusCommand)diContainer.Instantiate(bonusType);
			bonusCommand.Clone(originalBonus);
			return bonusCommand;
		}
	}
}
