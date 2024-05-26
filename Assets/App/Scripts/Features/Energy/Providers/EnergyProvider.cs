using Cysharp.Threading.Tasks;
using Features.Energy.Configs;
using Features.Saves.Energy;
using Module.Saves;
using System;
using UnityEngine;

namespace Features.Energy.Providers
{
	public class EnergyProvider : IEnergyProvider
	{
		public event Action OnEnergyChanged;

		//private ITimeProvider timeProvider;
		private EnergyConfig config;
		private IDataProvider<EnergyData> energyDataProvider;

		private float timer;

		public EnergyProvider(EnergyConfig config,
							IDataProvider<EnergyData> energyDataProvider)
		{
			this.config = config;
			this.energyDataProvider = energyDataProvider;
		}

		public int CurrentEnergy { get; private set; }
		public EnergyConfig Config { get => config; }

		public async void StartEnergyRecoveringAsync(float startTimer = 0)
		{
			timer = startTimer;

			while (true)
			{
				if (CurrentEnergy >= config.MaxEnergy)
				{
					await UniTask.Yield();
					continue;
				}

				while (timer > 0)
				{
					await UniTask.Yield();
					timer -= Time.deltaTime;//timeProvider.DeltaTime;
				}
				timer = config.RecoveryTime;
				AddEnergy(config.RecoveryEnergy);
			}
		}

		public void AddEnergy(int energy)
		{
			if (energy <= 0)
			{
				return;
			}

			CurrentEnergy += energy;
			OnEnergyChanged?.Invoke();
			SaveEnergyData();
		}

		public void ReduceEnergy(int energy)
		{
			if (energy <= 0)
			{
				return;
			}
			CurrentEnergy -= energy;

			if (CurrentEnergy <= 0)
			{
				CurrentEnergy = 0;
			}
			OnEnergyChanged?.Invoke();
			SaveEnergyData();
		}

		private void SaveEnergyData()
		{
			var energyData = new EnergyData()
			{
				Energy = CurrentEnergy,
				ExitTime = DateTime.Now
			};
			energyDataProvider.SaveData(energyData);
		}
	}
}

