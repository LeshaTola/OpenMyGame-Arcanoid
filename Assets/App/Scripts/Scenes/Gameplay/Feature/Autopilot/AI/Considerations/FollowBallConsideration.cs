using Module.AI.Considerations;
using Scenes.Gameplay.Feature.AI.Providers;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.Feature.Player.Ball;
using UnityEngine;

namespace Scenes.Gameplay.Feature.AI.Considerations
{
	public class FollowBallConsideration : Consideration
	{
		[SerializeField] private ConsiderationConfig config;

		private Plate plate;
		private INearestObjectProvider nearestObjectProvider;
		private IFieldSizeProvider fieldSizeProvider;

		public FollowBallConsideration(Plate plate,
								 INearestObjectProvider nearestObjectProvider,
								 IFieldSizeProvider fieldSizeProvider,
								 ConsiderationConfig config)
		{
			this.config = config;

			this.plate = plate;
			this.nearestObjectProvider = nearestObjectProvider;
			this.fieldSizeProvider = fieldSizeProvider;
		}

		public override ConsiderationConfig Config => config;

		public override float GetScore()
		{
			Ball nearestBall = nearestObjectProvider.GetNearestBall();
			if (nearestBall == null || nearestBall.Movement.Direction.y > 0)
			{
				return 0;
			}

			float score = 1 -
				Mathf.Abs(plate.transform.position.y - nearestBall.transform.position.y)
				/ fieldSizeProvider.GameField.Height;

			return score;
		}
	}
}
