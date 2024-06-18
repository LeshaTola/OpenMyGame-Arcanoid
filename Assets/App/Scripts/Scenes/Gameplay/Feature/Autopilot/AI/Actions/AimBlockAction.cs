using Module.AI.Actions;
using Scenes.Gameplay.Feature.AI.Providers;
using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Player;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Autopilot.AI.Actions
{
	public class AimBlockAction : Action
	{
		private Plate plate;
		private INearestObjectProvider nearestObjectProvider;

		public AimBlockAction(Plate plate,
						INearestObjectProvider nearestObjectProvider)
		{
			this.plate = plate;
			this.nearestObjectProvider = nearestObjectProvider;
		}

		public override void Execute()
		{
			Block nearestBlock = nearestObjectProvider.GetNearestBlock();

			if (nearestBlock == null)
			{
				return;
			}

			plate.Movement.Move(new Vector2(nearestBlock.transform.position.x, plate.transform.position.y));
		}
	}
}
