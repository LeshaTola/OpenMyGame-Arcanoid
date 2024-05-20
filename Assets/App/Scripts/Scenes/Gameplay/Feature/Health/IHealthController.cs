using System;

namespace Scenes.Gameplay.Feature.Health
{
	public interface IHealthController
	{
		int CurrentHealth { get; }
		int MaxHealth { get; }

		event Action OnDeath;

		void AddHealth(int health);
		void ReduceHealth(int health);
		void ResetHealth();
	}
}