using System;
using Features.StateMachine;
using Features.StateMachine.States;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.Player.Configs;
using Scenes.Gameplay.Feature.Player.PlayerInput;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Player
{
    public class Player: MonoBehaviour, IUpdatable, IResetable
    {
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private FieldController fieldController;

        [SerializeField] private Movement movement;

        private IInput input;

        public void Init(IInput input)
        {
            this.input = input;
        }
        
        void IUpdatable.Update()
        {
            Vector2 targetPosition = input.GetPosition();
            Vector2 direction;
            if (!targetPosition.Equals(default))
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

        public void Reset()
        {
            transform.position = new Vector2(0,transform.position.y);
            movement.Move(Vector2.zero);
        }
    }
}