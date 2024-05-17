using Features.StateMachine;
using Scenes.Gameplay.Feature.Health;
using Scenes.Gameplay.Feature.Player.Ball;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Feature.Field
{
	public class BoundaryValidator : MonoBehaviour, IUpdatable
	{
		public event Action OnBallFall;
		public event Action OnLastBallFall;

		[SerializeField] private HealthController healthController;
		[SerializeField] private BallsController ballsController;
		[SerializeField] private StateMachineHandler stateMachine;

		private List<Ball> ballsToRemove = new();
		private IFieldSizeProvider fieldSizeProvider;

		[Inject]
		public void Init(IFieldSizeProvider fieldSizeProvider)
		{
			this.fieldSizeProvider = fieldSizeProvider;
		}

		void IUpdatable.Update()
		{
			ValidateBalls();
		}

		private void ValidateBalls()
		{
			GetFalledBalls();

			if (ballsToRemove.Count <= 0)
			{
				return;
			}

			foreach (Ball ball in ballsToRemove)
			{
				ball.transform.parent = ballsController.transform;
				ball.Release();
			}

			ballsToRemove.Clear();
			OnBallFall?.Invoke();

			if (ballsController.BallPool.Active.Count == 0)
			{
				OnLastBallFall?.Invoke();
			}
		}

		private void GetFalledBalls()
		{
			foreach (Ball ball in ballsController.BallPool.Active)
			{
				if (ball.transform.position.y < fieldSizeProvider.GameField.MinY)
				{
					ballsToRemove.Add(ball);
				}
			}
		}
	}
}
