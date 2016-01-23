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
		LoadPhysicMtl ();

		LoadBrick ();
	}

	public void LoadResource_Editor ()
	{

	}

	public void LoadResource_Play ()
	{
		LoadCoinResource ();
		LoadMagnetResource();
	}

	#region brick
	public static int FUNCTION_BRICK_NUM = 4;
	public static string[] FUNCTION_BRICK_NAMES = {
		"_Brick_REMOVE",
		"_Brick_JUMP_HEIGHTER",
		"_Brick_JUMP_TWICE",
		"_Brick_UNBEATABLE",
	};
	public GameObject[] m_FunctionBirckPrefabs;
	
	public void LoadFunctionBrick ()
	{

			m_FunctionBirckPrefabs = new GameObject[FUNCTION_BRICK_NUM];
			for (int i=0; i<FUNCTION_BRICK_NUM; i++) {
				m_FunctionBirckPrefabs [i] = Resources.Load ("Brick/" + FUNCTION_BRICK_NAMES [i]) as GameObject;
			}

	}

	public static int EMPTY_BRICK_NUM = 6;
	public static string[] EMPTY_BRICK_NAMES = {
		"_Brick_EMPTY_0",
		"_Brick_EMPTY_1",
		"_Brick_EMPTY_2",
		"_Brick_EMPTY_3",
		"_Brick_EMPTY_4",
		"_Brick_EMPTY_5",
	};
	public GameObject[] m_EmptyBrickPrefabs;

	public void LoadEmptyBrick ()
	{
	
			m_EmptyBrickPrefabs = new GameObject[EMPTY_BRICK_NUM];
			for (int i=0; i<EMPTY_BRICK_NUM; i++) {
				m_EmptyBrickPrefabs [i] = Resources.Load ("Brick/" + EMPTY_BRICK_NAMES [i]) as GameObject;
			}

	}

	public GameObject m_CoinBrickPrefab;

	public void LoadCoinBrick ()
	{

			m_CoinBrickPrefab = Resources.Load ("Brick/_Brick_Coin") as GameObject;

	}

	public GameObject m_BrickPrefab;

	public void LoadBrick ()
	{

			m_BrickPrefab = Resources.Load ("Brick") as GameObject;


		LoadCoinBrick ();
		LoadEmptyBrick ();
		LoadFunctionBrick ();
	}

	public const int TYPE_EMPTY = 0;
	public const int TYPE_REMOVE = 1;
	public const int TYPE_HEIGHGER = 2;
	public const int TYPE_TWICE = 3;
	public const int TYPE_UNBEATABLE = 4;
	public const int TYPE_COIN = 5;

	public GameObject CreateBrick (int type)
	{
		GameObject brick = GameObject.Instantiate (m_BrickPrefab) as GameObject;
		GameObject child = CreateBrick_Child (type);
		if (child != null) {
			child.transform.parent = brick.transform;
			M3DUtil.InitLocalTf (child.transform);
		}
		return brick;
	}

	public GameObject CreateBrick_Child (int type)
	{
		GameObject child = null;
		switch (type) {
		case TYPE_EMPTY:
			child = GameObject.Instantiate (m_EmptyBrickPrefabs [0]) as GameObject;
			break;
		case TYPE_REMOVE:
			child = GameObject.Instantiate (m_FunctionBirckPrefabs [0]) as GameObject;
			break;
		case TYPE_HEIGHGER:
			child = GameObject.Instantiate (m_FunctionBirckPrefabs [1]) as GameObject;
			break;
		case TYPE_TWICE:
			child = GameObject.Instantiate (m_FunctionBirckPrefabs [2]) as GameObject;
			break;
		case TYPE_UNBEATABLE:
			child = GameObject.Instantiate (m_FunctionBirckPrefabs [3]) as GameObject;
			break;
		case TYPE_COIN:
			child = GameObject.Instantiate (m_CoinBrickPrefab) as GameObject;
			break;
		}
		return child;
	}


	#endregion


	#region brick Mtls
	public Material[] m_BrickMtls;
	public static int BRICKMTL_NUM = 8;
	public static string[] BRICKMTL_NAMES = {
		"BrickMtl_EMPTY",
		"BrickMtl_REMOVE",
		"BrickMtl_JUMP_HIGHER",
		"BrickMtl_JUMP_TWICE",
		"BrickMtl_SPEED_UP",
		"BrickMtl_SPEED_DOWN",
		"BrickMtl_UNBEATABLE",
		"BrickMtl_COIN",
	};
	public const int MTL_ID_COIN = 7;
	public const int MTL_ID_EMPTY = 0;

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
	
	#region  props coin
	ObjectPool<CoinController> m_CoinController_Pool;
	GameObject m_Coin_Prefab;
	public const int COIN_INIT_NUM = 20;
	public const int COIN_ADD_NUM = 5;

	CoinController NewCoinController ()
	{
		GameObject go = GameObject.Instantiate (m_Coin_Prefab) as GameObject;
		CoinController cC = go.GetComponent<CoinController> ();
		cC.IReset ();
		return cC;
	}
	
	public CoinController GetCoinController ()
	{ 
		return m_CoinController_Pool.Obtain ();
	}

	public void FreeCoinController (CoinController cC)
	{
		m_CoinController_Pool.Free (cC);
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

	#region  props magnet
	ObjectPool<MagnetController> m_MagnetController_Pool;
	GameObject m_Magnet_Prefab;
	public const int MAGNET_INIT_NUM = 10;
	public const int MAGNET_ADD_NUM = 5;
	
	MagnetController NewMagnetController ()
	{
		GameObject go = GameObject.Instantiate (m_Magnet_Prefab) as GameObject;
		MagnetController mC = go.GetComponent<MagnetController> ();
		mC.IReset ();
		return mC;
	}
	
	public MagnetController GetMagnetController ()
	{ 
		return m_MagnetController_Pool.Obtain ();
	}
	
	public void FreeMagnetController (MagnetController mC)
	{
		m_MagnetController_Pool.Free (mC);
	}
	
	void DestoryMagnetResource ()
	{
		m_MagnetController_Pool.DestoryAll ();
		m_MagnetController_Pool = null;
	}
	
	void LoadMagnetResource ()
	{
		m_Magnet_Prefab = Resources.Load ("Props/Magnet") as GameObject;
		m_MagnetController_Pool = new ObjectPool<MagnetController> (MAGNET_INIT_NUM, MAGNET_ADD_NUM);
		m_MagnetController_Pool.NewObject = NewMagnetController;
		m_MagnetController_Pool.Init ();
	}
	
	#endregion

}
