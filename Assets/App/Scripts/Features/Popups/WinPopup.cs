using Module.PopupLogic.General;
using TNRD;
using UnityEngine;

public class WinPopup : MonoBehaviour, IPopup
{
	[SerializeField] private SerializableInterface<IPopupAnimation> popupAnimation;
	public bool IsActive { get; private set; }

	public void Init()
	{
		Hide();
	}

	public void Activate()
	{
	}

	public void Deactivate()
	{
	}

	public void Hide()
	{
		IsActive = false;
		popupAnimation.Value.Hide(() =>
		{
			Deactivate();
			gameObject.SetActive(false);
		});
	}

	public void Show()
	{
		gameObject.SetActive(true);
		popupAnimation.Value.Show(() =>
		{
			Activate();
			IsActive = true;
		});

	}
}

