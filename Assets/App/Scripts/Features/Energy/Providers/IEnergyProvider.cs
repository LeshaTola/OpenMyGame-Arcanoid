using Features.Energy.Configs;
using System;

namespace Features.Energy.Providers
{
	public interface IEnergyProvider
	{
		event Action OnEnergyChanged;

		int CurrentEnergy { get; }
		EnergyConfig Config { get; }
		float RemainingRecoveryTime { get; }

		void AddEnergy(int energy);
		void ReduceEnergy(int energy);
		void StartEnergyRecoveringAsync(float startTimer = 0);
	}
}

