using System;

namespace Features.UI.SceneTransitions
{
	public interface ISceneTransition
	{
		public void PlayOn(Action action = null);
		public void PlayOff(Action action = null);
	}
}