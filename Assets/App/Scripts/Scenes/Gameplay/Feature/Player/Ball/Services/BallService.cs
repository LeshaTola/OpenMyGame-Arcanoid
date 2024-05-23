using Module.ObjectPool;
using Scenes.Gameplay.Feature.Progress;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Ball.Services
{
	public class BallService : IBallService
	{
		private IPool<Ball> pool;
		private IProgressController progressController;

		public BallService(IPool<Ball> pool, IProgressController progressController)
		{
			this.pool = pool;
			this.progressController = progressController;
		}

		public Ball GetBall()
		{
			Ball ball = pool.Get();
			ball.Init(this);
			ball.OnCollisionEnter += OnBallCollisionEnter;
			return ball;
		}

		public void ReleaseBall(Ball ball)
		{
			ball.OnCollisionEnter -= OnBallCollisionEnter;
			pool.Release(ball);
		}

		private void OnBallCollisionEnter(Ball ball, Collision2D collision)
		{

			Vector2 newDirection = ball.Movement.Direction;
			if (!collision.gameObject.TryGetComponent(out Plate player))
			{
				newDirection = ball.Movement.GetValidDirection();
			}
			ball.Movement.Push(newDirection, progressController.NormalizedProgress);
		}
	}
}
