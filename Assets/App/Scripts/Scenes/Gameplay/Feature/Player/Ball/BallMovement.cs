using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace App.Scripts.Scenes.Gameplay.Feature.Player.Ball
{
    public class BallMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float minAngle;
        [SerializeField] private Rigidbody2D  rb;

        private Vector2[] axes = 
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };
        
        public void Push(Vector2 direction)
        {
            direction = ValidateDirection(direction);
            rb.velocity = Vector2.zero;
            rb.AddForce(direction * speed,ForceMode2D.Impulse);
        }

        private Vector2 ValidateDirection(Vector2 direction)
        {
            foreach(Vector2 axis in axes)
            {
                float angle = Vector2.Angle(direction, axis);
                if (angle < minAngle)
                {
                    float rotateAngle = minAngle - angle;
                    direction = Quaternion.Euler(0, 0, rotateAngle) * direction;
                }
            }
            return direction;
        }
    }
}