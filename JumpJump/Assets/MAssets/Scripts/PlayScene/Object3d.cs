
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Object3d
{
	public const float MOVE_DELAY_TIME_Cell = 0.02F;

	#region direction
	protected int m_Dirction;
	public const int DIRICTION_LEFT = 1;
	public const int DIRICTION_RIGHT = 1 << 2;
	public const int DIRICTION_UP = 1 << 3;
	public const int DIRICTION_DOWN = 1 << 4;

	public static int GetDiriction (Vector3 startPot, Vector3 endPot)
	{
		int direction = 0;
		direction |= (startPot.x < endPot.x) ? DIRICTION_RIGHT : 0;
		direction |= (startPot.x > endPot.x) ? DIRICTION_LEFT : 0;
		direction |= (startPot.y < endPot.y) ? DIRICTION_UP : 0;
		direction |= (startPot.y > endPot.y) ? DIRICTION_DOWN : 0;
		
		return direction;
		
	}
	#endregion

	#region menber
	private Object3d m_Parent;
	
	public Object3d M_Parent {
		get {
			return m_Parent;
		}
		set {
			m_Parent = value;
			UpdateWorldPot ();
		}
	}

	Vector3 m_StartPot;

	public Vector3 M_StartPot {
		get {
			return m_StartPot;
		}
		set {
			m_StartPot = value;
		}
	}

	Vector3 m_EndPot;

	public Vector3 M_EndPot {
		get {
			return m_EndPot;
		}
		set {
			m_EndPot = value;
		}
	}

	Vector3 m_CurPot;

	public Vector3 M_CurPot {
		get {
			return m_CurPot;
		}
		set {
			m_CurPot = value;
		}
	}

	Vector3 m_Loc_StartPot;

	public Vector3 M_Loc_StartPot {
		get {
			return m_Loc_StartPot;
		}
		set {
			m_Loc_StartPot = value;
			UpdateWorldPot ();
		}
	}

	Vector3 m_Loc_EndPot;

	public Vector3 M_Loc_EndPot {
		get {
			return m_Loc_EndPot;
		}
		set {
			m_Loc_EndPot = value;
			UpdateWorldPot ();
		}
	}

	Vector3 m_Loc_CurPot;

	public Vector3 M_Loc_CurPot {
		get {
			return m_Loc_CurPot;
		}
		set {
			m_Loc_CurPot = value;
			UpdateWorldPot ();
		}
	}

	float m_MoveDelay = 0f;

	public float M_MoveDelay {
		get {
			return m_MoveDelay;
		}
		set {
			m_MoveDelay = value;
		}
	}

	float m_MoveDuration = 0f;

	public float M_MoveDuration {
		get {
			return m_MoveDuration;
		}
		set {
			m_MoveDuration = value;
		}
	}

	float m_Time = 0;

	public float M_Time {
		get {
			return m_Time;
		}
		set {
			m_Time = value;
		}
	}

	bool m_ActiveMove = false;
	
	public bool M_ActiveMove {
		get {
			return m_ActiveMove;
		}
		set {
			m_ActiveMove = value;
		}
	}
	
	bool m_Active = false;
	
	public bool M_Active {
		get {
			return m_Active;
		}
		set {
			m_Active = value;
		}
	}
	#endregion

	#region tweener

	public const bool USETWEENER=true;
	public Tweener DoPosition (Vector3 endValue, float duration, float delay=0)
	{
		return DOTween.To (() => this.M_Loc_CurPot, delegate (Vector3 x) {
			this.M_Loc_CurPot = x;
		}, endValue, duration).SetDelay (delay).SetTarget (this);
	}

	public bool IsMoveOver ()
	{
		return Vector3.SqrMagnitude (M_Loc_CurPot - M_Loc_EndPot) < 0.1f * 0.1f;
	}
	#endregion
	
	public Object3d ()
	{

	}

	
	public Object3d (Object3d parent, Vector3 locStartPot, Vector3 locEndPot, float moveDelay, float moveDuration=1f)
	{
		this.m_Parent = parent;
		this.m_Loc_StartPot = locStartPot;
		this.m_Loc_EndPot = locEndPot;
		this.m_CurPot = locStartPot;
		this.m_MoveDelay = moveDelay;
		this.m_MoveDuration = moveDuration;
		this.m_Dirction = GetDiriction (locStartPot, locEndPot);
	}


	public virtual void UpdateWorldPot ()
	{
		if (m_Parent != null) {
			m_StartPot = m_Parent.M_StartPot + m_Loc_StartPot;
			m_EndPot = m_Parent.M_EndPot + m_Loc_EndPot;
			m_CurPot = m_Parent.M_CurPot + m_Loc_CurPot;
		} else {
			m_StartPot = m_Loc_StartPot;
			m_EndPot = m_Loc_EndPot;
			m_CurPot = m_Loc_CurPot;
		}

	}

	public virtual void UpdateLocPot ()
	{
	}
	

	#region condition
	public delegate bool delegate_ActiveMoveCondition ();
	
	public delegate_ActiveMoveCondition ActiveMoveCondition;
	
	public void CheckActiveMove ()
	{
		if ((ActiveMoveCondition == null || ActiveMoveCondition ()) && CheckDelayTime()) {
			if (!m_ActiveMove) {
				ActiveMove ();
				m_ActiveMove=true;
			}
		} 
		CheckActiveMove_Child ();
	}

	public virtual void CheckActiveMove_Child ()
	{

	}
	
	public virtual void ActiveMove ()
	{
		DoPosition(M_Loc_EndPot,M_MoveDuration,M_MoveDelay);
	}

	public bool CheckDelayTime ()
	{
		return (m_Time += Time.deltaTime) >= m_MoveDelay;

	}
	
	public delegate bool delegate_ActiveCondition ();
	
	public delegate_ActiveCondition ActiveCondition;
	
	public void CheckActive ()
	{
		if (ActiveCondition == null || ActiveCondition ()) {
			if (!m_Active) {
				Active ();
				m_Active = true;
			}
		} 
		CheckActive_Child ();
	}

	public virtual void CheckActive_Child ()
	{

	}

	public virtual void Active ()
	{

	}
	#endregion




}


