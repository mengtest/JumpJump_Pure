using UnityEngine;
using System.Collections;
using TMPro;

public class GameOverPanel : UIWindow {
	public TextMeshProUGUI finalScoreText;

	public void SetFinalScoreText(string score){
		finalScoreText.text = score;
	}

	public void ToMenuScreen(){

		DelayAction delay1 = new DelayAction (GameController.GetInstance ().GetGameOverPanel().DisplayTime,()=>{
			GameController.GetInstance ().GetPlayPanel().ShowOut ();
			GameController.GetInstance ().GetGameOverPanel().ShowOut ();
		},()=>{
			GameController.GetInstance ().GetMainPanel().ShowIn ();
		});
		delay1.Play ();
	}
	
	public void OnRestart(){

		DelayAction delay1 = new DelayAction (GameController.GetInstance ().GetGameOverPanel().DisplayTime,()=>{
			GameController.GetInstance ().GetGameOverPanel().ShowOut ();
		},()=>{
			GameController.GetInstance ().GetPlayGameInstance().OnResume ();
			GameController.GetInstance ().GetPlayGameInstance().OnReStart ();
			GameController.GetInstance ().GetPlayPanel ().Start ();
		});
		delay1.Play ();
	}
}