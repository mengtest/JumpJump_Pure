using LitJson;
using UnityEngine;
using System;

public class GameData  {


	public static GameData Instance() {
			if(_Instance == null){
				_Instance = new GameData();
			}
			return _Instance;
	}
	
	private static GameData _Instance;

	private GameData(){
	}

	private static string SETTING_DATA = "SettingData";
	public SettingData M_SettingData {
		get{
			if(m_SettingData == null){
				m_SettingData = RWData.Load<SettingData>(SETTING_DATA);
			}
			return m_SettingData;
		}
		set
		{
			m_SettingData= value;
		}
	}
	private SettingData m_SettingData;

	public void SaveSettingData(){
		RWData.Save<SettingData> (SETTING_DATA,M_SettingData);
	}

	private static string PERPETUAL_DATA = "PerpetualData";
	public PerpetualData M_PerpetualData {
		get{
			if(m_PerpetualData == null){
				m_PerpetualData = RWData.Load<PerpetualData>(PERPETUAL_DATA);
			}
			return m_PerpetualData;
		}
		set
		{
			m_PerpetualData= value;
		}
	}
	private PerpetualData m_PerpetualData;

	public void SavePerpetualData(){
		Debug.Log (JsonMapper.ToJson (M_PerpetualData));
		RWData.Save<PerpetualData> (PERPETUAL_DATA,M_PerpetualData);
	}


	public RunningData M_RunningData {
		get{
			if(m_RunningData == null){
				m_RunningData = new RunningData();
			}
			return m_RunningData;
		}
		set
		{
			m_RunningData= value;
		}
	}
	private RunningData m_RunningData;



}
