using Features.Energy.Configs;
using System;

namespace Features.Energy.Providers
{
	public interface IEnergyProvider
	{
		event Action OnEnergyChanged;
		event Action OnEnergyTimerChanged;

		int CurrentEnergy { get; }
		EnergyConfig Config { get; }
		float RemainingRecoveryTime { get; set; }

		void AddEnergy(int energy);
		void ReduceEnergy(int energy);
		void StartEnergyRecoveringAsync();
		void LoadData();
		void SaveData();
	}
}

