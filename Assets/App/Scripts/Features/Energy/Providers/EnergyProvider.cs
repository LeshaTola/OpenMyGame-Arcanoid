using Cysharp.Threading.Tasks;
using Features.Energy.Configs;
using Features.Energy.Saves;
using Module.Saves;
using Module.TimeProvider;
using System;

namespace Features.Energy.Providers
{
	public class EnergyProvider : IEnergyProvider
	{
		public event Action OnEnergyChanged;
		public event Action OnEnergyTimerChanged;

		private ITimeProvider timeProvider;
		private EnergyConfig config;
		private IDataProvider<EnergyData> energyDataProvider;

		private float timer;

		public EnergyProvider(EnergyConfig config,
						ITimeProvider timeProvider,
						IDataProvider<EnergyData> energyDataProvider)
		{
			this.config = config;
			this.timeProvider = timeProvider;
			this.energyDataProvider = energyDataProvider;
		}

		public int CurrentEnergy { get; private set; }
		public EnergyConfig Config { get => config; }
		public float RemainingRecoveryTime { get => timer; set => timer = value; }

		public async void StartEnergyRecoveringAsync()
		{
			while (true)
			{
				if (CurrentEnergy >= config.MaxEnergy)
				{
					await UniTask.Yield();
					RemainingRecoveryTime = config.RecoveryTime;
					continue;
				}

				while (timer > 0)
				{
					await UniTask.Yield();
					timer -= timeProvider.DeltaTime;
					OnEnergyTimerChanged?.Invoke();
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
			SaveData();
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
			SaveData();
		}

		public void SaveData()
		{
			var energyData = new EnergyData()
			{
				Energy = CurrentEnergy,
				RemainingRecoveryTime = RemainingRecoveryTime,
				ExitTime = DateTime.Now
			};
			energyDataProvider.SaveData(energyData);
		}

		public void LoadData()
		{
			var energyData = energyDataProvider.GetData();
			if (energyData == null)
			{
				energyData = FormFirstData();
				energyDataProvider.SaveData(energyData);
			}

			int totalEnergy = energyData.Energy;
			int additionalEnergy = GetAdditionalEnergyBetweenSessions(energyData, totalEnergy);
			totalEnergy += additionalEnergy;

			AddEnergy(totalEnergy);
		}

		private int GetAdditionalEnergyBetweenSessions(EnergyData energyData, int totalEnergy)
		{
			if (totalEnergy >= Config.MaxEnergy)
			{
				return 0;
			}

			TimeSpan timeSpan = DateTime.Now - energyData.ExitTime;
			float remainingTime = (float)(energyData.RemainingRecoveryTime - timeSpan.TotalSeconds);
			if (remainingTime >= 0)
			{
				RemainingRecoveryTime = remainingTime;
				return 0;
			}
			int offlineEnergy = Config.RecoveryEnergy;

			float recoverAmount = -remainingTime / Config.RecoveryTime;
			int recoverAmountInt = (int)recoverAmount;

			float remainingRecoveryTime = Config.RecoveryTime - (-remainingTime % Config.RecoveryTime);
			RemainingRecoveryTime = remainingRecoveryTime;

			offlineEnergy += recoverAmountInt * Config.RecoveryEnergy;
			int remainingEnergy = Config.MaxEnergy - totalEnergy;

			return GetAdditionalEnergy(offlineEnergy, remainingEnergy);
		}

		private static int GetAdditionalEnergy(int offlineEnergy, int remainingEnergy)
		{
			int additionalEnergy = remainingEnergy;
			if (remainingEnergy > offlineEnergy)
			{
				additionalEnergy = offlineEnergy;
			}

			return additionalEnergy;
		}

		private EnergyData FormFirstData()
		{
			EnergyData energyData = new()
			{
				Energy = Config.MaxEnergy,
				RemainingRecoveryTime = 0,
				ExitTime = DateTime.Now
			};
			return energyData;
		}
	}
}

