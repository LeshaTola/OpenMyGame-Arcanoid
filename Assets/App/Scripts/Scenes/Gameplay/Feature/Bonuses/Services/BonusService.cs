using Module.ObjectPool;
using Module.TimeProvider;
using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Score;
using Scenes.Gameplay.Feature.Bonuses.Commands;
using Scenes.Gameplay.Feature.Bonuses.Factories;
using Scenes.Gameplay.Feature.Bonuses.UI;
using Scenes.Gameplay.Feature.LevelCreation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scenes.Gameplay.Feature.Bonuses.Services
{
	public class BonusService : IBonusService
	{
		public event Action<IBonusCommand> OnBonusStart;
		public event Action<IBonusCommand> OnBonusUpdate;
		public event Action<IBonusCommand> OnBonusStop;

		private IPool<Bonus> pool;
		private IBonusCommandsFactory bonusCommandsFactory;
		private ITimeProvider timeProvider;
		private IBonusesUI bonusesUI;

		private List<IBonusCommand> bonusCommands = new();
		private List<IBonusCommand> commandsToRemove = new();

		public BonusService(IPool<Bonus> pool,
					  IBonusCommandsFactory bonusCommandsFactory,
					  ILevelGenerator levelGenerator,
					  ITimeProvider timeProvider)
		{
			this.pool = pool;
			this.timeProvider = timeProvider;
			this.bonusCommandsFactory = bonusCommandsFactory;

			levelGenerator.OnBlockDestroyed += OnBlockDestroyed;
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

		public void StartBonus(IBonusCommand bonusCommand)
		{
			var activeBonus = bonusCommands.FirstOrDefault(x => x.Id == bonusCommand.Id);
			if (activeBonus != null)
			{
				activeBonus.Timer = activeBonus.Duration;
				return;
			}

			RemoveAllConflicts(bonusCommand);

			bonusCommand.StartBonus();
			OnBonusStart?.Invoke(bonusCommand);
			if (bonusCommand.Duration <= 0)
			{
				return;
			}

			bonusCommands.Add(bonusCommand);
		}

		public void UpdateBonus()
		{
			foreach (var command in bonusCommands)
			{
				command.Timer -= timeProvider.DeltaTime;
				OnBonusUpdate?.Invoke(command);
				if (command.Timer <= 0)
				{
					commandsToRemove.Add(command);
				}
			}
			StopCommandsToRemove();
		}

		public void StopBonus(IBonusCommand bonusCommand)
		{
			bonusCommand.StopBonus();
			OnBonusStop?.Invoke(bonusCommand);
			bonusCommands.Remove(bonusCommand);
		}

		public void Cleanup()
		{
			var bonuses = new List<IBonusCommand>(bonusCommands);
			foreach (var command in bonuses)
			{
				StopBonus(command);
			}
			bonusCommands.Clear();
			bonuses.Clear();
		}

		private void RemoveAllConflicts(IBonusCommand bonusCommand)
		{
			foreach (int bonusId in bonusCommand.Conflicts)
			{
				var conflictBonus = bonusCommands.FirstOrDefault(x => x.Id == bonusId);
				if (conflictBonus != null)
				{
					commandsToRemove.Add(conflictBonus);
				}
			}
			StopCommandsToRemove();
		}

		private void OnBlockDestroyed(Block block)
		{
			string bonusId = GetBonusId(block);
			if (string.IsNullOrEmpty(bonusId))
			{
				return;
			}

			Bonus bonus = pool.Get();
			IBonusCommand bonusCommand = bonusCommandsFactory.GetBonusCommand(bonusId);
			bonus.Setup(bonusCommand, pool, this);

			bonus.transform.position = block.transform.position;
		}

		private void StopCommandsToRemove()
		{
			foreach (var command in commandsToRemove)
			{
				StopBonus(command);
			}
			commandsToRemove.Clear();
		}
	}
}
