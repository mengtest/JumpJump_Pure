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


	GameObject m_GO;

	public GameObject M_GO {
		get {
			return m_GO;
		}
	}

	public override void UpdateWorldPot ()
	{
		base.UpdateWorldPot();
		m_GO.transform.localPosition = M_CurPot;
	}

	public void Update ()
	{
		if (!M_Active)
			return;
		if (USETWEENER && M_ActiveMove)  M_Loc_CurPot = Vector3.Lerp (M_Loc_CurPot, M_Loc_EndPot, Time.deltaTime * 5f);

	}

	public void Reset ()
	{
		M_Time = 0;
		M_MoveDelay = 0;
		M_Parent=null;
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
