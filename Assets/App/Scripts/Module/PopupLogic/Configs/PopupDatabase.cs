using Module.PopupLogic.General.Popup;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Module.PopupLogic.Configs
{
	[CreateAssetMenu(fileName = "PopupDatabase", menuName = "Dictionaries/Popup")]
	public class PopupDatabase : SerializedScriptableObject
	{
		[SerializeField] private List<IPopup> popups;

		public List<IPopup> Popups { get => popups; }
	}
}