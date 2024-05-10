using System;
using App.Scripts.Features.StateMachine;
using App.Scripts.Scenes.Gameplay.Feature.Field;
using App.Scripts.Scenes.Gameplay.StateMachine.States;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.Player.Ball
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private FieldController fieldController;
        [SerializeField] private BallMovement movement;

        public BallMovement Movement => movement;

        private void Awake()
        {
            movement.Push(Vector2.up);
        }

        private void Update()
        {
            //GameField  gameField = fieldController.GetGameFieldRect();
            //if(transform.position.x>)
        }

        private void OnCollisionEnter(Collision collision)
        {
            //movement.Push(collision.contacts[0].point);
        }
    }
}
