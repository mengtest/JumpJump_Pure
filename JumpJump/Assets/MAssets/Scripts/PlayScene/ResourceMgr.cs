using UnityEngine;
using System.Collections;

public class ResourceMgr
{

	static ResourceMgr g_Instance;

	public static ResourceMgr Instance ()
	{
		if (g_Instance == null)
			g_Instance = new ResourceMgr ();
		return g_Instance;
	}

	private ResourceMgr ()
	{
		LoadBrickMtl ();
		LoadPhysicMtl();
	}


	#region brick Mtls
	public Material[] m_BrickMtls;
	public static int BRICKMTL_NUM = 8;
	public static string[] BRICKMTL_NAMES = {
		"BrickMtl_EMPTY",
		"BrickMtl_REMOVE",
		"BrickMtl_JUMP_HEIGHTER",
		"BrickMtl_JUMP_TWICE",
		"BrickMtl_SPEED_UP",
		"BrickMtl_SPEED_DOWN",
		"BrickMtl_UNBEATABLE",
		"BrickMtl_COIN",
	};

	public static int MTL_ID_COIN=7;

	public void LoadBrickMtl ()
	{
		if (m_BrickMtls == null)
			m_BrickMtls = new Material[BRICKMTL_NUM];
		for (int i=0; i<BRICKMTL_NUM; i++) {
			m_BrickMtls [i] = Resources.Load ("CommonMtl/" + BRICKMTL_NAMES [i]) as Material;
		}
	}
	 
	#endregion 

	#region physic  Mtls
	public PhysicMaterial m_Ice_PMtl;

	public void LoadPhysicMtl ()
	{
		m_Ice_PMtl = Resources.Load ("PhysicMaterial/Ice") as PhysicMaterial;
	}
	#endregion
	


}
