using UnityEngine;
using System.Collections;
using TMPro;

public class PlayPanel : UIWindow {
  public TextMeshProUGUI scoreText;

  public void SetScore(string score){
		scoreText.text = score;
	}

	public void OnPause(){
		GameController.GetInstance ().GetPausePanel().ShowIn ();
		GameController.GetInstance ().GetPlayGameInstance().OnPause ();
	}

	public void OnTouchDown(){
		GameController.GetInstance ().GetPlayGameInstance().OnTouchDown ();
	}

	void Update () {
		SetScore (GameData.Instance().M_RunningData.M_Score + "");
	}
}
