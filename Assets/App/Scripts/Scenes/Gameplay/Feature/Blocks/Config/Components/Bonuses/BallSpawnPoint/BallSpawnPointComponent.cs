using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.BallSpawnPoint
{
	public class BallSpawnPointComponent : General.Component
	{
		public override void Execute()
		{
			base.Execute();

			var newBall = Block.BallService.GetBall();

			newBall.transform.position = Block.transform.position;
			newBall.Movement.Push(newBall.Movement.GetValidDirection(Random.insideUnitCircle));
			Block.BallService.ChangeBallsSpeed(Block.BallService.SpeedMultiplier);
			newBall.Movement.Rb.simulated = true;
		}
	}
}
