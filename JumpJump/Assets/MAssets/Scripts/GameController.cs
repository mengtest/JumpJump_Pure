using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	private static GameController instance;

	public PlaySceneController playSceneController;
	public MainPanel mainPanel;
	public PlayPanel playPanel;
	public PausePanel pausePanel;

	void Awake ()
	{
		instance = this;
		pausePanel.Init ();
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
