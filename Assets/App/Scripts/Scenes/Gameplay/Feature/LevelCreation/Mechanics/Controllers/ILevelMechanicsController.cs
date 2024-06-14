using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.LevelCreation.Mechanics.Controllers
{
	public interface ILevelMechanicsController
	{
		void SetupLevelMechanics(List<ILevelMechanics> levelMechanicsList);
		void StopLevelMechanics();
		void Cleanup();
		void StartLevelMechanics();
	}
}