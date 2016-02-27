using System;
using MStateMachine;

public class BaseWorld
{
	public static EntityManager entityManager;
	public static MessageDispatcher messageDispatcher;
	public static float g_gravity=10;
	
	public BaseWorld ()
	{
		
		InitInstance ();
		
	}
	
	private void InitInstance ()
	{
		entityManager = new EntityManager ();
		messageDispatcher = new MessageDispatcher ();
	}
	
	public void Update ()
	{
		messageDispatcher.Update ();
		entityManager.Update ();
		
	}
	
	public BaseGameEntity GetEnity(int id){
		return entityManager.getEntityFromId(id);
	}
	
	public void test(){

	}

	
}


