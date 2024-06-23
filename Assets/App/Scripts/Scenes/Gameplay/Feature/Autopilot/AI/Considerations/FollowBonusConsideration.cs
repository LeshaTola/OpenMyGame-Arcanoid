using Module.AI.Considerations;
using Scenes.Gameplay.Feature.AI.Providers;
using Scenes.Gameplay.Feature.Autopilot.Configs;
using Scenes.Gameplay.Feature.Bonuses;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.Player;
using UnityEngine;

namespace Scenes.Gameplay.Feature.AI.Considerations
{
	public class FollowBonusConsiderationConfig : ConsiderationConfig
	{
		[SerializeField] private AutopilotPriorityConfig priorityConfig;

		public AutopilotPriorityConfig PriorityConfig { get => priorityConfig; }
	}

	public class FollowBonusConsideration : Consideration
	{
		[SerializeField] private FollowBonusConsiderationConfig config;

		private Plate plate;
		private INearestObjectProvider nearestObjectProvider;
		private IFieldSizeProvider fieldSizeProvider;

		public FollowBonusConsideration(Plate plate,
								 INearestObjectProvider nearestObjectProvider,
								 IFieldSizeProvider fieldSizeProvider,
								 FollowBonusConsiderationConfig config)
		{
			this.config = config;

			this.plate = plate;
			this.nearestObjectProvider = nearestObjectProvider;
			this.fieldSizeProvider = fieldSizeProvider;
		}

		public override ConsiderationConfig Config => config;

		public override float GetScore()
		{
			Bonus nearestBonus = nearestObjectProvider.GetNearestBonus();
			if (nearestBonus == null || config.PriorityConfig.IsNegative(nearestBonus))
			{
				return 0;
			}

			float score = 1
				- Mathf.Abs(plate.transform.position.y - nearestBonus.transform.position.y)
				/ fieldSizeProvider.GameField.Height;
			//* GetHorizontalDistanceMultiplier(nearestBall.transform.position.x);

			return score;
		}


		private float GetHorizontalDistanceMultiplier(float xPosition)
		{
			float xPlatePosition = plate.transform.position.x;
			float halfPlateWidth = plate.BoxCollider.size.x / 2;
			float leftPoint = xPlatePosition - halfPlateWidth;
			float rightPoint = xPlatePosition + halfPlateWidth;
			float distance;
			if (xPosition > leftPoint && xPosition < rightPoint)
			{
				return 1;
			}
			if (xPosition > xPlatePosition)
			{
				distance = Mathf.Abs(fieldSizeProvider.GameField.MaxX - rightPoint);
				return 1 - Mathf.Abs(rightPoint - xPosition) / distance;
			}

			distance = Mathf.Abs(fieldSizeProvider.GameField.MinX - leftPoint);
			return 1 - Mathf.Abs(leftPoint - xPosition) / distance;
		}
	}
}
