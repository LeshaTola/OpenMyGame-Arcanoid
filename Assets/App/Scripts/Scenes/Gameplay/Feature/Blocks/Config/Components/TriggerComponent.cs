using Scenes.Gameplay.Feature.Blocks.Config.Components.General;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components
{
	public class TriggerComponent : General.Component
	{
		[SerializeField] private List<IComponent> triggerComponents;

		public GameObject TriggerGameObject { get; set; }

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