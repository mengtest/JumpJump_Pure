using UnityEngine;
using System.Collections;
using TMPro;

public class GameOverPanel : UIWindow {
	public TextMeshProUGUI finalScoreText;

	public void SetFinalScoreText(string score){
		finalScoreText.text = score;
	}
}