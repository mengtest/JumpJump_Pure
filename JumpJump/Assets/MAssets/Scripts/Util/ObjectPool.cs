using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public  class ObjectPool<T> where T :IPoolable
{
	int m_InitSize = 20;
	int m_AddSize = 10;
	List<T> m_Objects;

	public delegate  T CreateObject ();

	public CreateObject  NewObject;
	
	public ObjectPool (int initSize, int addSize)
	{
		this.m_InitSize = initSize;
		this.m_AddSize = addSize;
		m_Objects = new List<T> ();
	}

	public void Init(){
		InitSize(m_InitSize);
	}

	void InitSize (int size)
	{
		for (int i=0; i<size; i++) {
			T t = NewObject ();
			m_Objects.Add (t);
		}
	}
	
	public T Obtain ()
	{
		if (m_Objects.Count < 1) {
			InitSize (m_AddSize);
		}
		int index = m_Objects.Count - 1;
		T t = m_Objects [index];
		m_Objects.RemoveAt (index);
		((IPoolable)t).IReset ();
		return t;
	}
	
	public void Free (T t)
	{
		t.IReset ();
		m_Objects.Add (t);
	}
	
	public void DestoryAll ()
	{
	
		for (int i=0; i<m_Objects.Count; i++) {
			m_Objects [i].IDestory ();
		}
		m_Objects.Clear ();
	}
	
	public void ResetAll ()
	{
		for (int i=0; i<m_Objects.Count; i++) {
			m_Objects [i].IReset ();
		}
	}
	
}

public interface IPoolable
{
	void IReset ();

	void  IDestory ();
}