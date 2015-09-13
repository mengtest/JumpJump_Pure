using UnityEngine;
using System.Collections;

public class Brick :  Object3d, IPoolable
{
	public const float WIDTH = 1f;
	public const float HEIGHT = 1f;

	public Brick (GameObject go)
	{
		this.m_GO = go;
	}

	Block m_Block;

	public Block M_Block {
		get {
			return m_Block;
		}
		set {
			m_Block = value;
			UpdateWorldPot ();
		}
	}

	GameObject m_GO;

	public GameObject M_GO {
		get {
			return m_GO;
		}
	}

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

	Vector3 m_CurPot;

	public Vector3 M_CurPot {
		get {
			return m_CurPot;
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

	float m_Time = 0;
	float m_MoveDelay = 0;

	public float M_MoveDelay {
		get {
			return m_MoveDelay;
		}
		set {
			m_MoveDelay = value;
		}
	}

	public void UpdateWorldPot ()
	{
		if (m_Block != null) {
			m_StartPot = m_Block.M_StartPot + m_Loc_StartPot;
			m_EndPot = m_Block.M_EndPot + m_Loc_EndPot;
			m_CurPot = m_Block.M_CurPot + m_Loc_CurPot;
			m_GO.transform.localPosition = m_CurPot;
		} else {
			m_StartPot = m_Loc_StartPot;
			m_EndPot = m_Loc_EndPot;
			m_CurPot = m_Loc_CurPot;
			m_GO.transform.localPosition = m_CurPot;
		}
	}

	public void Update ()
	{
		if (m_Time >= m_MoveDelay) {
//			M_Loc_CurPot = Vector3.Lerp (m_Loc_StartPot, m_Loc_EndPot, Time.deltaTime * 5f);
			m_Loc_CurPot = Vector3.Lerp (m_Loc_StartPot, m_Loc_EndPot, Time.deltaTime * 5f);
		} else {
			m_Time += Time.deltaTime;
		}

	}

	public void Reset ()
	{
		m_Time = 0;
		m_MoveDelay = 0;
		m_Block = null;
		m_GO.SetActive (false);
	}

	public void Destory ()
	{
		if (m_GO != null) {
			GameObject.Destroy (m_GO);
			m_GO = null;
		}
	}
}
