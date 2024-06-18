using Module.AI.Considerations;
using Scenes.Gameplay.Feature.AI.Providers;
using Scenes.Gameplay.Feature.Autopilot.Configs;
using Scenes.Gameplay.Feature.Bonuses;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.Player;
using UnityEngine;

namespace Scenes.Gameplay.Feature.AI.Considerations
{
	public class DodgeBonusConsiderationConfig : ConsiderationConfig
	{
		[SerializeField] private AutopilotPriorityConfig priorityConfig;

		public AutopilotPriorityConfig PriorityConfig { get => priorityConfig; }
	}

	public class DodgeBonusConsideration : Consideration
	{
		[SerializeField] private DodgeBonusConsiderationConfig config;

		private Plate plate;
		private INearestObjectProvider nearestObjectProvider;
		private IFieldSizeProvider fieldSizeProvider;

		public DodgeBonusConsideration(Plate plate,
								 INearestObjectProvider nearestObjectProvider,
								 IFieldSizeProvider fieldSizeProvider,
								 DodgeBonusConsiderationConfig config)
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
			if (nearestBonus == null || !config.PriorityConfig.IsNegative(nearestBonus) || !IsInBounds(nearestBonus))
			{
				return 0;
			}

			float score = 1
				- Mathf.Abs(plate.transform.position.y - nearestBonus.transform.position.y)
				/ fieldSizeProvider.GameField.Height;

			return score;
		}

		private bool IsInBounds(Bonus nearestBonus)
		{
			float xPlatePosition = plate.transform.position.x;
			float halfPlateWidth = plate.BoxCollider.size.x / 2;
			float leftPoint = xPlatePosition - halfPlateWidth;
			float rightPoint = xPlatePosition + halfPlateWidth;
			float xPosition = nearestBonus.transform.position.x;

			return xPosition > leftPoint && xPosition < rightPoint;
		}
	}
}
