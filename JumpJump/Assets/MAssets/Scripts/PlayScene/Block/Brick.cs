using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Brick :  Object3d, IPoolable
{
	public const float WIDTH = 1f;
	public const float HEIGHT = 1f;
	bool m_IsCoin = false;
	bool m_IsCoinAbsorbed = false;
	bool m_AttachMagnet = false;
	MagnetController m_MagnetController;

	public bool M_AttachMagnet {
		get {
			return m_AttachMagnet;
		}
		set {
			m_AttachMagnet = value;
			if (m_AttachMagnet && m_MagnetController == null) {
				m_MagnetController = ResourceMgr.Instance ().GetMagnetController ();
				M3DUtil.SetItemVisible(m_MagnetController.gameObject,true,true);
				m_MagnetController.transform.parent = this.transform;
				M3DUtil.InitLocalTf (m_MagnetController.transform);
				Vector3 locPot=Vector3.zero;
				locPot.y+=1;
				m_MagnetController.transform.localPosition=locPot;
			}
		}
	}

	public int CurColumn {
		get { return (int)((M_CurPot.x + WIDTH * 0.5f) / WIDTH);}
	}

	public bool M_IsCoin {
		get {
			return m_IsCoin;
		}
		set {
			m_IsCoin = value;
			if (m_IsCoin) {
//				GetComponent<Renderer> ().material = ResourceMgr.Instance ().m_BrickMtls [ResourceMgr.MTL_ID_COIN];
				SetBrickChild (ResourceMgr.Instance ().CreateBrick_Child (ResourceMgr.TYPE_COIN));
			}
		}
	}

	bool m_OnUnbeatableCollided = false;

	public bool M_OnUnbeatableCollided {
		get {
			return m_OnUnbeatableCollided;
		}
		set {
			m_OnUnbeatableCollided = value;
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
		CheckInMagnetRange ();
	}

	void CheckInMagnetRange ()
	{
		if (! m_IsCoin || m_IsCoinAbsorbed)
			return;
		PlayerController pc = PlayGameInstance.INSTANCE.PSC.PC;
		Vector3 dstVec = pc.transform.position - this.transform.position;
		if (dstVec.sqrMagnitude < pc.m_MagnetRange * pc.m_MagnetRange) {
			m_IsCoinAbsorbed = true;
			ResourceMgr.Instance ().GetCoinController ().StartAnimation (this.transform.position);
			CoinBrickController cbc = this.GetComponentInChildren<CoinBrickController> ();
			if (cbc != null) {
				cbc.HideCoin ();
			}
//			GetComponent<Renderer> ().material = ResourceMgr.Instance ().m_BrickMtls [ResourceMgr.MTL_ID_EMPTY];
		}

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
//			GetComponent<Renderer> ().material = ResourceMgr.Instance ().m_BrickMtls [ResourceMgr.MTL_ID_EMPTY];
			SetBrickChild ();
		}
		if (m_MagnetController != null) {
			ResourceMgr.Instance ().FreeMagnetController (m_MagnetController);
			m_MagnetController = null;
		}
		m_AttachMagnet=false;
		m_IsCoin = false;
		m_IsCoinAbsorbed = false;
		m_OnUnbeatableCollided = false;
	
	}

	public void IDestory ()
	{
		if (m_GO != null) {
			if (m_MagnetController != null) {
				ResourceMgr.Instance ().FreeMagnetController (m_MagnetController);
				m_MagnetController = null;
			}
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

	public void OnUnbeatableCollided ()
	{
		m_OnUnbeatableCollided = true;
	}

	public override void ActiveMove ()
	{
		if (m_OnUnbeatableCollided) {
			PauseTweener ();
			return;
		}
		base.ActiveMove ();
	}

	public override void StartMoveIn ()
	{
		if (m_OnUnbeatableCollided) {
			PauseTweener ();
			return;
		}
		base.StartMoveIn ();
	}

	public override void StartMoveOut ()
	{
		if (m_OnUnbeatableCollided) {
			PauseTweener ();
			return;
		}
		base.StartMoveOut ();
	}
	
}
