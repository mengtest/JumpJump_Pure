using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	private static GameController instance;

	public PlaySceneController playSceneController;
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

	public GameController GetInstance(){
		return instance;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnStart(){

//		playSceneController.OnStart ();
		mainPanel.ShowOut ();
		playPanel.ShowIn ();
	}


	public void OnPause(){
		pausePanel.ShowIn ();
		playSceneController.OnPause ();
	}
	

	public void OnPlay(){
		pausePanel.ShowOut ();
		playSceneController.OnResume ();
	}

	public void ToMenuScreen(){
		mainPanel.ShowIn ();
		playPanel.ShowOut ();
		pausePanel.ShowOut ();
	}



}
