using Features.Saves.Gameplay.DTO.Bonuses;
using Module.TimeProvider;
using Scenes.Gameplay.Feature.Bonuses.Commands;
using Scenes.Gameplay.Feature.Bonuses.Factories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scenes.Gameplay.Feature.Bonuses.Services
{
	public class BonusCommandService : IBonusCommandService
	{
		public event Action<IBonusCommand> OnBonusStart;
		public event Action<IBonusCommand> OnBonusUpdate;
		public event Action<IBonusCommand> OnBonusStop;

		private IBonusCommandsFactory bonusCommandsFactory;
		private ITimeProvider timeProvider;

		private List<IBonusCommand> bonusCommands = new();
		private List<IBonusCommand> commandsToRemove = new();

		public BonusCommandService(IBonusCommandsFactory bonusCommandsFactory,
					  ITimeProvider timeProvider)
		{
			this.bonusCommandsFactory = bonusCommandsFactory;
			this.timeProvider = timeProvider;
		}

		public void StartBonus(IBonusCommand bonusCommand)
		{
			var activeBonus = bonusCommands.FirstOrDefault(x => x.Id.Equals(bonusCommand.Id));
			if (activeBonus != null)
			{
				activeBonus.Timer = activeBonus.Config.Duration;
				return;
			}

			RemoveAllConflicts(bonusCommand);

			bonusCommand.StartBonus();
			if (bonusCommand.Config.Duration <= 0)
			{
				return;
			}

			bonusCommands.Add(bonusCommand);
			OnBonusStart?.Invoke(bonusCommand);
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

		public IBonusCommand GetBonusCommand(string id)
		{
			return bonusCommandsFactory.GetBonusCommand(id);
		}

		public List<BonusCommandData> GetBonusesCommandsData()
		{
			var activeBonuses = new List<BonusCommandData>();
			foreach (IBonusCommand bonusCommand in bonusCommands)
			{
				activeBonuses.Add(new BonusCommandData()
				{
					Id = bonusCommand.Id,
					RemainingTime = bonusCommand.Timer
				});
			}
			return activeBonuses;
		}

		public void SetBonusesCommandsData(List<BonusCommandData> bonusCommands)
		{
			foreach (var bonusData in bonusCommands)
			{
				var bonusCommand = GetBonusCommand(bonusData.Id);
				StartBonus(bonusCommand);
				bonusCommand.Timer = bonusData.RemainingTime;
			}
		}

		private void RemoveAllConflicts(IBonusCommand bonusCommand)
		{
			foreach (string bonusId in bonusCommand.Config.Conflicts)
			{
				var conflictBonus = bonusCommands.FirstOrDefault(x => x.Id.Equals(bonusId));
				if (conflictBonus != null)
				{
					commandsToRemove.Add(conflictBonus);
				}
			}
			StopCommandsToRemove();
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
