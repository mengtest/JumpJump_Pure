
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Object3d
{
	Vector3 m_StartPot;

	public Vector3 M_StartPot {
		get {
			return m_StartPot;
		}
	}

	Vector3 m_EndPot;

	public Vector3 M_EndPot {
		get {
			return m_EndPot;
		}
	}

	Vector3 m_curPot;

	public Vector3 M_curPot {
		get {
			return m_curPot;
		}
	}

	Vector3 m_Loc_StartPot;

	public Vector3 M_Loc_StartPot {
		get {
			return m_Loc_StartPot;
		}
		set {
			m_Loc_StartPot = value;
			UpdateWorldPot();
		}
	}

	Vector3 m_Loc_EndPot;

	public Vector3 M_Loc_EndPot {
		get {
			return m_Loc_EndPot;
		}
		set {
			m_Loc_EndPot = value;
			UpdateWorldPot();
		}
	}

	Vector3 m_Loc_CurPot;

	public Vector3 M_Loc_CurPot {
		get {
			return m_Loc_CurPot;
		}
		set {
			m_Loc_CurPot = value;
			UpdateWorldPot();
		}
	}

	public Object3d ()
	{

	}

	public virtual void UpdateWorldPot(){

	}

	public virtual void UpdateLocPot(){
	}


}


