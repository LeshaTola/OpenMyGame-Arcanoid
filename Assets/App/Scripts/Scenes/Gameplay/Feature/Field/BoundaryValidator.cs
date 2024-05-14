using Features.StateMachine;
using Scenes.Gameplay.Feature.Player.Ball;
using Scenes.Gameplay.StateMachine.States;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Field
{
	public class BoundaryValidator : MonoBehaviour, IUpdatable
	{
		[SerializeField] private FieldController fieldController;
		[SerializeField] private BallsController ballsController;
		[SerializeField] private MonoBehStateMachine stateMachine;

		private List<Ball> ballsToRemove = new();

		void IUpdatable.Update()
		{
			ValidateBalls();
		}

		private void ValidateBalls()
		{
			foreach (Ball ball in ballsController.BallPool.Active)
			{
				if (ball.transform.position.y < fieldController.GameField.MinY)
				{
					ballsToRemove.Add(ball);
				}
			}
			if (ballsToRemove.Count > 0)
			{
				foreach (Ball ball in ballsToRemove)
				{
					ball.Release();
				}
				ballsToRemove.Clear();

				stateMachine.Core.ChangeState<ResetState>();
			}
		}
	}
}
