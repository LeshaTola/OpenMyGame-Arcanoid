using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Health
{
	public class ReduceHealthComponent : General.Component
	{
		[SerializeField] private int damage;

		public override void Execute()
		{
			base.Execute();
			var healthComponent = Block.Config.GetComponent<HealthComponent>();
			healthComponent.ReduceHealth(damage);

		}
	}
}
