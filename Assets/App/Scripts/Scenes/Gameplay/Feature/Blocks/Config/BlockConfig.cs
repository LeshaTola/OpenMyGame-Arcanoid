using Scenes.Gameplay.Feature.Blocks.Config.Components.General;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config
{
	[CreateAssetMenu(fileName = "BlockConfig", menuName = "Configs/Block")]
	public class BlockConfig : SerializedScriptableObject
	{
		[SerializeField] private string blockName;
		[SerializeField] private Sprite sprite;
		[SerializeField] private Color color;
		[SerializeField] private List<IComponent> components;

		public Sprite Sprite => sprite;
		public Color Color { get => color; }
		public List<IComponent> Components => components;
		public string BlockName { get => blockName; }

		public void Init(Block block)
		{
			foreach (IComponent component in Components)
			{
				component.Init(block);
			}
		}

		public T GetComponent<T>(List<IComponent> components) where T : IComponent
		{
			return (T)components.FirstOrDefault(x => x is T);
		}

		public T GetComponent<T>() where T : IComponent
		{
			return GetComponent<T>(components);
		}

		public void AddComponentIfNull<T>(T component, List<IComponent> components) where T : IComponent
		{
			T foundComponent = GetComponent<T>(components);
			if (foundComponent == null)
			{
				components.Add(component);
			}
		}
	}
}
