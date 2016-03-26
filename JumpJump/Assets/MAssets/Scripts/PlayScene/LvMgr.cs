using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LvMgr
{


	public const int HEAD_NUM =1;// 5;
	public const int BLOCK_DIFF_NUM =1;// 5;
	public const int BLOCK_TYPE_NUM =3;//10;
	public GameObject[]  head_Prefabs;
	public GameObject[][] block_Prefabs; // lv type


	public void LoadBlock_Prefabs ()
	{

		head_Prefabs = new GameObject[HEAD_NUM];
		for (int i=0; i<head_Prefabs.Length; i++) {
			head_Prefabs [i] = Resources.Load ("Lv_Blocks/Blocks_0_" + i) as GameObject;
		}


		block_Prefabs =new GameObject[BLOCK_DIFF_NUM][];
	


		for (int i=0; i<BLOCK_DIFF_NUM; i++) {
			block_Prefabs[i]=new GameObject[BLOCK_TYPE_NUM];
			for (int j=0; j<BLOCK_TYPE_NUM; j++) {
				Debug.Log("name="+"Lv_Blocks/Blocks_"+ i+"_"+j);
				block_Prefabs[i][j]=Resources.Load ("Lv_Blocks/Blocks_"+ (i+1)+"_"+j) as GameObject;
			}
		}

	}

	public DiffObjects[] diffObjects;

	public void CreateBlcoks ()
	{
		diffObjects = new DiffObjects[1 + BLOCK_DIFF_NUM];
		diffObjects [0] = new DiffObjects (head_Prefabs, 0);
		for (int i=0; i<BLOCK_DIFF_NUM; i++) {
			 
			diffObjects [i + 1] = new DiffObjects (block_Prefabs [i], i + 1);
		}
	}

	public Block GetRandomBlock ()
	{
		ComputeDiffId();
		//test
		diffId = 1;
		return diffObjects [diffId].GetRandomBlock ();
	}

	public Block GetRandomHeadBlock(){
		return diffObjects[0].GetRandomBlock();
	}



	bool isEdit=false;
	Block curBlock;
	Block block_Root;
	PlayerController pC;


	int diffId_Start=0;
	int diffId_End=5;
	int addBlockCount=0;
	int diffId=0;

	int pro_Num=10;
	float [][] pro_Diffs=new float [][]{
		new float[]{1f,0f,0f,0f,0f},
		new float[]{0.5f,0.5f,0f,0f,0f},
		new float[]{0.3f,0.4f,0.3f,0f,0f},
		new float[]{0.2f,0.2f,0.3f,0.3f,0f},
		new float[]{0.2f,0.2f,0.2f,0.2f,0.2f},
		//add diff
		new float[]{0,0.3f,0.3f,0.2f,0.2f},
		new float[]{0f,0.2f,0.3f,0.3f,0.2f},
		new float[]{0.1f,0f,0.3f,0.3f,0.3f},
		//add diff
		new float[]{0.1f,0.1f,0.1f,0.4f,0.3f},
		new float[]{0.1f,0.1f,0.1f,0.3f,0.4f},

	};
//	int [] diffIds=new int[]{
//		0,1,2,3,4,5,8,8,7,7,7,8,5,5,9,8,7,5,7,8,10
//	};

	void InitPro(){
		for(int i=0;i<pro_Num;i++){
			pro_Diffs[i]=new float[BLOCK_DIFF_NUM];
			for(int j=0;j<BLOCK_DIFF_NUM;j++){
//				pro_Diffs[i][j]=
			}
		}
	}

	void ComputeDiffId(){
		// add Block count ==1  , diffId<2
//		if(addBlockCount<=1){
//			diffId_Start=1;
//			diffId_End=1;
//		}else if(addBlockCount<3){
//			diffId_Start=1;
//			diffId_End=3;
//		}else if(addBlockCount<5){
//			diffId_Start=1;
//			diffId_End=5;
//		}

//		if(addBlockCount<5) diffId=addBlockCount-1;
//		else if(addBlockCount<10) diffId=addBlockCount;
//		else diffId=addBlockCount-1;

		int count=addBlockCount-1;
		int diffPro_Id=Mathf.Clamp(count,0,pro_Num);
		diffId=MathUtil.getRandomByProbability(pro_Diffs[diffPro_Id]);
	}



	public void Init(PlayerController pC, Block block_Root,bool isEdit){
		this.pC=pC;
		this.isEdit=isEdit;
		this.block_Root=block_Root;
	}

	void AddInMap (Block block, Vector3 locPot)
	{
		
		block_Root.AddBlock (block);
		block.transform.parent = block_Root.transform;
		block.M_Loc_CurPot = locPot;
		block.M_Loc_StartPot = locPot;
		
		block.IReset ();
		block.InitParam ();
		block.gameObject.SetActive (true);
		
		curBlock = block;
		addBlockCount++;
		Debug.Log ("add in map : locPot=" + locPot);
	}
	
	void RemoveInMap (Block block)
	{
		Debug.Log (" remove in map name=" + block.name);
		block_Root.RemoveBlock (block);
		diffObjects[block.M_DiffLv].Free(block);
		block.gameObject.SetActive (false);
		
		Debug.Log ("remove in map : locPot=" + block.M_Loc_CurPot);
	}


	void ClearMap ()
	{
		Block block;
		for (int i=0; i<block_Root.M_Blocks.Count; i++) {
			block = block_Root.M_Blocks [i];
			RemoveInMap (block);
			i--;
		}
	}




	public void Update ()
	{
		if(block_Root!=null) block_Root.OnUpdate();
		if(!isEdit) UpdateMap();
	}


	void UpdateMap ()
	{
		Vector3 ballPot = pC.transform.position;
		if (curBlock == null)
			AddInMap (GetRandomHeadBlock(), Vector3.zero);
		else {
			if (ballPot.x > curBlock.M_Min_StartX - 30) {
				Vector3 locpot = new Vector3 (curBlock.M_Max_EndX + 2, 0, 0);
				locpot = block_Root.transform.InverseTransformPoint (locpot);
				AddInMap (GetRandomBlock(), locpot);
			}
		}
		
		Block block;
		for (int i=1; i<block_Root.M_Blocks.Count; i++) {
			block = block_Root.M_Blocks [i];
			if (ballPot.x > block.M_Max_EndX + 20) {
				Debug.Log (" end x=" + block.M_Max_EndX + " ball pot.x=" + ballPot.x);
				RemoveInMap (block);
				i--;
			}
		}
	}



	
	public void Reset ()
	{
		ClearMap();
		block_Root.IReset();
		block_Root.InitParam ();
		addBlockCount=0;
		curBlock=null;
	}

	public void Start(){
		Reset();
		if(!isEdit){
			AddInMap(GetRandomHeadBlock(),Vector3.zero);
		}
	}



	
}
 
