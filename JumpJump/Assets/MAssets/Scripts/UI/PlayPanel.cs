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
	}

	void Update () {
		SetScore (GameData.Instance.MRunningData.Score + "");
	}
}
