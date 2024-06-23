using Features.Saves.Gameplay;
using Features.StateMachine;
using Module.ObjectPool;
using Module.TimeProvider;
using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Score;
using Scenes.Gameplay.Feature.Bonuses.Commands;
using Scenes.Gameplay.Feature.LevelCreation;
using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.Bonuses.Services.Bonuses
{
	public class BonusService : IBonusService, IUpdatable
	{
		private IPool<Bonus> pool;
		private ITimeProvider timeProvider;
		private IBonusCommandService bonusCommandsService;

		public BonusService(ILevelGenerator levelGenerator,
							 IPool<Bonus> pool,
							 ITimeProvider timeProvider,
							 IBonusCommandService bonusCommandsService)
		{
			this.timeProvider = timeProvider;
			this.pool = pool;
			this.bonusCommandsService = bonusCommandsService;

			levelGenerator.OnBlockDestroyed += OnBlockDestroyed;
		}

		public IEnumerable<Bonus> Bonuses { get => pool.Active; }

		public void Update()
		{
			bonusCommandsService.UpdateBonus();
			foreach (Bonus bonus in pool.Active)
			{
				bonus.Movement.Move(timeProvider.DeltaTime);
			}
		}

		public List<BonusPosition> GetBonusesData()
		{
			var bonusesPositions = new List<BonusPosition>();
			foreach (Bonus bonus in pool.Active)
			{
				bonusesPositions.Add(new BonusPosition()
				{
					Id = bonus.BonusCommand.Id,
					Position = new(bonus.transform.position)
				});
			}
			return bonusesPositions;
		}

		public void SetBonusData(List<BonusPosition> bonusPositions)
		{
			foreach (BonusPosition bonusData in bonusPositions)
			{
				Bonus bonus = GetBonus(bonusData.Id);
				bonus.transform.position = new(bonusData.Position.X, bonusData.Position.Y);
			}
		}

		public void Cleanup()
		{
			var bonuses = new List<Bonus>(pool.Active);
			foreach (Bonus bonus in bonuses)
			{
				bonus.Release();
			}
			bonuses.Clear();
		}

		private void OnBlockDestroyed(Block block)
		{
			string bonusId = GetBonusId(block);
			if (string.IsNullOrEmpty(bonusId))
			{
				return;
			}

			Bonus bonus = GetBonus(bonusId);
			bonus.transform.position = block.transform.position;
			bonus.Resize(block.SizeMultiplier);
		}


		private string GetBonusId(Block block)
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

		private Bonus GetBonus(string id)
		{
			Bonus bonus = pool.Get();
			IBonusCommand bonusCommand = bonusCommandsService.GetBonusCommand(id);
			bonus.Setup(bonusCommand, pool, bonusCommandsService);
			return bonus;
		}
	}
}
