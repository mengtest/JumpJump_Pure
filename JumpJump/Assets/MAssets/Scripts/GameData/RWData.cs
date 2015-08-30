using UnityEngine;
using System;
using LitJson;

public class RWData
{
	public RWData ()
	{
	}
	
	public string ToJsonString ()
	{
		return JsonMapper.ToJson (this);
	}
	
	public void Save (string prefsKey)
	{
		PlayerPrefs.SetString(prefsKey,this.ToJsonString());
	}

	public static T Load<T> (string prefsKey) where T:new()
	{
		string sDStr = PlayerPrefs.GetString (prefsKey);
		if (sDStr != null && sDStr != "")
			return JsonMapper.ToObject<T> (sDStr);
		return new T ();
	}
	
}


