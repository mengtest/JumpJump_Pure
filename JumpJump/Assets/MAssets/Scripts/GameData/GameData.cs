using LitJson;
using UnityEngine;
using System;

public class GameData
{
	private static GameData g_Instance;

	private  PerpetualData m_PerpetualData;
	public PerpetualData M_PerpetualData {
		get {
			return m_PerpetualData;
		}
	}

	private  SettingData m_SettingData;
	public SettingData M_SettingData {
		get {
			return m_SettingData;
		}
	}

	private RunningData m_RunningData;
	public RunningData M_RunningData {
		get {
			return m_RunningData;

		}
	}

	public static void Load(){
		Instance();
	}

	public static GameData Instance ()
	{
		if (g_Instance == null) {
			g_Instance = new GameData ();
		}
		return g_Instance;
	}
	
	private GameData ()
	{
		LoadData ();
	}

	private void LoadData ()
	{
		m_PerpetualData = RWData.Load<PerpetualData> (PerpetualData.PREFSKEY);
		m_SettingData  = RWData.Load<SettingData> (SettingData.PREFSKEY);
		m_RunningData = new RunningData ();
	}

	public void Save ()
	{
		SaveSettingData();
		SavePerpetualData();
		SaveFlush();
	}

	
	public void SaveSettingData ()
	{
		RWData.Save<SettingData> (SettingData.PREFSKEY, M_SettingData);
	}
	
	public void SavePerpetualData ()
	{
		RWData.Save<PerpetualData> (PerpetualData.PREFSKEY, M_PerpetualData);
	}

	public void SaveFlush(){
		PlayerPrefs.Save ();
	}



}
