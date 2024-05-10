using App.Scripts.Scenes.Gameplay.Feature.Blocks;
using App.Scripts.Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using App.Scripts.Scenes.Gameplay.Feature.Blocks.Config.Components.Score;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Feature.Score
{
	public class ScoreController : MonoBehaviour
	{
		private List<Block> scoredBlocks;
		private int startBlocksCount;
		private int currentBlocksCount;

		public void Init(List<Block> blocks)
		{
			scoredBlocks = blocks.Where(x => x.Config.GetComponent<ScoreComponent>() != null).ToList();

			foreach (var block in scoredBlocks)
			{
				var healthComponent = block.Config.GetComponent<HealthComponent>();
				if (healthComponent != null)
				{
					healthComponent.OnDeath += OnBlockDeath;
				}
			}

			startBlocksCount = blocks.Count;
			currentBlocksCount = startBlocksCount;
		}

		private void OnBlockDeath(Block obj)
		{
			scoredBlocks.Remove(obj);
		}
	}
}
