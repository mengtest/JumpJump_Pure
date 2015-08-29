using UnityEngine;
using System.Collections;

public class PerpetualData  {
	private static string HIGEST_SCORE = "HighestScore";
	public int HighestScore {
		get{ 
			return SharedPrefs.GetInt(HIGEST_SCORE);
		}
		set{
			SharedPrefs.SaveInt(HIGEST_SCORE,value);
		}
	}
}
