using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Bonuses;
using Scenes.Gameplay.Feature.Bonuses.Services.Bonuses;
using Scenes.Gameplay.Feature.LevelCreation.Services;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.Feature.Player.Ball;
using Scenes.Gameplay.Feature.Player.Ball.Services;
using UnityEngine;

namespace Scenes.Gameplay.Feature.AI.Providers
{
	public class NearestObjectProvider : INearestObjectProvider
	{
		private Plate plate;
		private IBallService ballService;
		private IBonusService bonusService;
		private ILevelService levelService;

		private float plateYPosition;

		public NearestObjectProvider(Plate plate,
							   IBallService ballService,
							   IBonusService bonusService,
							   ILevelService levelService)
		{
			this.plate = plate;
			this.ballService = ballService;
			this.bonusService = bonusService;
			this.levelService = levelService;

			plateYPosition = plate.transform.position.y;
		}

		public Block GetNearestBlock()
		{
			Block nearestBlock = null;
			float minDistance = float.MaxValue;
			float plateXPosition = plate.transform.position.x;

			foreach (Block block in levelService.Blocks)
			{
				float distance = Mathf.Abs(plateXPosition - block.transform.position.x);
				if (distance < minDistance)
				{
					minDistance = distance;
					nearestBlock = block;
				}
			}
			return nearestBlock;
		}

		public Ball GetNearestBall()
		{
			Ball nearestBall = null;
			float minDistance = float.MaxValue;
			foreach (var ball in ballService.Balls)
			{
				if (ball.transform.position.y < plateYPosition)
				{
					continue;
				}

				float distance = Vector2.Distance(plate.transform.position, ball.transform.position);
				if (distance < minDistance)
				{
					minDistance = distance;
					nearestBall = ball;
				}
			}

			return nearestBall;
		}

		public Bonus GetNearestBonus()
		{
			Bonus nearestBonus = null;
			float minDistance = float.MaxValue;
			foreach (var bonus in bonusService.Bonuses)
			{
				if (bonus.transform.position.y < plateYPosition)
				{
					continue;
				}

				float distance = Vector2.Distance(plate.transform.position, bonus.transform.position);
				if (distance < minDistance)
				{
					minDistance = distance;
					nearestBonus = bonus;
				}
			}

			return nearestBonus;
		}
	}
}
