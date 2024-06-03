using Scenes.Gameplay.Feature.Blocks;

namespace Scenes.Gameplay.Feature.LevelCreation
{
	public interface IBlockFactory
	{
		Block GetBlock(int id);
	}
}