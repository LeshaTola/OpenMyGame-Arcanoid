using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands
{
	public class BonusConfig
	{
		[PreviewField]
		[SerializeField]
		private Sprite sprite;

		[PreviewField]
		[SerializeField]
		private Sprite blockSprite;

		[SerializeField] private float duration;
		[SerializeField] private List<string> conflicts;

		public Sprite Sprite { get => sprite; }
		public Sprite BlockSprite { get => blockSprite; }
		public float Duration { get => duration; }
		public List<string> Conflicts => conflicts;
	}

	public abstract class BonusCommand : IBonusCommand
	{
		public string Id { get; private set; }
		public float Timer { get; set; }
		public abstract BonusConfig Config { get; }

		public void Init(string id)
		{
			Id = id;
		}

		public virtual void StartBonus()
		{
			Timer = Config.Duration;
		}

		public virtual void StopBonus()
		{
		}
	}
}
