using UnityEngine;
using System.Collections;
using MStateMachine;

public class State<T>
{
	public State ()
	{
	}
		
	public virtual void Execute (T  t)
	{
	}
	
	public virtual void Enter (T  t)
	{
		
	}
	
	public virtual void Exit (T  t)
	{
	}
	
	public virtual bool OnMessage (T t, Message message)
	{
		return false;
	}
		
	public virtual string ToString(T t){
		return "";
	}
		
}




