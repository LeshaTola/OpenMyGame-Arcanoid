using Features.Energy.Providers;
using Features.ProjectCondition.Providers;
using Scenes.PackSelection.Feature.Packs;

namespace Features.Saves
{
	public class ProjectSavesController : IProjectSavesController
	{
		private IEnergyProvider energyProvider;
		private IPackProvider packProvider;

		public ProjectSavesController(IProjectConditionProvider projectConditionProvider,
								IEnergyProvider energyProvider,
								IPackProvider packProvider)
		{
			this.energyProvider = energyProvider;
			this.packProvider = packProvider;

			projectConditionProvider.OnApplicationStart += OnApplicationStart; ;
			projectConditionProvider.OnApplicationQuitted += OnApplicationQuitted;
			projectConditionProvider.OnApplicationPaused += OnApplicationPaused;
		}

		public void SaveAllData()
		{
			energyProvider.SaveData();
			packProvider.SaveData();
		}

		private void LoadAllData()
		{
			energyProvider.LoadData();
			packProvider.LoadData();
		}

		private void OnApplicationStart()
		{
			LoadAllData();
			energyProvider.StartEnergyRecoveringAsync();
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
