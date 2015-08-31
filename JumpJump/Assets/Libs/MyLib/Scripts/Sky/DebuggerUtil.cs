using UnityEngine;
using System.Collections;

public class DebuggerUtil : MonoBehaviour {

	public const  int ALL = 0;
	public const int DEBUG =1;
	public const int INFO =2;
	public const int WARN =3;
	public const int ERROR =4;
	public const int FATAL =5;
	public const int OFF = 100;
	
	static public int DEBUG_LEVEL = ALL;
	
	static public void Log(object message)
	{
		Log(message,null);
	}
	static public void Log(object message, Object context)
	{
		if(DEBUG>=DEBUG_LEVEL)
		{
			Debug.Log(message,context);
		}
	}
	
	static public void LogInfo(object message)
	{
		LogInfo(message,null);
	}
	static public void LogInfo(object message, Object context)
	{
		if(INFO>=DEBUG_LEVEL)
		{
			Debug.Log(message,context);
		}
	}
	
	static public void LogWarning(object message)
	{
		LogWarning(message,null);
	}
	static public void LogWarning(object message, Object context)
	{
		if(WARN>=DEBUG_LEVEL)
		{
			Debug.LogWarning(message,context);
		}
	}
	
	
	static public void LogError(object message)
	{
		LogError(message,null);
	}
	static public void LogError(object message, Object context)
	{
		if(ERROR>=DEBUG_LEVEL)
		{
			Debug.LogError(message,context);
		}
	}
	
	static public void LogFatal(object message)
	{
		LogFatal(message,null);
	}
	static public void LogFatal(object message, Object context)
	{
		if(FATAL>=DEBUG_LEVEL)
		{
			Debug.LogError(message,context);
		}
	}
}
