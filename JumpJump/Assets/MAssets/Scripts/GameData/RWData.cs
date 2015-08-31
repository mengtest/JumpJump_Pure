using UnityEngine;
using System;
using LitJson;

public class RWData
{
	
	public static void Save<T> (string prefsKey,T t)
	{
		PlayerPrefs.SetString(prefsKey,JsonMapper.ToJson (t));
	}

	public static T Load<T> (string prefsKey) where T:new()
	{
		string sDStr = PlayerPrefs.GetString (prefsKey);
		if (sDStr != null && sDStr != "")
			return JsonMapper.ToObject<T> (sDStr);
		return new T ();
	}
	
}


