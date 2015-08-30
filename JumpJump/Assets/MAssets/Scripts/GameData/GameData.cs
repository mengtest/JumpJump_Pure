using LitJson;
using UnityEngine;
using System;

public class GameData
{


//	public static GameData Instance() {
//			if(_Instance == null){
//				_Instance = new GameData();
//			}
//			return _Instance;
//	}
//	
//	private static GameData _Instance;
//
//	private GameData(){
//	}
//
//	private static string SETTING_DATA = "SettingData";
//	public SettingData M_SettingData {
//		get{
//			if(m_SettingData == null){
//				m_SettingData = RWData.Load<SettingData>(SETTING_DATA);
//			}
//			return m_SettingData;
//		}
//		set
//		{
//			m_SettingData= value;
//		}
//	}
//	private SettingData m_SettingData;
//
//	public void SaveSettingData(){
//		RWData.Save<SettingData> (SETTING_DATA,M_SettingData);
//	}
//
//	private static string PERPETUAL_DATA = "PerpetualData";
//	public PerpetualData M_PerpetualData {
//		get{
//			if(m_PerpetualData == null){
//				m_PerpetualData = RWData.Load<PerpetualData>(PERPETUAL_DATA);
//			}
//			return m_PerpetualData;
//		}
//		set
//		{
//			m_PerpetualData= value;
//		}
//	}
//	private PerpetualData m_PerpetualData;
//
//	public void SavePerpetualData(){
//		Debug.Log (JsonMapper.ToJson (M_PerpetualData));
//		RWData.Save<PerpetualData> (PERPETUAL_DATA,M_PerpetualData);
//	}
//
//
//	public RunningData M_RunningData {
//		get{
//			if(m_RunningData == null){
//				m_RunningData = new RunningData();
//			}
//			return m_RunningData;
//		}
//		set
//		{
//			m_RunningData= value;
//		}
//	}
//	private RunningData m_RunningData;

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
		Debug.Log (JsonMapper.ToJson (M_PerpetualData));
		RWData.Save<PerpetualData> (PerpetualData.PREFSKEY, M_PerpetualData);
	}

	public void SaveFlush(){
		PlayerPrefs.Save ();
	}



}
