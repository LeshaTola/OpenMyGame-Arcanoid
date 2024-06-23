using Module.AI.Actions;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Autopilot.Configs
{
	[CreateAssetMenu(fileName = "ActionsDatabase", menuName = "Dictionaries/Actions")]
	public class ActionsDatabase : SerializedScriptableObject
	{
		[SerializeField] List<IAction> actions = new();

		public List<IAction> Actions { get => actions; }
	}
}
