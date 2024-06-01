using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.LevelCreation.Mechanics.Controllers
{
	public interface ILevelMechanicsController
	{
		void StartLevelMechanics(List<LevelMechanics> levelMechanicsList);
		void CleanUp();
	}
}