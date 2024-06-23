using Features.Saves.Gameplay.DTO.LevelMechanics;

namespace Scenes.Gameplay.Feature.LevelCreation.Mechanics
{
	public interface ILevelMechanics
	{
		void Cleanup();
		void StartMechanics();
		void StopMechanics();

		LevelMechanicsData GetMechanicsData();
		void SetMechanicsData(LevelMechanicsData data);
	}
}
