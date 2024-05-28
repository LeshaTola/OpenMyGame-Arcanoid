using Scenes.Gameplay.Feature.Blocks;

namespace Scenes.Gameplay.Feature.Bonuses.Services
{
	public interface IBonusService
	{
		void OnBlockDestroyed(Block block);
	}
}