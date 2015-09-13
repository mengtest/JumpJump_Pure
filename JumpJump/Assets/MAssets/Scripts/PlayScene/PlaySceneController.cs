using UnityEngine;
using System.Collections;

public class PlaySceneController : MonoBehaviour
{
	
	BrickManager bM;

	public BrickManager BM {
		get { return bM;}
	}

	PlayerController pC;

	public PlayerController PC {
		get { return pC;}
	}


	BlockManager m_BlockMgr;
	
	public BlockManager M_BlockMgr {
		get {
			return m_BlockMgr;
		}
	}




	void Awake ()
	{
//		instance = this;
		bM = this.gameObject.GetComponentInChildren<BrickManager> ();
		pC = this.gameObject.GetComponentInChildren<PlayerController> ();
		pC.gameObject.SetActive (false);

		m_BlockMgr=this.gameObject.GetComponentInChildren<BlockManager> ();

		PlayGameInstance.INSTANCE=new PlayGameInstance(this);
	}


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Update_DelayShowPlayer();
	}

	void Prepare ()
	{

	}

	void StartGame ()
	{

		start=true;
	}

	void PauseGame ()
	{

	}

	public void LoadLv (int lvId)
	{

	}

	bool start = false;
	public float delayTime_ShowPlayer = 2f;
	bool ShowedPlayer = false;
	float time = 0;

	void Update_DelayShowPlayer ()
	{
		if (start && !ShowedPlayer) {
			if (time > delayTime_ShowPlayer) {
				pC.gameObject.SetActive (true);
				ShowedPlayer = true;
			}
		}
		time+=Time.deltaTime;
	}

	public void OnStart ()
	{
		LoadLv (0);
		StartGame ();
	}

	public void OnPause ()
	{
		Time.timeScale=0f;
	}

	public void OnResume(){
		Time.timeScale=1f;
	}

	public void OnRestart(){
//		StartGame();//
		time=0;
		ShowedPlayer=false;
		BM.Reset();
		pC.gameObject.SetActive (false);
		pC.transform.localPosition=new Vector3(0,4,0);
	}

	public void OnGameOver ()
	{

	}
	


}
