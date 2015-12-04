using UnityEngine;
using System.Collections;

public class MainPanel : UIWindow {
	public void Play(){
		GameController.GetInstance ().GetPlayGameInstance().OnStart ();
		GameController.GetInstance ().GetMainPanel().ShowOut ();
		GameController.GetInstance ().GetPlayPanel().ShowIn ();
		GameController.GetInstance ().GetPlayPanel ().Start ();
	}

}
