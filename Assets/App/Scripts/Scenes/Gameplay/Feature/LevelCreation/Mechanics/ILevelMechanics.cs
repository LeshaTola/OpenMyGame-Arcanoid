namespace Scenes.Gameplay.Feature.LevelCreation.Mechanics
{
	public interface ILevelMechanics
	{
		void Cleanup();
		public void StartMechanics();
		public void StopMechanics();
	}
}
