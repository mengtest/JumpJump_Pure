using UnityEngine;
using System.Collections;

public class PausePanel : UIWindow {
	public void OnPlay(){
		GameController.GetInstance ().GetPausePanel().ShowOut ();
		GameController.GetInstance ().GetPlayGameInstance().OnResume ();
	}
	
	public void ToMenuScreen(){
		GameController.GetInstance ().GetMainPanel().ShowIn ();
		GameController.GetInstance ().GetPlayPanel().ShowOut ();
		GameController.GetInstance ().GetPausePanel().ShowOut ();
	}

	public void OnRestart(){
		GameController.GetInstance ().GetPausePanel().ShowOut ();
		GameController.GetInstance ().GetPlayGameInstance().OnResume ();
		GameController.GetInstance ().GetPlayGameInstance().OnReStart ();
	}

}
