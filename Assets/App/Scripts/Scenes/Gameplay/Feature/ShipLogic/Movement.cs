using App.Scripts.Features.StateMachine;
using App.Scripts.Module.TimeProvider;
using App.Scripts.Scenes.Gameplay.Feature.Field;
using App.Scripts.Scenes.Gameplay.Feature.PlayerInput;
using App.Scripts.Scenes.Gameplay.Feature.ShipLogic.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.ShipLogic
{
	public class Movement : MonoBehaviour, IUpdatable
	{
		[SerializeField] private MovementConfig config;
		[SerializeField] private Rigidbody2D rb;
		[SerializeField] private BoxCollider2D boxCollider;
		[SerializeField] private FieldController fieldController;

		private IInput input;
		private ITimeProvider timeProvider;

		public void Init(IInput input, ITimeProvider timeProvider)
		{
			this.input = input;
			this.timeProvider = timeProvider;
		}

		void IUpdatable.Update()
		{
			ApplyDrag();
			ClampPosition();

			Vector2 targetPosition = input.GetPosition();
			if (targetPosition.Equals(default))
			{
				return;
			}

			Vector2 direction = GetDirection(targetPosition);
			if (direction.magnitude < config.DeadZone)
			{
				rb.velocity = Vector2.zero;
				return;
			}

			rb.velocity = direction.normalized * config.Speed;
		}

		private Vector2 GetDirection(Vector2 targetPosition)
		{
			return new(targetPosition.x - transform.position.x, 0f);
		}

		private void ApplyDrag()
		{
			rb.velocity -= rb.velocity * timeProvider.DeltaTime * config.Drag;
		}

		private void ClampPosition()
		{
			var gameField = fieldController.GetGameFieldRect();
			float xPos = Mathf.Clamp(transform.position.x, gameField.MinX + boxCollider.size.x / 2, gameField.MaxX - boxCollider.size.x / 2);
			transform.position = new Vector2(xPos, transform.position.y);
		}
	}
}
