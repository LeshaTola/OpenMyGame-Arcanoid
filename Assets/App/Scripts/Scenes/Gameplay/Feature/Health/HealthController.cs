using Features.Saves.Gameplay.DTOs.Health;
using Scenes.Gameplay.Feature.Health.Configs;
using System;
using Zenject;

namespace Scenes.Gameplay.Feature.Health
{
	public class HealthController : IInitializable, IHealthController
	{
		public event Action OnDeath;

		private HealthConfig config;
		private IHealthBarUI healthBarUI;

		private int currentHealth;

		public HealthController(HealthConfig config, IHealthBarUI healthBarUI)
		{
			this.config = config;
			this.healthBarUI = healthBarUI;
		}

		public int MaxHealth { get => config.MaxHealth; }
		public int CurrentHealth { get => currentHealth; }

		public void Initialize()
		{
			healthBarUI.CreateUI(config.MaxHealth);
		}

		public void AddHealth(int health)
		{
			if (health <= 0)
			{
				return;
			}
			int prevHealth = currentHealth;
			currentHealth += health;

			if (currentHealth > config.MaxHealth)
			{
				currentHealth = config.MaxHealth;
			}

			healthBarUI.ActivateAmount(currentHealth, prevHealth);
		}

		public void ReduceHealth(int health)
		{
			if (health <= 0)
			{
				return;
			}
			int prevHealth = currentHealth;
			currentHealth -= health;

			if (currentHealth <= 0)
			{
				currentHealth = 0;
				OnDeath?.Invoke();
			}

			healthBarUI.DeactivateAmount(currentHealth, prevHealth);
		}

		public void ResetHealth()
		{
			AddHealth(config.Health);
		}

		public void SetHealthState(HealthState healthState)
		{
			currentHealth = 0;
			AddHealth(healthState.Health);
		}

		public HealthState GetHealthState()
		{
			return new HealthState()
			{
				Health = currentHealth,
			};
		}
	}
}