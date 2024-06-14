using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Ball.Services.AngleCorrector
{
	public interface IAngleCorrectorService
	{
		Vector2 GetCorrectAngle(Vector2 vector, float deflectionAngle);
	}
}