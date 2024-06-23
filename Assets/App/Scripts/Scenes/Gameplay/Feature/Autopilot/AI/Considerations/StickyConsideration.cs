using Module.AI.Considerations;
using Scenes.Gameplay.Feature.Player;
using UnityEngine;

namespace Scenes.Gameplay.Feature.AI.Considerations
{
	public class StickyConsideration : Consideration
	{
		[SerializeField] private ConsiderationConfig config;

		private Plate plate;

		public StickyConsideration(Plate plate, ConsiderationConfig config)
		{
			this.plate = plate;
			this.config = config;
		}

		public override ConsiderationConfig Config => config;

		public override float GetScore()
		{
			if (plate == null || plate.IsSticky == false || plate.ConnectedBalls.Count <= 0)
			{
				return 0;
			}

			return 1;
		}
	}
}
