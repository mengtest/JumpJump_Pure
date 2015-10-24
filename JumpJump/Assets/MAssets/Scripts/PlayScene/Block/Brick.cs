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

	[SerializeField]
	GameObject
		m_GO;

	public GameObject M_GO {
		get {
			return m_GO;
		}set {
			m_GO = value;
		}
	}

	public override void UpdateWorldPot ()
	{
		base.UpdateWorldPot ();
//		m_GO.transform.localPosition = M_CurPot;

//		if (!IS_EDITOR_UPDATEPOT)
//			m_GO.transform.position = M_CurPot;
	}

	void Update ()
	{

	}

	public void OnUpdate ()
	{
		if (!M_Active)
			return;
		if (!USETWEENER && M_ActiveMove)  
			M_Loc_CurPot = Vector3.Lerp (M_Loc_CurPot, M_Loc_EndPot, Time.deltaTime * 5f);
		
		//		Debug.Log (" curPot=" + M_CurPot + " startPot=" + M_Loc_StartPot + " endpot=" + M_Loc_EndPot);

	}

	public void IReset ()
	{
//		M_Time = 0;
//		M_MoveDelay = 0;
//		M_Parent = null;
//		m_GO.SetActive (false);

		Reset();

	}

	public void IDestory ()
	{
		if (m_GO != null) {
			GameObject.Destroy (m_GO);
			m_GO = null;
		}
	}

	void OnDrawGizmosSelected ()
	{
		Color c = Color.red;
		c.a = 0.2f;
		Gizmos.color = c;
		UpdateWorldPot ();
		Gizmos.DrawCube (M_EndPot, Vector3.one);
	}

	public override void Delete ()
	{
		if (M_Parent != null) {
			((Block)M_Parent).DeleteBrick (this);
		}
	}
	
}
