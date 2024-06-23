using Module.AI.Actions;
using Scenes.Gameplay.Feature.AI.Providers;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.Feature.Player.Providers;
using UnityEngine;
namespace Scenes.Gameplay.Feature.AI.Actions
{
	public class DodgeBonusAction : Action
	{
		private const float OFFSET = 0.1f;

		private Plate plate;
		private IPlateSizeProvider plateSizeProvider;
		private INearestObjectProvider nearestObjectProvider;

		public DodgeBonusAction(Plate plate,
			INearestObjectProvider nearestObjectProvider,
			IPlateSizeProvider plateSizeProvider)
		{
			this.plate = plate;
			this.nearestObjectProvider = nearestObjectProvider;
			this.plateSizeProvider = plateSizeProvider;
		}

		public override void Execute()
		{
			var bonus = nearestObjectProvider.GetNearestBonus();

			if (bonus == null)
			{
				return;
			}

			Vector2 targetPosition;
			float distance;
			if (bonus.transform.position.x > 0)
			{
				distance = Mathf.Abs(bonus.transform.position.x - plateSizeProvider.RightPosition.x) + OFFSET;
				targetPosition
					= new Vector2(bonus.transform.position.x - distance, plate.transform.position.y);
			}
			else
			{
				distance = Mathf.Abs(bonus.transform.position.x - plateSizeProvider.LeftPosition.x) + OFFSET;
				targetPosition
					= new Vector2(bonus.transform.position.x + distance, plate.transform.position.y);
			}

			plate.Movement.Move(targetPosition);
		}
	}
}
