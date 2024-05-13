using Scenes.Gameplay.Feature.Blocks.Config.Components.General;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Health
{
	public class ReduceHealthComponent : Component
	{
		[UnityEngine.SerializeField] private int damage;

		public override void Execute()
		{
			base.Execute();
			var healthComponent = Block.Config.GetComponent<HealthComponent>();
			healthComponent.ReduceHealth(damage);
		}
	}
}
