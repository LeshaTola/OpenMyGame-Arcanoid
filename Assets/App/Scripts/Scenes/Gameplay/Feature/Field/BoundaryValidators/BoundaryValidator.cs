using Module.ObjectPool;
using Scenes.Gameplay.Feature.Bonuses;
using Scenes.Gameplay.Feature.Player.Ball;
using Scenes.Gameplay.Feature.Player.Machineguns.Bullets;
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
		private IPool<Bullet> bulletPool;
		private IPool<Bonus> bonusesPool;
		private IFieldSizeProvider fieldSizeProvider;

		private List<Ball> ballsToRemove;
		private List<Bullet> bulletsToRemove;
		private List<Bonus> bonusesToRemove;

		public BoundaryValidator(IPool<Ball> ballsPool,
						   IPool<Bullet> bulletPool,
						   IFieldSizeProvider fieldSizeProvider,
						   IPool<Bonus> bonusesPool)
		{
			this.ballsPool = ballsPool;
			this.bulletPool = bulletPool;
			this.bonusesPool = bonusesPool;
			this.fieldSizeProvider = fieldSizeProvider;

			ballsToRemove = new();
			bulletsToRemove = new();
			bonusesToRemove = new();
		}

		public void Tick()
		{
			ValidateBalls();
			ValidateBullets();
			ValidateBonuses();
		}

		private void ValidateBullets()
		{
			foreach (var bullet in bulletPool.Active)
			{
				if (!fieldSizeProvider.GameField.IsValid(bullet.transform.position))
				{
					bulletsToRemove.Add(bullet);
				}
			}

			foreach (var bullet in bulletsToRemove)
			{
				bullet.Release();
			}
			bulletsToRemove.Clear();
		}

		private void ValidateBonuses()
		{
			GetFalledBonuses();

			foreach (Bonus bonus in bonusesToRemove)
			{
				bonus.Release();
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
