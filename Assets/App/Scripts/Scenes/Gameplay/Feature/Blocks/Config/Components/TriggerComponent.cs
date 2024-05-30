using Scenes.Gameplay.Feature.Blocks.Config.Components.General;
using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components
{
	public class TriggerComponent : Component
	{
		[UnityEngine.SerializeField] private List<IComponent> triggerComponents;

		public override void Init(Block block)
		{
			base.Init(block);
			foreach (var component in triggerComponents)
			{
				component.Init(block);
			}
		}

		override public void Execute()
		{
			foreach (var component in triggerComponents)
			{
				component.Execute();
			}
		}
	}
}