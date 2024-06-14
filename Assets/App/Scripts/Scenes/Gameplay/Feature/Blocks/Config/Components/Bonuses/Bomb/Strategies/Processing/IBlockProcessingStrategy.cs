using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb.Strategies.Processing
{
	public interface IBlockProcessingStrategy
	{
		public UniTask Process(List<Block> blocks);
	}
}
