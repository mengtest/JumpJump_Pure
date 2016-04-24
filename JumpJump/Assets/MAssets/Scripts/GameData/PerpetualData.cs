
public class PerpetualData
{
	public const string PREFSKEY = "PerpetualData";
	public int m_HighestScore = 0;

	public int m_Coins = 0;

	public void SetHighestScore (int value)
	{
		m_HighestScore = value;
	}

	public void AddCoins(int addVaule){
		m_Coins += addVaule;
	}
}
