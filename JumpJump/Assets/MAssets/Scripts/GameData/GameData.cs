public class GameData  {


	public static GameData Instance {
		get{
			if(_Instance == null){
				_Instance = new GameData();
			}
			return _Instance;
		}
		set
		{
			_Instance= value;
		}
	}
	
	private static GameData _Instance;

	public SettingData MSettingData {
		get{
			if(_MSettingData == null){
				_MSettingData = new SettingData();
			}
			return _MSettingData;
		}
		set
		{
			_MSettingData= value;
		}
	}
	private SettingData _MSettingData;


	public PerpetualData MPerpetualData {
		get{
			if(_MPerpetualData == null){
				_MPerpetualData = new PerpetualData();
			}
			return _MPerpetualData;
		}
		set
		{
			_MPerpetualData= value;
		}
	}
	private PerpetualData _MPerpetualData;



	public RunningData MRunningData {
		get{
			if(_MRunningData == null){
				_MRunningData = new RunningData();
			}
			return _MRunningData;
		}
		set
		{
			_MRunningData= value;
		}
	}
	private RunningData _MRunningData;

}
