using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaySceneController : MonoBehaviour
{


	PlayerController pC;
	
	public PlayerController PC {
		get { return pC;}
	}
	
	public Block block_Root;
	public Block block_Root_Eidtor;
	public float moveDelayTime_Unit = 0.1f;
	public float moveDurationTime_Unit = 0.2f;
	public float ConditionTime_Unit = 0.1f;
	public bool isEditMode = false;
	LvMgr lvMgr;





	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
		if (GameState.Instance ().M_PlayState == PlayState.PLAY) {
			lvMgr.Update ();
			GameData.Instance ().M_RunningData.M_Score = (int)pC.transform.position.x;
		}
	}

	void Awake ()
	{

		Init ();

	}

	void Init ()
	{
		pC = this.gameObject.GetComponentInChildren<PlayerController> ();
		pC.Init();
		pC.gameObject.SetActive (false);
		PlayGameInstance.INSTANCE = new PlayGameInstance (this);

		lvMgr = new LvMgr ();
		lvMgr.LoadBlock_Prefabs ();
		lvMgr.CreateBlcoks ();
		lvMgr.Init (pC, isEditMode ? block_Root_Eidtor : block_Root, isEditMode);
		ResourceMgr.Instance ().LoadResource_Play ();

		block_Root_Eidtor.gameObject.SetActive (isEditMode);
		block_Root.gameObject.SetActive (!isEditMode);


	}

	void Prepare ()
	{
		
	}
	
	void StartGame ()
	{
		pC.gameObject.SetActive (true);
		lvMgr.Start ();
	}

	void PauseGame ()
	{

	}

	void ResumeGame ()
	{

	}

	public void OnStart ()
	{
		Time.timeScale = 1f;

		pC.gameObject.SetActive (true);
		pC.Reset();
		lvMgr.Start ();

		GameState.Instance ().M_PlayState = PlayState.PLAY;
		GameData.Instance ().M_RunningData.M_RoleState = "Normal";

		block_Root_Eidtor.gameObject.SetActive (isEditMode);
		block_Root.gameObject.SetActive (!isEditMode);
	}
	
	public void OnPause ()
	{
		Time.timeScale = 0f;
		PauseGame ();
		GameState.Instance ().M_PlayState = PlayState.PAUSE;
		GameData.Instance ().M_RunningData.M_RoleState = "Normal";
	}
	
	public void OnResume ()
	{
		Time.timeScale = 1f;
		ResumeGame ();
		GameState.Instance ().M_PlayState = PlayState.PLAY;
	}
	
	public void OnRestart ()
	{
		Time.timeScale = 1f;
		pC.gameObject.SetActive (true);
		pC.Reset ();
		lvMgr.Reset ();
		GameState.Instance ().M_PlayState = PlayState.PLAY;
		GameData.Instance ().M_RunningData.M_RoleState = "Normal";
//		StartGame ();

	}
	
	public void OnGameOver ()
	{
		
	}

	public void OnRoleChange (int id)
	{
		pC.RoleChange (id);
	}

	public void OnRoleRevive ()
	{

	}

}
