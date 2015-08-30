using UnityEngine;
using System.Collections;
using TMPro;

public class GameOverPanel : UIWindow {
	public TextMeshProUGUI finalScoreText;

	public void SetFinalScoreText(string score){
		finalScoreText.text = score;
	}

	public void ToMenuScreen(){
		GameController.GetInstance ().mainPanel.ShowIn ();
		GameController.GetInstance ().playPanel.ShowOut ();
		GameController.GetInstance ().gameOverPanel.ShowOut ();
	}
	
	public void OnRestart(){
		GameController.GetInstance ().gameOverPanel.ShowOut ();
		PlayGameInstance.INSTANCE.OnResume ();
		PlayGameInstance.INSTANCE.OnReStart ();
	}
}