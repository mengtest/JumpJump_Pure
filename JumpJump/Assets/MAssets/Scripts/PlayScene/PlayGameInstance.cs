using UnityEngine;
using System.Collections;

public class PlayGameInstance :IPlayGame {

	public static PlayGameInstance INSTANCE;
	PlaySceneController pSC;
	public PlaySceneController PSC {
		get { return pSC;}
		set { pSC = value;}
	}
	public delegate void OnGameResult_Delegate ();
	public OnGameResult_Delegate  OnGameResultDelegate;


	public delegate void UI_Delegate();
	public UI_Delegate OnMagnetEndTip;
	public UI_Delegate OnMagnetEnd;
	public UI_Delegate OnCurFuncEndTip;
	public UI_Delegate OnCurFuncEnd;
	public UI_Delegate OnGetCoin;
	
	public void OnGameResult ()
	{
		OnPause();
		
		if (OnGameResultDelegate != null)
			OnGameResultDelegate ();
	}
	
	public PlayGameInstance (PlaySceneController pSC)
	{
		this.pSC = pSC;
		INSTANCE = this;
		GameState.Init();
	}

	public void Init(){
		GameState.Init();
		GameData.Load();
	}
	
	public void OnStart ()
	{
		pSC.OnStart ();

	}
	
	public void OnReStart ()
	{
		pSC.OnRestart ();
	}
	
	public void OnPause ()
	{
		pSC.OnPause ();
	}
	
	public  void OnResume ()
	{
		pSC.OnResume ();
	}
	
	public void OnVectory ()
	{
		
	}
	
	public void OnFailed ()
	{
		
	}
	
	public void OnTouchDown(){
		pSC.PC.OnTouchDownScreen();
	}

	public void OnSkill_SpeedUp ()
	{
		pSC.PC.OnSkill_SpeedUp();
	}
	
	public void OnSkill_SlownDown ()
	{
		pSC.PC.OnSkill_SlownDown();
	}

	public void OnRoleSeleted(int id){
		int lastId=GameData.Instance().M_SettingData.m_LastSelectedRoleId;
		if(lastId!=id){
			GameData.Instance().M_SettingData.m_LastSelectedRoleId=id;
			pSC.OnRoleChange(id);
		}
	}

	public void OnRoleRevive(){
		pSC.OnRoleRevive();
	}

	public void AddCoin(int num){
		GameData.Instance().M_SettingData.m_Coin+=num;
	}

}
