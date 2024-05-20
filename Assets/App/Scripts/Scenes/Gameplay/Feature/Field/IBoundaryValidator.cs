using System;

namespace Scenes.Gameplay.Feature.Field
{
	public interface IBoundaryValidator
	{
		event Action OnBallFall;
		event Action OnLastBallFall;

		void ValidateBalls();
	}
}