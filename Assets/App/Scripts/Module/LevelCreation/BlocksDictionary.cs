using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
namespace Module.LevelCreation
{
	[CreateAssetMenu(fileName = "BlocksDictionary", menuName = "Dictionaries/Blocks")]
	public class BlocksDictionary : SerializedScriptableObject
	{
		[SerializeField] private Dictionary<int, Color> blocks = new();

		public Dictionary<int, Color> Blocks { get => blocks; }
		public List<int> IDs
		{
			get
			{
				var ids = new List<int>(Blocks.Keys);
				ids.Sort();
				return ids;
			}
		}
	}
}
