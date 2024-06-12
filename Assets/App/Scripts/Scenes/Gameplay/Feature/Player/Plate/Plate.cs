using DG.Tweening;
using Features.Saves.Gameplay.DTO.Plate;
using Features.StateMachine;
using Module.Saves.Structs;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.Player.Ball.Services;
using Scenes.Gameplay.Feature.Player.Machineguns;
using Scenes.Gameplay.Feature.Player.PlayerInput;
using Scenes.Gameplay.Feature.Progress;
using Scenes.Gameplay.Feature.Reset;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Feature.Player
{
	public class Plate : MonoBehaviour, IUpdatable, IResetable
	{
		[SerializeField] private BoxCollider2D boxCollider;
		[SerializeField] private Transform ballPosition;
		[SerializeField] private PlateVisual visual;
		[SerializeField] private Machinegun machinegun;

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
			AddConnectedBall(ball);
		}

		public void ChangeWidth(float multiplier, float duration = 0)
		{
			AnimateWidth(defaultWidth, defaultWidth * multiplier, duration);

			visual.ChangeWidth(multiplier, duration);
			machinegun.ChangeWidth(multiplier, duration);
		}

		public void ResetWidth(float duration = 0)
		{
			AnimateWidth(boxCollider.size.x, defaultWidth, duration);

			visual.ResetWidth(duration);
			machinegun.ResetWidth(duration);
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
			movement.Stop();
		}

		void IUpdatable.Update()
		{
			Vector2 targetPosition = input.GetPosition();
			if (!targetPosition.Equals(default) && fieldController.GameField.IsValid(targetPosition))
			{
				Vector2 direction = new(targetPosition.x, transform.position.y);
				movement.Move(direction, SpeedMultiplier);
			}
			else
			{
				movement.ApplyDrag();
			}

			ClampPosition();
		}

		public PlateState GetPlateState()
		{
			return new PlateState
			{
				Position = new JsonVector2()
				{
					X = transform.position.x,
					Y = transform.position.y
				},
				BallsLocalPositions = connectedBalls.Select(x => new JsonVector2(x.transform.localPosition)).ToList(),
			};
		}

		public void SetPlateState(PlateState state)
		{
			transform.position = new Vector2(state.Position.X, state.Position.Y);
			foreach (JsonVector2 ballPosition in state.BallsLocalPositions)
			{
				Ball.Ball ball = ballService.GetBall();
				AddConnectedBall(ball);
				ball.transform.localPosition = new(ballPosition.X, ballPosition.Y);
			}
		}

		private void AddConnectedBall(Ball.Ball ball)
		{
			connectedBalls.Add(ball);
			ball.transform.SetParent(transform);
			ball.Movement.Rb.simulated = false;
		}

		private void AnimateWidth(float from, float to, float duration = 0)
		{
			DOVirtual.Float(from, to, duration, value =>
			{
				boxCollider.size = new Vector2(value, boxCollider.size.y);
			});
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