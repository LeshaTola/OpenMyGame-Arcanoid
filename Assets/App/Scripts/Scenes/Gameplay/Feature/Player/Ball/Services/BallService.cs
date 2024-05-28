using Cysharp.Threading.Tasks;
using Module.ObjectPool;
using Module.TimeProvider;
using Scenes.Gameplay.Feature.Progress;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Ball.Services
{
	public class BallService : IBallService
	{
		private IPool<Ball> pool;
		private IProgressController progressController;
		private ITimeProvider timeProvider;

		private Dictionary<Ball, Vector2> lastBallsDirections = new();

		public float SpeedMultiplier { get; private set; } = 1;

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
			ball.Movement.Push(newDirection, progressController.NormalizedProgress, SpeedMultiplier);
		}

		public async UniTask StopAllBallsAsync(float duration)
		{
			float elapsedTime = 0f;

			float multiplier;
			while (elapsedTime < duration)
			{
				elapsedTime += timeProvider.DeltaTime;
				multiplier = Mathf.Lerp(1, 0, elapsedTime / duration);

				ChangeBallsSpeed(multiplier);
				await UniTask.Yield(PlayerLoopTiming.Update);
			}
		}

		public void ChangeBallsSpeed(float multiplier)
		{
			SpeedMultiplier = multiplier;
			foreach (var ball in pool.Active)
			{
				Vector2 direction = ball.Movement.Direction;
				ball.Movement.Push(direction, progressController.NormalizedProgress, multiplier);
			}
		}

		public void Reset()
		{
			List<Ball> balls = new List<Ball>();
			balls.AddRange(pool.Active);
			foreach (var ball in balls)
			{
				pool.Release(ball);

			}
		}

		public void PauseBalls()
		{
			lastBallsDirections = GetBallsDirections();
			foreach (Ball ball in pool.Active)
			{
				ball.Movement.Push(Vector2.zero);
			}
		}

		public void ResumeBalls()
		{
			List<Ball> balls = new List<Ball>(lastBallsDirections.Keys);
			foreach (var ball in balls)
			{
				ball.Movement.Push(lastBallsDirections[ball], progressController.NormalizedProgress);
			}
		}

		private Dictionary<Ball, Vector2> GetBallsDirections()
		{
			Dictionary<Ball, Vector2> ballsDirections = new();
			foreach (var ball in pool.Active)
			{
				ballsDirections.Add(ball, ball.Movement.Direction);
			}

			return ballsDirections;
		}
	}
}
