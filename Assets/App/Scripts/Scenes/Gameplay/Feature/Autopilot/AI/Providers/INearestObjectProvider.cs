using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Bonuses;
using Scenes.Gameplay.Feature.Player.Ball;

namespace Scenes.Gameplay.Feature.AI.Providers
{
	public interface INearestObjectProvider
	{
		Ball GetNearestBall();
		Block GetNearestBlock();
		Bonus GetNearestBonus();
	}
}