public class DiffObjects
{
	int m_DiffLv;//0-head,1-5 diff
	List<Block> m_Blocks;
	List<Block> m_UsedBlocks;
	GameObject [] m_Prefabs;

	public DiffObjects (GameObject[] prefabs, int diffLv)
	{
		this.m_Prefabs=prefabs;
		int num = prefabs.Length;
		this.m_DiffLv = diffLv;
		this.m_Blocks = new List<Block> ();
		this.m_UsedBlocks = new List<Block> ();
		for (int i=0; i<num; i++) {
			Debug.Log(" difflv="+diffLv+" i="+i+" name="+prefabs[i].name);
			Block block=GameObject.Instantiate (prefabs [i]).GetComponent<Block> ();
			block.M_DiffLv=diffLv;
			block.gameObject.SetActive(false);
			this.m_Blocks.Add (block);
		}
	}

	public Block GetRandomBlock ()
	{
		Block block;
		if(m_Blocks.Count==0){
			for (int i=0; i<m_Prefabs.Length; i++) {
				Debug.Log(" add ");
				block=GameObject.Instantiate (m_Prefabs [i]).GetComponent<Block> ();
				block.M_DiffLv=m_DiffLv;
				block.gameObject.SetActive(false);
				this.m_Blocks.Add (block);
			}
		}
		int rid = Random.Range (0, m_Blocks.Count);
		Debug.Log(" rid="+rid);
		block = m_Blocks [rid];
		m_Blocks.Remove (block);
		m_UsedBlocks.Add (block);
		return block;
	}

	public void Free (Block block)
	{
		m_UsedBlocks.Remove (block);
		m_Blocks.Add (block);
		block.IReset ();
		block.transform.parent=null;
	}

}