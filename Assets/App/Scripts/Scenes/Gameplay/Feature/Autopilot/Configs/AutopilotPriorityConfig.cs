using Scenes.Gameplay.Feature.Bonuses.Configs;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Autopilot.Configs
{
	[CreateAssetMenu(fileName = "AutopilotPriorityConfig", menuName = "Dictionaries/Autopilot/Priorates")]
	public class AutopilotPriorityConfig : SerializedScriptableObject
	{
		[SerializeField] private BonusesDatabase bonusesDatabase;
		[SerializeField] private int ballPriority;
		[SerializeField] private List<PriorityCommand> priorities;

		public List<PriorityCommand> Priorities { get => priorities; }
		public int BallPriority { get => ballPriority; }

		private void OnValidate()
		{
			foreach (var priority in priorities)
			{
				priority.SetBonusesDatabase(bonusesDatabase);
			}
		}
	}
}
