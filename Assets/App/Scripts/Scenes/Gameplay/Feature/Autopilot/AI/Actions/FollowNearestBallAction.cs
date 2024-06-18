using Module.AI.Actions;
using Scenes.Gameplay.Feature.AI.Providers;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.Feature.Player.Ball;
using UnityEngine;
namespace Scenes.Gameplay.Feature.AI.Actions
{
	public class FollowNearestBallAction : Action
	{
		private Plate plate;
		private INearestObjectProvider nearestObjectProvider;

		public FollowNearestBallAction(Plate plate, INearestObjectProvider nearestObjectProvider)
		{
			this.plate = plate;
			this.nearestObjectProvider = nearestObjectProvider;
		}

		public override void Execute()
		{
			Ball ball = nearestObjectProvider.GetNearestBall();
			Vector2 targetPosition = new(ball.transform.position.x, plate.transform.position.y);
			plate.Movement.Move(targetPosition);
		}
	}
}
