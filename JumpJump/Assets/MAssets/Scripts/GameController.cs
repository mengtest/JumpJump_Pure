using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	private static GameController instance;
	
	public MainPanel mainPanel;
	public PlayPanel playPanel;
	public PausePanel pausePanel;
	public GameOverPanel gameOverPanel;

	void Awake ()
	{
		instance = this;
		Init ();
	}

	void Init(){

		mainPanel.gameObject.SetActive (true);
		playPanel.gameObject.SetActive (false);
		pausePanel.gameObject.SetActive (false);
		gameOverPanel.gameObject.SetActive (false);
	}

	public static GameController GetInstance(){
		return instance;
	}

	// Use this for initialization
	void Start () {
		PlayGameInstance.INSTANCE.OnGameResultDelegate += OnGameOver;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void OnGameOver(){
		gameOverPanel.ShowIn ();
		gameOverPanel.SetFinalScoreText (GameData.Instance().M_RunningData.M_Score + "");
	}



}
