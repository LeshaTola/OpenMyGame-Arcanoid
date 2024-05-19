﻿using Scenes.PackSelection.Feature.Packs.Configs;
using Scenes.PackSelection.Feature.Packs.UI;

namespace Scenes.PackSelection.Feature.Packs
{
	public interface IPackFactory
	{
		PackUI GetPackUI(Pack pack);
	}
}