using Module.ObjectPool;
using Scenes.Gameplay.Feature.Bonuses;
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
		private IPool<Bonus> bonusesPool;
		private IFieldSizeProvider fieldSizeProvider;

		private List<Ball> ballsToRemove;
		private List<Bonus> bonusesToRemove;

		public BoundaryValidator(IPool<Ball> ballsPool, IFieldSizeProvider fieldSizeProvider, IPool<Bonus> bonusesPool)
		{
			this.ballsPool = ballsPool;
			this.bonusesPool = bonusesPool;
			this.fieldSizeProvider = fieldSizeProvider;

			ballsToRemove = new();
			bonusesToRemove = new();
		}

		public void Tick()
		{
			ValidateBalls();
			ValidateBonuses();
		}

		private void ValidateBonuses()
		{
			GetFalledBonuses();

			foreach (Bonus bonus in bonusesToRemove)
			{
				bonusesPool.Release(bonus);
			}

			bonusesToRemove.Clear();
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

		private void GetFalledBonuses()
		{
			foreach (Bonus bonus in bonusesPool.Active)
			{
				if (bonus.transform.position.y < fieldSizeProvider.GameField.MinY)
				{
					bonusesToRemove.Add(bonus);
				}
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
