using Scenes.Gameplay.Feature.LevelCreation.Mechanics.Factories;
using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.LevelCreation.Mechanics.Controllers
{
	public class LevelMechanicsController : ILevelMechanicsController
	{
		private List<ILevelMechanics> levelMechanicsList = new();
		private ILevelMechanicsFactory levelMechanicsFactory;

		public LevelMechanicsController(ILevelMechanicsFactory levelMechanicsFactory)
		{
			this.levelMechanicsFactory = levelMechanicsFactory;
		}

		public void SetupLevelMechanics(List<ILevelMechanics> levelMechanicsList)
		{
			foreach (var levelMechanics in levelMechanicsList)
			{
				ILevelMechanics localLevelMechanics = levelMechanicsFactory.GetLevelMechanics(levelMechanics);
				this.levelMechanicsList.Add(localLevelMechanics);
			}
		}

		public void StartLevelMechanics()
		{
			foreach (var levelMechanics in levelMechanicsList)
			{
				levelMechanics.StartMechanics();
			}
		}

		public void StopLevelMechanics()
		{
			if (IsLevelMechanicsEmpty())
			{
				return;
			}

			foreach (var levelMechanics in levelMechanicsList)
			{
				levelMechanics.StopMechanics();
			}
		}

		public void Cleanup()
		{
			if (IsLevelMechanicsEmpty())
			{
				return;
			}

			foreach (var levelMechanics in levelMechanicsList)
			{
				levelMechanics.Cleanup();
			}
			levelMechanicsList.Clear();
		}

		private bool IsLevelMechanicsEmpty()
		{
			return levelMechanicsList == null || levelMechanicsList.Count <= 0;
		}
	}
}
