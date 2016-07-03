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
	[SerializeField]
	private Button VedioButton;
	[SerializeField]
	private TextMeshProUGUI CoinsNumber;

	public void Play ()
	{

		DelayAction delay1 = new DelayAction (GameController.GetInstance ().GetGameOverPanel().DisplayTime,()=>{
			GameController.GetInstance ().GetMainPanel ().ShowOut ();
			GameController.GetInstance ().GetPlayPanel ().ShowIn ();
		},()=>{
				GameController.GetInstance ().GetPlayGameInstance ().OnStart ();
				GameController.GetInstance ().GetPlayPanel ().Start ();
		});
		delay1.Play ();
	}

	void Update ()
	{
		if (SGConfig.Instant.isLoad) {
			VedioButton.gameObject.SetActive (true);
		} else {
			VedioButton.gameObject.SetActive (false);
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
