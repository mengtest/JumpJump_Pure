using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PropertyMachine<Entity>
{

	Entity owner;
	List<Property<Entity>>  propertys;
	List<Property<Entity>> removePropertys;
	
	public PropertyMachine ()
	{
		
	}
	
	public PropertyMachine (Entity owner)
	{
		this.owner = owner;
		propertys = new List<Property<Entity>> ();
		removePropertys=new List<Property<Entity>>();
	}
		
	public void Update ()
	{
		removePropertys.Clear();
		for (int i=0; i<propertys.Count; i++) {
			propertys [i].Execute (owner);// can be delete the property by the owner
		}
		for(int i=0;i<removePropertys.Count;i++){
			propertys.Remove(removePropertys[i]);
			removePropertys[i].Exit (owner);
		}
	}
	
	public void AddProperty (Property<Entity> property)
	{
		bool isContains = propertys.Contains (property);
		if (!isContains)
			propertys.Add (property);
		property.Enter (owner);
	}
	
	public void RemoveProperty (Property<Entity> property)
	{
		removePropertys.Add (property);
		
	}
	
	public  void RemoveAllProperty(){
		removePropertys.Clear();
		propertys.Clear();
	}
	
	public void Init(){
		RemoveAllProperty();
	}
}
