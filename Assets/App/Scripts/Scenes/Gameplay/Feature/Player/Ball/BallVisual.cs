using Scenes.Gameplay.Feature.Player.Configs;
using Scenes.Gameplay.Feature.RageMode.Entities;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Ball
{
	public class BallVisual : MonoBehaviour, IEnraged
	{
		[SerializeField] private SpriteRenderer spriteRenderer;
		[SerializeField] private BallVisualConfig config;
		[SerializeField] private TrailRenderer trail;

		public void ActivateRageMode()
		{
			spriteRenderer.sprite = config.RageSprite;
			trail.gameObject.SetActive(true);
		}

		public void DeactivateRageMode()
		{
			spriteRenderer.sprite = config.DefaultSprite;
			trail.gameObject.SetActive(false);
		}
	}
}
