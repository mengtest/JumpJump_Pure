using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Brick :  Object3d, IPoolable
{
	public const float WIDTH = 1f;
	public const float HEIGHT = 1f;
	bool m_IsCoin = false;

	public bool M_IsCoin {
		get {
			return m_IsCoin;
		}
		set {
			m_IsCoin = value;
			if(m_IsCoin){
				GetComponent<Renderer> ().material = ResourceMgr.Instance ().m_BrickMtls [ResourceMgr.MTL_ID_COIN];
			}
		}
	}

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

	}

	public void IReset ()
	{
		Reset ();
		Rigidbody rg = GetComponent<Rigidbody> ();
		if (rg != null)
			GameObject.Destroy (rg);
		transform.localRotation = Quaternion.identity;
		GetComponent<Collider> ().material = ResourceMgr.Instance ().m_Ice_PMtl;
		if (m_IsCoin) {
			GetComponent<Renderer> ().material = ResourceMgr.Instance ().m_BrickMtls [0];
		}
		m_IsCoin = false;
	}

	public void IDestory ()
	{
		if (m_GO != null) {
			GameObject.Destroy (m_GO);
			m_GO = null;
		}
	}

	Color red = Color.red;

	void OnDrawGizmosSelected ()
	{
		red.a = 0.2f;
		Gizmos.color = red;
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
