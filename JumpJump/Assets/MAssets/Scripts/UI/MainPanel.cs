using UnityEngine;
using System.Collections;

public class MainPanel : UIWindow {
	public void Play(){
		PlayGameInstance.INSTANCE.OnStart ();
		GameController.GetInstance ().mainPanel.ShowOut ();
		GameController.GetInstance ().playPanel.ShowIn ();
	}

}
