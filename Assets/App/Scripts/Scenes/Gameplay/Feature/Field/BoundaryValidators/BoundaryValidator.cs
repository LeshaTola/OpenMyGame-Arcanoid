using Module.ObjectPool;
using Scenes.Gameplay.Feature.Player.Ball;
using System;
using System.Collections.Generic;
using Zenject;

namespace Scenes.Gameplay.Feature.Field
{
	public class BoundaryValidator : IBoundaryValidator, ITickable
	{
		public event Action OnBallFall;
		public event Action OnLastBallFall;

		private IPool<Ball> ballsPool;
		private IFieldSizeProvider fieldSizeProvider;
		private List<Ball> ballsToRemove;

		public BoundaryValidator(IPool<Ball> ballsPool, IFieldSizeProvider fieldSizeProvider)
		{
			this.ballsPool = ballsPool;
			this.fieldSizeProvider = fieldSizeProvider;
			ballsToRemove = new();
		}

		public void Tick()
		{
			ValidateBalls();
		}

		public void ValidateBalls()
		{
			GetFalledBalls();

			if (ballsToRemove.Count <= 0)
			{
				return;
			}

			foreach (Ball ball in ballsToRemove)
			{
				ball.Release();
			}

			ballsToRemove.Clear();
			OnBallFall?.Invoke();

			if (ballsPool.Active.Count == 0)
			{
				OnLastBallFall?.Invoke();
			}
		}

		private void GetFalledBalls()
		{
			foreach (Ball ball in ballsPool.Active)
			{
				if (ball.transform.position.y < fieldSizeProvider.GameField.MinY)
				{
					ballsToRemove.Add(ball);
				}
			}
		}
	}
}
