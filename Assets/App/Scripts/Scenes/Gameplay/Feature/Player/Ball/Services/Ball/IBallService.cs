﻿using Cysharp.Threading.Tasks;
using Features.Saves.Gameplay.DTO.Balls;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Ball.Services
{
	public interface IBallService
	{
		float SpeedMultiplier { get; }

		Ball GetBall();
		void ReleaseBall(Ball ball);

		void PushBalls();
		void PushBalls(Dictionary<Ball, Vector2> ballsDirections);
		void StopBalls();
		UniTask StopBallsAsync(float duration);

		void ChangeBallsSpeed(float multiplier);
		BallsServiceState GetBallServiceState();
		void SetBallServiceState(BallsServiceState state);
		void Reset();
	}
}