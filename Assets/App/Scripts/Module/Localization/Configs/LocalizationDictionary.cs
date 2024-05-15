using System.Collections.Generic;
using UnityEngine;

namespace Module.Localization.Configs
{
	[CreateAssetMenu(fileName = "LocalizationDictionary", menuName = "Dictionaries/Localization")]
	public class LocalizationDictionary : ScriptableObject
	{
		[SerializeField] private List<Language> languages;

		public List<Language> Languages { get => languages; }
	}
}
