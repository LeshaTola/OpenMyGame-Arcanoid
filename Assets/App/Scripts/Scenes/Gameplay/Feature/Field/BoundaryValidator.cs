using Features.StateMachine;
using Scenes.Gameplay.Feature.Player.Ball;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Field
{
	public class BoundaryValidator : MonoBehaviour, IUpdatable
	{
		[SerializeField] private FieldController fieldController;
		[SerializeField] private BallsController ballsController;

		void IUpdatable.Update()
		{
			foreach (Ball ball in ballsController.BallPool.Active)
			{
				if (ball.transform.position.y < fieldController.GameField.MinY)
				{
					ball.Release();
					ballsController.BallPool.Get();
				}
			}
		}
	}
}
