using Scenes.Gameplay.Feature.Blocks.Config.Components.General;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components
{
	public class DestroyComponent : Component
	{
		public override void Execute()
		{
			UnityEngine.GameObject.Destroy(Block.gameObject);
		}
	}
}
