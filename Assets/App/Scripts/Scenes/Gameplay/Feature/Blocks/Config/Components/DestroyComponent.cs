using App.Scripts.Scenes.Gameplay.Feature.Blocks.Config.Components.General;

namespace App.Scripts.Scenes.Gameplay.Feature.Blocks.Config.Components
{
	public class DestroyComponent : Component
	{
		public override void Execute()
		{
			UnityEngine.GameObject.Destroy(Block.gameObject);
		}
	}
}
