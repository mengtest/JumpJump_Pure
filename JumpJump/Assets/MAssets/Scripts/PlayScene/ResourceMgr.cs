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
		LoadCoinResource();
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

	public const int MTL_ID_COIN=7;
	public const int MTL_ID_EMPTY=0;

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
	
	#region  props
	ObjectPool<CoinController> m_CoinController_Pool;
	GameObject m_Coin_Prefab;
	public const int COIN_INIT_NUM=20;
	public const int COIN_ADD_NUM=5;
	CoinController NewCoinController ()
	{
		GameObject go = GameObject.Instantiate (m_Coin_Prefab) as GameObject;
		CoinController cC = go.GetComponent<CoinController> ();
		cC.IReset ();
		return cC;
	}
	
	public CoinController GetCoinController()
	{ 
		return m_CoinController_Pool.Obtain ();
	}
	public void FreeCoinController(CoinController cC){
		m_CoinController_Pool.Free(cC);
	}
	
	void DestoryCoinResource ()
	{
		m_CoinController_Pool.DestoryAll ();
		m_CoinController_Pool = null;
	}
	
	void LoadCoinResource ()
	{
		m_Coin_Prefab = Resources.Load ("Props/Coin") as GameObject;
		m_CoinController_Pool = new ObjectPool<CoinController> (COIN_INIT_NUM, COIN_ADD_NUM);
		m_CoinController_Pool.NewObject = NewCoinController;
		m_CoinController_Pool.Init ();
	}

	#endregion

}
