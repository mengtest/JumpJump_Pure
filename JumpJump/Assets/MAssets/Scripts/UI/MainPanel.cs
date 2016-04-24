using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using Supergood.Unity;
using Supergood.Unity.Ad;

using UnityEngine.SocialPlatforms.GameCenter; 
using UnityEngine.SocialPlatforms;

using TMPro;

public class MainPanel : UIWindow
{

	public Button VedioButton;
	public TextMeshProUGUI CoinsNumber;

	public void Play ()
	{
		GameController.GetInstance ().GetPlayGameInstance ().OnStart ();
		GameController.GetInstance ().GetMainPanel ().ShowOut ();
		GameController.GetInstance ().GetPlayPanel ().ShowIn ();
		GameController.GetInstance ().GetPlayPanel ().Start ();
	}

	void Update ()
	{
		if (SGConfig.Instant.isLoad) {
			VedioButton.interactable = AdManager.instant.VideoIsLoad ();
		}
		CoinsNumber.text = GameData.Instance ().M_PerpetualData.m_Coins.ToString ();
	}

	public void ShowVideo(){
		AdManager.instant.ShowVideo ();
	}

	public void ShowLearBroad ()
	{
		
		//GameCenterPlatform.ShowLeaderboardUI (GPID.leaderboard_,TimeScope.Today);
		Social.Active.ShowLeaderboardUI ();
	}
	
//	public void GameCenterButtonPressed ()
//	{
//		Social.ReportScore (-5, GPID.leaderboard_, HandleScoreReported);
//		
//	}

}
