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
		LoadBrickMtl();
	}

	public Material[] brickMtls;
	public static int BRICKMTL_NUM = 3;
	public static string[] BRICKMTL_NAMES = {
		"BrickMtl_EMPTY",
		"BrickMtl_REMOVE",
		"BrickMtl_JUMP_HEIGHTER"
	};

	public void LoadBrickMtl ()
	{
		if (brickMtls == null)
			brickMtls = new Material[BRICKMTL_NUM];
		for (int i=0; i<BRICKMTL_NUM; i++) {
			brickMtls [i] = Resources.Load ("CommonMtl/"+BRICKMTL_NAMES [i]) as Material;
		}
	}
	 

}
