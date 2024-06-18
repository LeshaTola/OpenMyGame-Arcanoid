using Module.AI.Actions;
using Scenes.Gameplay.Feature.AI.Providers;
using Scenes.Gameplay.Feature.Bonuses;
using Scenes.Gameplay.Feature.Player;
using UnityEngine;
namespace Scenes.Gameplay.Feature.AI.Actions
{
	public class FollowNearestBonusAction : Action
	{
		private Plate plate;
		private INearestObjectProvider nearestObjectProvider;

		public FollowNearestBonusAction(Plate plate, INearestObjectProvider nearestObjectProvider)
		{
			this.plate = plate;
			this.nearestObjectProvider = nearestObjectProvider;
		}

		public override void Execute()
		{
			Bonus bonus = nearestObjectProvider.GetNearestBonus();
			Vector2 targetPosition = new(bonus.transform.position.x, plate.transform.position.y);
			plate.Movement.Move(targetPosition);
		}
	}
}
