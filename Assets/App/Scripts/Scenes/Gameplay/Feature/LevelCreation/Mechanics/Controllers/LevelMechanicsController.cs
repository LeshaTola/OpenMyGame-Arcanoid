using Scenes.Gameplay.Feature.LevelCreation.Mechanics.Factories;
using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.LevelCreation.Mechanics.Controllers
{
	public class LevelMechanicsController : ILevelMechanicsController
	{
		private List<LevelMechanics> levelMechanicsList = new();
		private ILevelMechanicsFactory levelMechanicsFactory;

		public LevelMechanicsController(ILevelMechanicsFactory levelMechanicsFactory)
		{
			this.levelMechanicsFactory = levelMechanicsFactory;
		}

		public void StartLevelMechanics(List<LevelMechanics> levelMechanicsList)
		{
			foreach (var levelMechanics in levelMechanicsList)
			{
				var localLevelMechanics = levelMechanicsFactory.GetLevelMechanics(levelMechanics);
				localLevelMechanics.StartMechanics();
				this.levelMechanicsList.Add(localLevelMechanics);
			}
		}

		public void CleanUp()
		{
			foreach (var levelMechanics in levelMechanicsList)
			{
				levelMechanics.StopMechanics();
			}
		}
	}
}
