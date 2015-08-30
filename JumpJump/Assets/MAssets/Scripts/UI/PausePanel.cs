using UnityEngine;
using System.Collections;

public class PausePanel : UIWindow {
	public void OnPlay(){
		GameController.GetInstance ().pausePanel.ShowOut ();
		PlayGameInstance.INSTANCE.OnResume ();
	}
	
	public void ToMenuScreen(){
		GameController.GetInstance ().mainPanel.ShowIn ();
		GameController.GetInstance ().playPanel.ShowOut ();
		GameController.GetInstance ().pausePanel.ShowOut ();
	}

	public void OnRestart(){
		GameController.GetInstance ().pausePanel.ShowOut ();
		PlayGameInstance.INSTANCE.OnResume ();
		PlayGameInstance.INSTANCE.OnReStart ();
	}

}
