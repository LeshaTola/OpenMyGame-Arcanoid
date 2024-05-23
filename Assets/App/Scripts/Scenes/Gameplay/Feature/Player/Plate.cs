using Features.StateMachine;
using Features.StateMachine.States;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.Player.Ball.Services;
using Scenes.Gameplay.Feature.Player.PlayerInput;
using Scenes.Gameplay.Feature.Progress;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Feature.Player
{
	public class Plate : MonoBehaviour, IUpdatable, IResetable
	{
		[SerializeField] private BoxCollider2D boxCollider;

		[SerializeField] private Transform ballPosition;

		private IFieldSizeProvider fieldController;
		private IInput input;
		private IMovement movement;
		private IBallService ballService;
		private IProgressController progressController;
		private List<Ball.Ball> connectedBalls = new();

		[Inject]
		public void Construct(IFieldSizeProvider fieldController,
						IInput input,
						IMovement movement,
						IBallService ballService,
						IProgressController progressController)
		{
			this.fieldController = fieldController;
			this.input = input;
			this.movement = movement;
			this.ballService = ballService;
			this.progressController = progressController;
		}

		public void PushBalls()
		{
			foreach (var ball in connectedBalls)
			{
				ball.transform.SetParent(null);
				ball.Movement.Push(Vector2.up, progressController.NormalizedProgress);
			}
			connectedBalls.Clear();
		}

		void IUpdatable.Update()
		{
			movement.ApplyDrag();
			Vector2 targetPosition = input.GetPosition();
			Vector2 direction;
			if (!targetPosition.Equals(default) && fieldController.GameField.IsValid(targetPosition))
			{
				direction = GetDirection(targetPosition);
				movement.Move(direction);
			}

			ClampPosition();
		}

		private Vector2 GetDirection(Vector2 targetPosition)
		{
			return new(targetPosition.x - transform.position.x, 0f);
		}

		private void ClampPosition()
		{
			var gameField = fieldController.GetGameField();
			var position = transform.position;
			float xPos = Mathf.Clamp(position.x, gameField.MinX + boxCollider.size.x / 2, gameField.MaxX - boxCollider.size.x / 2);
			position = new Vector2(xPos, position.y);
			transform.position = position;
		}

		void IResetable.Reset()
		{
			transform.position = new Vector2(0, transform.position.y);
			movement.Move(Vector2.zero);
			Ball.Ball ball = ballService.GetBall();
			connectedBalls.Add(ball);
			ball.transform.position = ballPosition.position;
			ball.transform.SetParent(transform);
		}
	}
}