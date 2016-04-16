using UnityEngine;
using System.Collections;
using Supergood.Unity;
using Supergood.Unity.Ad;

public class GameController : MonoBehaviour {


	private static GameController instance;
	
	private MainPanel mainPanel;
	private PlayPanel playPanel;
	private PausePanel pausePanel;
	private GameOverPanel gameOverPanel;
	public int targetFrameRate = 60;
	void Awake ()
	{
		instance = this;
		Init ();
		DebuggerUtil.DEBUG_LEVEL = DebuggerUtil.DebugLevel.ALL;
		DebuggerUtil.Log (" targetFrameRate : " + Application.targetFrameRate);
		Application.targetFrameRate = targetFrameRate;
		SGCross.Init ();
	}

	void Init(){
		mainPanel =  GameObjectTools.GetComponentInChildren <MainPanel> (gameObject);
		playPanel = GameObjectTools.GetComponentInChildren<PlayPanel> (gameObject);
		pausePanel = GameObjectTools.GetComponentInChildren<PausePanel> (gameObject);
		gameOverPanel = GameObjectTools.GetComponentInChildren<GameOverPanel> (gameObject);

		mainPanel.gameObject.SetActive (true);
		playPanel.gameObject.SetActive (false);
		pausePanel.gameObject.SetActive (false);
		gameOverPanel.gameObject.SetActive (false);

		mainPanel.Init ();
		playPanel.Init ();
		pausePanel.Init ();
		gameOverPanel.Init ();
	}

	public static GameController GetInstance(){
		return instance;
	}

	// Use this for initialization
	void Start () {
//		PlayGameInstance_old.INSTANCE.OnGameResultDelegate += OnGameOver;
		PlayGameInstance.INSTANCE.OnGameResultDelegate += OnGameOver;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	public PlayGameInstance_old GetPlayGameInstance(){
//		return PlayGameInstance_old.INSTANCE;
//	}

	public PlayGameInstance GetPlayGameInstance(){
		return PlayGameInstance.INSTANCE;
	}
	
	private void OnGameOver(){
		playPanel.Pause ();
		gameOverPanel.ShowIn ();
		gameOverPanel.SetFinalScoreText (GameData.Instance().M_RunningData.M_Score + "");
		AdManager.instant.ShowFull ();
	}

	public MainPanel GetMainPanel(){
		return mainPanel;
	}

	public PlayPanel GetPlayPanel(){
		return playPanel;
	}

	public PausePanel GetPausePanel(){
		return pausePanel;
	}

	public GameOverPanel GetGameOverPanel(){
		return gameOverPanel;
	}


}
