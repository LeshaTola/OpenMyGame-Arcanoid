using Features.Saves.Gameplay.DTOs.Health;
using System;

namespace Scenes.Gameplay.Feature.Health
{
	public interface IHealthController
	{
		int CurrentHealth { get; }
		int MaxHealth { get; }

		event Action OnDeath;

		void AddHealth(int health);
		HealthState GetHealthState();
		void ReduceHealth(int health);
		void ResetHealth();
		void SetHealthState(HealthState healthState);
	}
}