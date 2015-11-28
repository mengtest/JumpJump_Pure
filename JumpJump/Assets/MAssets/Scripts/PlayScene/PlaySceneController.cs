using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaySceneController : MonoBehaviour
{


	PlayerController pC;
	
	public PlayerController PC {
		get { return pC;}
	}

	public Block block_Root;
	public float moveDelayTime_Unit = 0.5f;
	public float moveDurationTime_Unit = 1f;
	public float ConditionTime_Unit = 0.5f;
	GameObject m_Block_Head_1_Prefab;
	GameObject m_Block_1_Prefab;
	GameObject m_Block_2_Prefab;



	void LoadPrefab ()
	{
		m_Block_Head_1_Prefab = Resources.Load ("Blocks/Blocks_Head_1") as GameObject;
		m_Block_1_Prefab = Resources.Load ("Blocks/Blocks_1") as GameObject;
		m_Block_2_Prefab = Resources.Load ("Blocks/Blocks_2") as GameObject;

	}

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
		if (GameState.Instance ().M_PlayState == PlayState.PLAY) {

			if (block_Root != null) {
				block_Root.OnUpdate ();
				if (!IsEditor ())
					UpdateMap ();
			}
			GameData.Instance().M_RunningData.M_Score=(int)pC.transform.position.x;
		}
	}

	void Awake ()
	{

		LoadPrefab ();


		Init ();


	}

	void Init ()
	{
		pC = this.gameObject.GetComponentInChildren<PlayerController> ();
		pC.gameObject.SetActive (false);
		PlayGameInstance.INSTANCE = new PlayGameInstance (this);
		
		if (block_Root != null ) {
			block_Root.Set_ExternalCallUpdate (true);
			block_Root.InitParam ();
		}

		if(!IsEditor()){
			InitBlocks ();
			InitMap();
		}

	}

	void InitMap(){
		AddInMap(headBlock,Vector3.zero);// head
	}

	List<Block> m_FreeBlocks = new List<Block> ();
	Block curBlock;

	Block GetFreeBlocks ()
	{
		if (m_FreeBlocks.Count > 0) {
			Block b = m_FreeBlocks [m_FreeBlocks.Count - 1];
			m_FreeBlocks.Remove (b);
			return b;
		} else {
			InitBlocks ();
			Block b = m_FreeBlocks [m_FreeBlocks.Count - 1];
			m_FreeBlocks.Remove (b);

		
			Debug.Log (" no free block");
			return b;
		}
		return null;
	}

	GameObject head;
	Block headBlock;
	void InitBlocks ()
	{
		if(head==null){
			head= GameObject.Instantiate (m_Block_Head_1_Prefab) as GameObject;
			headBlock=head.GetComponent<Block>();
			head.SetActive(false);

		}

		for (int i=0; i<2; i++) {
			GameObject go;
			if(i==0)  go= GameObject.Instantiate (m_Block_1_Prefab) as GameObject;
			else  go= GameObject.Instantiate (m_Block_2_Prefab) as GameObject;
			Block block = go.GetComponent<Block> ();
			m_FreeBlocks.Add (block);
			go.SetActive (false);
		}
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

		Debug.Log ("add in map : locPot=" + locPot);
	}

	void RemoveInMap (Block block)
	{
		Debug.Log(" remove in map name="+block.name);
		block_Root.RemoveBlock (block);
		m_FreeBlocks.Add (block);
		block.transform.parent = null;
		block.gameObject.SetActive (false);

		Debug.Log ("remove in map : locPot=" + block.M_Loc_CurPot);
	}

	void RemoveInMap_Head (Block block)
	{
		block_Root.RemoveBlock (block);
		block.transform.parent = null;
		block.gameObject.SetActive (false);
	}

	void RemoveAllInMap ()
	{
		Block block;
		for (int i=1; i<block_Root.M_Blocks.Count; i++) {
			block = block_Root.M_Blocks [i];

			RemoveInMap (block);
			i--;
		}
	}

	void UpdateMap ()
	{
		Vector3 ballPot = pC.transform.position;
		if (curBlock == null)
			AddInMap (headBlock, Vector3.zero);
		else {
			if (ballPot.x > curBlock.M_Min_StartX - 30) {
				Vector3 locpot = new Vector3 (curBlock.M_Max_EndX + 2, 0, 0);
				locpot = block_Root.transform.InverseTransformPoint (locpot);
				AddInMap (GetFreeBlocks (), locpot);
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

	void Prepare ()
	{
		
	}
	
	void StartGame ()
	{
		
		pC.gameObject.SetActive (true);
	
	}

	void PauseGame ()
	{
	}

	void ResumeGame ()
	{
	}

	public void OnStart ()
	{
		Time.timeScale = 1f;
		StartGame ();

		GameState.Instance ().M_PlayState = PlayState.PLAY;
	}
	
	public void OnPause ()
	{
		Time.timeScale = 0f;
		PauseGame ();

		GameState.Instance ().M_PlayState = PlayState.PAUSE;

	}
	
	public void OnResume ()
	{
		Time.timeScale = 1f;
		ResumeGame ();

		GameState.Instance ().M_PlayState = PlayState.PLAY;
	}
	
	public void OnRestart ()
	{

		pC.Reset ();
	
		if (IsEditor ()) {
			block_Root.IReset ();
			block_Root.InitParam ();
		} else {
			RemoveAllInMap ();
			RemoveInMap_Head(headBlock);
			AddInMap(headBlock,Vector3.zero);
//			curBlock = headBlock;
//			headBlock.gameObject.SetActive(true);

		}

		GameState.Instance ().M_PlayState = PlayState.PLAY;

	}
	
	public void OnGameOver ()
	{
		
	}

	bool IsEditor ()
	{
		return block_Root.name == "Blocks_Editor";
	}




}
