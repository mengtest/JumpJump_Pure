using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public  class ObjectPool<T> where T :IPoolable
{
	int initSize = 20;
	int addSize = 10;
	List<T> objects;

	public delegate  T CreateObject ();

	public CreateObject  NewObject;
	
	public ObjectPool (int initSize, int addSize)
	{
		this.initSize = initSize;
		this.addSize = addSize;
		objects = new List<T> ();
	}

	public void Init(){
		InitSize(initSize);
	}

	void InitSize (int size)
	{
		for (int i=0; i<size; i++) {
			T t = NewObject ();
			objects.Add (t);
		}
	}
	
	public T Obtain ()
	{
		if (objects.Count < 1) {
			InitSize (addSize);
		}
		int index = objects.Count - 1;
		T t = objects [index];
		objects.RemoveAt (index);
		((IPoolable)t).Reset ();
		return t;
	}
	
	public void Free (T t)
	{
		t.Reset ();
		objects.Add (t);
	}
	
	public void DestoryAll ()
	{
	
		for (int i=0; i<objects.Count; i++) {
			objects [i].Destory ();
		}
		objects.Clear ();
	}
	
	public void ResetAll ()
	{
		for (int i=0; i<objects.Count; i++) {
			objects [i].Reset ();
		}
	}
	
}

public interface IPoolable
{
	void Reset ();

	void  Destory ();
}