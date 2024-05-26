using Features.Energy.Providers;
using Features.ProjectCondition.Providers;
using Features.Saves.Energy;
using Module.Saves;
using Scenes.PackSelection.Feature.Packs;
using System;
using System.Linq;

namespace Features.Saves
{
	public class ProjectSavesController : IProjectSavesController
	{
		IDataProvider<EnergyData> energyDataProvider;
		IDataProvider<PlayerProgressData> playerProgressDataProvider;

		IEnergyProvider energyProvider;
		IPackProvider packProvider;

		public ProjectSavesController(IProjectConditionProvider projectConditionProvider,
								IDataProvider<EnergyData> energyDataProvider,
								IDataProvider<PlayerProgressData> playerProgressDataProvider,
								IEnergyProvider energyProvider,
								IPackProvider packProvider)
		{
			this.energyDataProvider = energyDataProvider;
			this.playerProgressDataProvider = playerProgressDataProvider;
			this.energyProvider = energyProvider;
			this.packProvider = packProvider;

			projectConditionProvider.OnApplicationStart += OnApplicationStart; ;
			projectConditionProvider.OnApplicationQuitted += OnApplicationQuitted;
			projectConditionProvider.OnApplicationPaused += OnApplicationPaused;
		}

		public void SaveAllData()
		{

		}

		private void LoadAllData()
		{
			LoadEnergyData();
			LoadPlayerProgressData();
		}

		private void LoadPlayerProgressData()
		{
			var playerProgressData = playerProgressDataProvider.GetData();
			if (playerProgressData == null)
			{
				playerProgressData = FormFirstPlayerProgressData();
				playerProgressDataProvider.SaveData(playerProgressData);
			}
		}

		private void LoadEnergyData()
		{
			var energyData = energyDataProvider.GetData();
			if (energyData == null)
			{
				energyData = FormFirstEnergyData();
				energyDataProvider.SaveData(energyData);
			}
			//Calculate energy between sessions

			energyProvider.AddEnergy(energyData.Energy);
		}

		private EnergyData FormFirstEnergyData()
		{
			EnergyData energyData = new()
			{
				Energy = energyProvider.Config.MaxEnergy,
				ExitTime = DateTime.Now
			};
			return energyData;
		}

		private PlayerProgressData FormFirstPlayerProgressData()
		{
			PlayerProgressData playerProgress = new();

			foreach (var pack in packProvider.Packs)
			{

				SavedPackData savedPackData = new()
				{
					Id = pack.Id,
					CurrentLevel = 0,
					IsOpened = false,
					IsCompeted = false,
				};

				playerProgress.Packs.Add(pack.Id, savedPackData);
			}
			playerProgress.Packs[packProvider.Packs.Last().Id].IsOpened = true;
			playerProgress.IsFirstSession = true;
			return playerProgress;
		}

		private void OnApplicationStart()
		{
			LoadAllData();
		}

		private void OnApplicationPaused(bool pause)
		{
			if (pause)
			{
				SaveAllData();
			}
		}

		private void OnApplicationQuitted()
		{
			SaveAllData();
		}
	}
}
