using Cysharp.Threading.Tasks;

namespace Scenes.Gameplay.Feature.Player.Ball.Services
{
	public interface IBallService
	{
		float SpeedMultiplier { get; }

		void ChangeBallsSpeed(float multiplier);
		Ball GetBall();
		void PauseBalls();
		void ReleaseBall(Ball ball);
		void Reset();
		void ResumeBalls();
		UniTask StopAllBallsAsync(float duration);
		void ActivateRageMode();
		void DeactivateRageMode();
	}
}