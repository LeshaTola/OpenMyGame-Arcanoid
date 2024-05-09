using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.Blocks.Config
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
	}
}
