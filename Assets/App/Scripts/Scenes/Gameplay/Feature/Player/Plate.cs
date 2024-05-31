using Features.StateMachine;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.Player.Ball.Services;
using Scenes.Gameplay.Feature.Player.PlayerInput;
using Scenes.Gameplay.Feature.Progress;
using Scenes.Gameplay.Feature.Reset;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Feature.Player
{
	public class Plate : MonoBehaviour, IUpdatable, IResetable
	{
		[SerializeField] private BoxCollider2D boxCollider;
		[SerializeField] private Transform ballPosition;
		[SerializeField] private PlateVisual visual;

		private IFieldSizeProvider fieldController;
		private IInput input;
		private IMovement movement;
		private IBallService ballService;
		private IProgressController progressController;
		private List<Ball.Ball> connectedBalls = new();


		private float defaultWidth;

		public float SpeedMultiplier { get; set; } = 1;
		public bool IsSticky { get; set; } = false;

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

			defaultWidth = boxCollider.size.x;
			visual.Init();
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (!IsSticky || !collision.gameObject.TryGetComponent(out Ball.Ball ball))
			{
				return;
			}
			connectedBalls.Add(ball);
			ball.transform.SetParent(transform);
			ball.Movement.Rb.simulated = false;
		}


		public void ChangeWidth(float multiplier)
		{
			boxCollider.size = new Vector2(defaultWidth * multiplier, boxCollider.size.y);
			visual.ChangeWidth(multiplier);
		}

		public void ResetWidth()
		{
			boxCollider.size = new Vector2(defaultWidth, boxCollider.size.y);
			visual.ResetWidth();
		}

		public void PushBalls()
		{
			foreach (var ball in connectedBalls)
			{
				ball.Movement.Rb.simulated = true;
				ball.transform.SetParent(null);
				ball.Movement.Push(Vector2.up, progressController.NormalizedProgress);
			}
			connectedBalls.Clear();
		}

		public void Stop()
		{
			movement.Move(Vector2.zero);
		}

		void IUpdatable.Update()
		{
			movement.ApplyDrag();
			Vector2 targetPosition = input.GetPosition();
			Vector2 direction;
			if (!targetPosition.Equals(default) && fieldController.GameField.IsValid(targetPosition))
			{
				direction = GetDirection(targetPosition);
				movement.Move(direction, SpeedMultiplier);
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
			if (connectedBalls.Count > 0)
			{
				PushBalls();
			}

			ballService.Reset();
			transform.position = new Vector2(0, transform.position.y);
			Stop();
			Ball.Ball ball = ballService.GetBall();
			ball.Movement.Rb.simulated = false;
			connectedBalls.Add(ball);
			ball.transform.position = ballPosition.position;
			ball.transform.SetParent(transform);
		}
	}
}