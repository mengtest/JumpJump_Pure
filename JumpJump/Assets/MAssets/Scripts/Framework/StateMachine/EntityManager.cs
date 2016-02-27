using UnityEngine;
using System.Collections;

public class EntityManager
{
	
	Hashtable entityMap = new Hashtable ();

	public EntityManager ()
	{
	}
	
	public void registerEntity (BaseGameEntity baseGameEntity)
	{
		entityMap.Add (baseGameEntity.getId (), baseGameEntity);
		
	}

	public void removeEntity (BaseGameEntity baseGameEntity)
	{
		entityMap.Remove (baseGameEntity);
	}
	
	public BaseGameEntity getEntityFromId (int id)
	{
		return (BaseGameEntity)entityMap[id];
	}
	
	public void Update(){
		foreach(DictionaryEntry e in entityMap){
			((BaseGameEntity)e.Value).Update();
		}
	}
	
	public void Reset(){
		foreach(DictionaryEntry e in entityMap){
			((BaseGameEntity)e.Value).Reset();
		}
	}
	
	public void OnStart(){
			foreach(DictionaryEntry e in entityMap){
			((BaseGameEntity)e.Value).Start();
		}
	}
	
	public int GetEntityNum(){
		return entityMap.Count;
	}
	
}

	

