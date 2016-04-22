using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using Supergood.Unity;
using Supergood.Unity.Ad;

public class MainPanel : UIWindow
{

	public Button VedioButton;

	public void Play ()
	{
		GameController.GetInstance ().GetPlayGameInstance ().OnStart ();
		GameController.GetInstance ().GetMainPanel ().ShowOut ();
		GameController.GetInstance ().GetPlayPanel ().ShowIn ();
		GameController.GetInstance ().GetPlayPanel ().Start ();
	}

	void Update ()
	{
		if (SGConfig.Instant.isLoad) {
			VedioButton.interactable = AdManager.instant.VideoIsLoad ();
		}
	}

}
