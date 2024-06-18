using Module.AI.Considerations;
using Scenes.Gameplay.Feature.AI.Providers;
using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Player.Providers;
using UnityEngine;

namespace Scenes.Gameplay.Feature.AI.Considerations
{
	public class BlockAimConsideration : Consideration
	{
		[SerializeField] private ConsiderationConfig config;

		private IPlateSizeProvider plateSizeProvider;
		private INearestObjectProvider nearestObjectProvider;

		public BlockAimConsideration(INearestObjectProvider nearestObjectProvider,
								 IPlateSizeProvider plateSizeProvider,
								 ConsiderationConfig config)
		{
			this.config = config;

			this.nearestObjectProvider = nearestObjectProvider;
			this.plateSizeProvider = plateSizeProvider;
		}

		public override ConsiderationConfig Config => config;

		public override float GetScore()
		{
			Block nearestBlock = nearestObjectProvider.GetNearestBlock();

			if (nearestBlock == null || !plateSizeProvider.InBounds(nearestBlock.transform.position))
			{
				return 0;
			}

			return 1;
		}
	}
}
