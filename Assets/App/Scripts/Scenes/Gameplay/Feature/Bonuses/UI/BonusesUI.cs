using Scenes.Gameplay.Feature.Bonuses.Commands;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.UI
{
	public class BonusesUI : MonoBehaviour, IBonusesUI
	{
		private Dictionary<IBonusCommand, BonusTimerUI> timers = new();

		public void AddTimer(IBonusCommand command, BonusTimerUI timer)
		{
			timer.UpdateSprite(command.Config.Sprite);
			timers.Add(command, timer);
		}

		public void UpdateTimer(IBonusCommand command)
		{
			float normalizedValue = command.Timer / command.Config.Duration;
			timers[command].UpdateTimer(normalizedValue);
		}

		public BonusTimerUI RemoveTimer(IBonusCommand command)
		{
			BonusTimerUI removedTimer = timers[command];
			timers.Remove(command);
			return removedTimer;
		}

	}
}
