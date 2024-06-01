namespace Scenes.Gameplay.Feature.LevelCreation.Mechanics.Factories
{
	public interface ILevelMechanicsFactory
	{
		LevelMechanics GetLevelMechanics(LevelMechanics originalMechanics);
	}
}