using UnityEngine;
using System.Collections;

public class DebuggerUtil : MonoBehaviour {

	public enum DebugLevel  {
		ALL = 0,
		DEBUG =1,
		INFO =2,
		WARN =3,
		EXCEPTION = 4,
		ERROR =5,
		FATAL =6,
		OFF = 100
	}

	static public DebugLevel DEBUG_LEVEL = DebugLevel.ALL;
	
	static public void Log(object message)
	{
		Log(message,null);
	}
	static public void Log(object message, Object context)
	{
		if(DebugLevel.DEBUG>=DEBUG_LEVEL)
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
		if(DebugLevel.INFO>=DEBUG_LEVEL)
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
		if(DebugLevel.WARN>=DEBUG_LEVEL)
		{
			Debug.LogWarning(message,context);
		}
	}

	static public void LogException(System.Exception exception)
	{
		LogException(exception,null);
	}
	static public void LogException(System.Exception exception, Object context)
	{
		if(DebugLevel.EXCEPTION>=DEBUG_LEVEL)
		{
			Debug.LogException(exception,context);
		}
	}
	
	
	static public void LogError(object message)
	{
		LogError(message,null);
	}
	static public void LogError(object message, Object context)
	{
		if(DebugLevel.ERROR>=DEBUG_LEVEL)
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
		if(DebugLevel.FATAL>=DEBUG_LEVEL)
		{
			Debug.LogError(message,context);
		}
	}
}
