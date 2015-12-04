using UnityEngine;
using System.Collections;
using TMPro;

public class GameOverPanel : UIWindow {
	public TextMeshProUGUI finalScoreText;

	public void SetFinalScoreText(string score){
		finalScoreText.text = score;
	}

	public void ToMenuScreen(){
		GameController.GetInstance ().GetMainPanel().ShowIn ();
		GameController.GetInstance ().GetPlayPanel().ShowOut ();
		GameController.GetInstance ().GetGameOverPanel().ShowOut ();
	}
	
	public void OnRestart(){
		GameController.GetInstance ().GetGameOverPanel().ShowOut ();
		GameController.GetInstance ().GetPlayGameInstance().OnResume ();
		GameController.GetInstance ().GetPlayGameInstance().OnReStart ();
		GameController.GetInstance ().GetPlayPanel ().Start ();
	}
}