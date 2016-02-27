using UnityEngine;
using System.Collections;
using MStateMachine;

public class BaseGameEntity
{
	int m_ID;

	public BaseGameEntity (int id)
	{
		this.m_ID=id;
	}
	
	public int getId(){
		return m_ID;
	}
		
	
	public virtual void Update ()
	{
		
	}
	
	public virtual void handleMessage(Message message){
	}
	
	public virtual void Init(){
		
	}
	
	public virtual void Reset(){
		
	}
	
	public virtual void Start(){
	}
		
	
}

	

