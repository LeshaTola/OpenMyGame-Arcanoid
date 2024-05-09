using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.Blocks.Config
{
	public class BlockConfig : SerializedScriptableObject
	{
		[SerializeField] private Sprite sprite;
		[SerializeField] private List<IComponent> components;

		public Sprite Sprite => sprite;

		public List<IComponent> Components => components;
	}

	public interface IComponent
	{
		public void Execute();
	}
	
	public abstract class Component : IComponent
	{
		public virtual void Execute()
		{
		}
	}

	public class HealthComponent : Component
	{
		[SerializeField] private int health;

		public override void Execute()
		{
			base.Execute();
			ReduceHealth(1);
		}

		private void ReduceHealth(int health)
		{
			this.health -= health;

			if (health == 0)
			{

			}
		}
	}
}
