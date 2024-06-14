namespace Scenes.Gameplay.Feature.LevelCreation.Mechanics.Factories
{
	public interface ILevelMechanicsFactory
	{
		ILevelMechanics GetLevelMechanics(ILevelMechanics originalMechanics);
	}
}