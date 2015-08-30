using UnityEngine;
using System.Collections;
using TMPro;

public class PlayPanel : UIWindow {
  public TextMeshProUGUI scoreText;

  public void SetScore(string score){
		scoreText.text = score;
	}

	public void OnPause(){
		GameController.GetInstance ().pausePanel.ShowIn ();
		PlayGameInstance.INSTANCE.OnPause ();
		GameData.Instance().M_PerpetualData.SetHighestScore(25);
	}

	public void OnTouchDown(){
		PlayGameInstance.INSTANCE.OnTouchDown ();
	}

	void Update () {
		SetScore (GameData.Instance().M_RunningData.M_Score + "");

	}
}
