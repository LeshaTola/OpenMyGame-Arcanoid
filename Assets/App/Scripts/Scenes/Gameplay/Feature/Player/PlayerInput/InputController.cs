using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.PlayerInput
{
	public class InputController : MonoBehaviour
	{
		public IInput Input { get; private set; }

		public void Init(IInput input)
		{
			Input = input;
		}
	}
}
