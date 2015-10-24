using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrickManager : MonoBehaviour
{

	public GameObject CloneDynamicBrick;
	int initSize = 100;
	int addSize = 20;
	ObjectPool<DynamicBrick> pool;

	void InitPool ()
	{
		pool = new ObjectPool<DynamicBrick> (initSize, addSize);
		pool.NewObject = NewDynamicBrick;
		pool.Init ();
		
	}
	
	DynamicBrick NewDynamicBrick ()
	{
		GameObject go = GameObject.Instantiate (CloneDynamicBrick) as GameObject;//  GameObject.CreatePrimitive (PrimitiveType.Cube);
		go.transform.parent = this.transform;
		go.SetActive (false);
		DynamicBrick db = new DynamicBrick (go);
		go.GetComponent<DynamicBrickController> ().DB = db;
		return db;
	}

	public void Reset ()
	{
		for (int i=0; i<dynamicBricks.Count; i++) {
			pool.Free (dynamicBricks [i]);
		}
		dynamicBricks.Clear ();

		for (int i=0; i<leaveDynamicBricks.Count; i++) {
			pool.Free (leaveDynamicBricks [i]);
		}
		leaveDynamicBricks.Clear ();

		endPot = Vector2.zero;
		GenerateBlockBrick (Vector2.zero, 10, 3, new Vector3 (20, 20, 0), delayTime_Cell, false);
	}

	void Awake ()
	{
		InitPool ();
	}
	
	

	// Use this for initialization
	void Start ()
	{		
		GenerateBlockBrick (Vector2.zero, 10, 3, new Vector3 (20, 20, 0), delayTime_Cell, false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		DynamicEffect ();

	}
	
	public static bool useContanstSpeed = true;
	float cellSizeX = 1f;
	float cellSizeY = 1f;
	Vector2 endPot = Vector3.zero;
	float delayTime_Cell = 0.01f;// 0.02f;
	
	void GenerateBlockBrick (Vector2 startPot, int xCount, int yCount, Vector3 spanPot, float delayTime_Cell, bool useConstantSpeed)
	{
		float xPot, yPot;
		int count = 0;
		int tmpCount = 0;
		float maxTime = ComputeMaxTime (spanPot);
		
		for (int i=0; i<xCount; i++) {
			xPot = startPot.x + i * cellSizeX;
			
			for (int j=0; j<yCount; j++) {
				yPot = startPot.y + j * cellSizeY;
//				if(spanPot.y>=0) 
//				else {
//					yPot= startPot.y + (yCount-j-1) * cellSizeY;
//				}
			
				count++;
				tmpCount = count;
				if (spanPot.y < 0)
					tmpCount = count - (yCount - j - 1);
				
				DynamicBrick db = pool.Obtain ();
				db.Comming = true;
				db.DelayTime = tmpCount * delayTime_Cell;
				db.LeastFrame = tmpCount;
				db.UseConstantSpeed = useConstantSpeed;
				db.MaxMoveTime = maxTime;
				db.BColor = BrickColor.GetRandomColor ();
				db.TargetPot = new Vector3 (xPot, yPot, 0);
				db.InitPot = db.TargetPot + spanPot;
				db.Go.transform.localPosition = db.InitPot;
				db.Go.GetComponent<Renderer> ().material.color = db.BColor.C;// new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
				db.Go.SetActive (false);
				
				dynamicBricks.Add (db);
				
				
			}
		}
		
		endPot.x = startPot.x + xCount * cellSizeX;
		endPot.y = startPot.y + yCount * cellSizeY;
		
		useContanstSpeed = !useContanstSpeed;
		
	}
	
	List<DynamicBrick> dynamicBricks = new List<DynamicBrick> ();
	List<DynamicBrick> leaveDynamicBricks = new List<DynamicBrick> ();

	void DynamicEffect ()
	{	
		UpdateCurDynamicBrick ();
		UpdateLeaveDynamicBrick ();
//		UpdateSimulatorPot ();
		UpdateGenerageDynamicBrick ();
	}

	void UpdateCurDynamicBrick ()
	{
		for (int i=0; i<dynamicBricks.Count; i++) {
			DynamicBrick db = dynamicBricks [i];
			db.Update ();
		}
	}

	void UpdateGenerageDynamicBrick ()
	{
		if (dynamicBricks.Count < 20) {
			int x, y;
			int r = Random.Range (0, 10);
			if (r < 3) {
				x = Random.Range (10, 15);
				y = Random.Range (-5, 5);
			} else if (r < 6) {
				x = Random.Range (3, 8);
				y = Random.Range (-15, -10);
			} else {
				x = Random.Range (3, 8);
				y = Random.Range (10, 15);
			}
			GenerateBlockBrick (new Vector2 (endPot.x + Random.Range (0, 3), 0), 3, 3, new Vector3 (x, y, x), delayTime_Cell * 5, false);
		}
	}

	void SetLeave (DynamicBrick db, int leaveCount)
	{
		db.IReset ();
		db.Comming = false;
		db.DelayTime = leaveCount * delayTime_Cell * 5f;
		db.LeastFrame = leaveCount;
		db.InitPot = db.Go.transform.localPosition;
		db.TargetPot = db.InitPot + leaveSpan;
		db.MaxMoveTime = ComputeMaxTime (leaveSpan);
	}
	
	void UpdateLeaveDynamicBrick ()
	{
		for (int i=0; i<leaveDynamicBricks.Count; i++) {
			DynamicBrick db = leaveDynamicBricks [i];
			db.Update ();
			if (db.Over) {
				pool.Free (db);
				leaveDynamicBricks.Remove (db);
				i--;
			}
		}
	}

	public void GetLeaveBricks (DynamicBrick db_BelowPlayer)
	{
		int leaveCount = 0;
		for (int i=0; i<dynamicBricks.Count; i++) {
			DynamicBrick db = dynamicBricks [i];
			if (db_BelowPlayer.Go.transform.localPosition.x >= db.Go.transform.localPosition.x) {
				leaveCount++;
				leaveDynamicBricks.Add (db);
				SetLeave (db, leaveCount);
				dynamicBricks.Remove (db);
				i--;
			} 
		}
	}

	public Vector3 leaveSpan = new Vector3 (0, -15, 0);
	public float brickMoveSpeed = 10f;
	public float brickMoveMaxTime = 1f;
	
	float ComputeMaxTime (Vector3 span)
	{
		float maxLenght = (Mathf.Abs (span.x) > Mathf.Abs (span.y)) ? Mathf.Abs (span.x) : Mathf.Abs (span.y);
		float time = maxLenght / brickMoveSpeed;
		if (time < brickMoveMaxTime)
			return time;
		return brickMoveMaxTime;
	}

//	float curValidateX = 0;
//	public PlayerController player;
//	bool start = false;
//	Vector3 v = new Vector3 (2.5f, 6, 0);
//	
//	public void Init ()
//	{
//		player.transform.localPosition = new Vector3 (0, 3, 0);
//	}
//	float time = 0;
//	
//	void UpdateSimulatorPot ()
//	{
//		if ((time += Time.deltaTime) > 2f) {
//			if (!start) {
//				start = true;
//				Init ();
//			}
//		}
//		if (start) {
//			v.y -= 10f * Time.deltaTime;
//			Vector3 pot = player.transform.localPosition;
//			pot += v * Time.deltaTime;
//			if (pot.y < 3) {
//				v.y = 6;
//				int leaveCount = 0;
//				DynamicBrick lastDB = null;
//				for (int i=0; i<dynamicBricks.Count; i++) {
//					DynamicBrick db = dynamicBricks [i];
//					if (player.transform.localPosition.x > db.Go.transform.localPosition.x) {
//						leaveCount++;
//						leaveDynamicBricks.Add (db);
//						SetLeave (db, leaveCount);
//						dynamicBricks.Remove (db);
//						i--;
//						
//						
//					} 
//					if (db.Go.transform.localPosition.x - 0.5f < pot.x &&
//					    pot.x < db.Go.transform.localPosition.x + 0.5f) {
//						if (db.Go.transform.localPosition.y > 1.5f)
//							lastDB = db;
//						
//					} else {
//						
//						if (lastDB != null) {
//							player.GetComponent<Renderer> ().material.color = lastDB.Go.GetComponent<Renderer> ().material.color;
//							lastDB = null;
//						}
//						
//					}
//					
//				}
//				
//			}
//			player.transform.localPosition = pot;
//			
//		}
//		
//	}

}

