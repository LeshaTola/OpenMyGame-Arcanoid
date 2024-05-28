using Module.ObjectPool;
using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Score;
using Scenes.Gameplay.Feature.Bonuses.Commands;
using Scenes.Gameplay.Feature.Bonuses.Factories;
using Scenes.Gameplay.Feature.LevelCreation;

namespace Scenes.Gameplay.Feature.Bonuses.Services
{
	public class BonusService : IBonusService
	{
		private IPool<Bonus> pool;
		private IBonusCommandsFactory bonusCommandsFactory;
		private ILevelGenerator levelGenerator;

		public BonusService(IPool<Bonus> pool, IBonusCommandsFactory bonusCommandsFactory, ILevelGenerator levelGenerator)
		{
			this.pool = pool;
			this.bonusCommandsFactory = bonusCommandsFactory;
			this.levelGenerator = levelGenerator;

			levelGenerator.OnBlockDestroyed += OnBlockDestroyed;
		}

		public void OnBlockDestroyed(Block block)
		{
			string bonusId = GetBonusId(block);
			if (string.IsNullOrEmpty(bonusId))
			{
				return;
			}

			Bonus bonus = pool.Get();
			IBonusCommand bonusCommand = bonusCommandsFactory.GetBonusCommand(bonusId);
			bonus.Setup(bonusCommand, pool);

			bonus.transform.position = block.transform.position;
		}

		private static string GetBonusId(Block block)
		{
			var healthComponent = block.Config.GetComponent<HealthComponent>();
			if (healthComponent == null)
			{
				return default;
			}

			var dropBonusComponent = block.Config.GetComponent<DropBonusComponent>(healthComponent.DeathComponents);
			if (dropBonusComponent == null)
			{
				return default;
			}

			string bonusId = dropBonusComponent.BonusId;
			return bonusId;
		}
	}
}
