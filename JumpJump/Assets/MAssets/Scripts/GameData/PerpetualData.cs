
public class PerpetualData  {

	public int GetHighestScore(){
		return m_HighestScore;
	}

	public void SetHighestScore(int value){
		m_HighestScore = value;
		Save();
	}

	public int m_HighestScore = 0;

	private static void Save(){
		GameData.Instance().SavePerpetualData ();
	}
}
