using Cysharp.Threading.Tasks;
using Module.ObjectPool;
using Module.TimeProvider;
using Scenes.Gameplay.Feature.Progress;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Ball.Services
{
	public class BallService : IBallService
	{
		private IPool<Ball> pool;
		private IProgressController progressController;
		private ITimeProvider timeProvider;

		public BallService(IPool<Ball> pool, IProgressController progressController, ITimeProvider timeProvider)
		{
			this.pool = pool;
			this.progressController = progressController;
			this.timeProvider = timeProvider;
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

		public async UniTask StopAllBallsAsync(float duration)
		{
			float elapsedTime = 0f;

			float multiplier;
			while (elapsedTime < duration)
			{
				elapsedTime += timeProvider.DeltaTime;
				multiplier = Mathf.Lerp(1, 0, elapsedTime / duration);

				foreach (var ball in pool.Active)
				{
					Vector2 direction = ball.Movement.Direction;
					ball.Movement.Push(direction, progressController.NormalizedProgress, multiplier);
				}

				await UniTask.Yield(PlayerLoopTiming.Update);
			}

		}
	}
}
