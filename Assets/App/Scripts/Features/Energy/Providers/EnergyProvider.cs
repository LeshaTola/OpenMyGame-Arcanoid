using Cysharp.Threading.Tasks;
using Features.Energy.Configs;
using Features.Saves.Energy.Controllers;
using System;
using UnityEngine;

namespace Features.Energy.Providers
{
	public class EnergyProvider : IEnergyProvider
	{
		public event Action OnEnergyChanged;

		//private ITimeProvider timeProvider;
		private EnergyConfig config;
		IEnergySavesController savesController;
		private float timer;

		public EnergyProvider(EnergyConfig config,
						IEnergySavesController savesController)
		{
			this.config = config;
			this.savesController = savesController;

			StartEnergyRecoveringAsync(Config.RecoveryTime);

		}

		public int CurrentEnergy { get; private set; }
		public EnergyConfig Config { get => config; }
		public float RemainingRecoveryTime { get => timer; }

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
			savesController.SaveEnergyData(this);
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
			savesController.SaveEnergyData(this);
		}
	}
}

