using Features.Energy.Providers;
using Features.Saves;
using UnityEngine;

namespace Features.ProjectInitServices
{
	public class ProjectInitService : IProjectInitService
	{
		private int targetFrameRate;
		private IProjectSavesController projectSavesController;
		private IEnergyProvider energyProvider;

		public ProjectInitService(int targetFrameRate,
			IProjectSavesController projectSavesController,
			IEnergyProvider energyProvider)
		{
			this.targetFrameRate = targetFrameRate;
			this.projectSavesController = projectSavesController;
			this.energyProvider = energyProvider;
		}

		public void InitProject()
		{
			Application.targetFrameRate = targetFrameRate;
			projectSavesController.LoadAllData();
			energyProvider.StartEnergyRecoveringAsync();
		}
	}
}
