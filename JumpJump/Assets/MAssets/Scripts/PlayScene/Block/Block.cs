
using System;
using System.Collections.Generic;
using UnityEngine;

public class Block : Object3d , IPoolable
{

	protected BlockType m_Type;

	public int M_BrickNum {
		get { return m_Bricks.Count;}
	}

	public int M_BlockNum {
		get { return m_Blocks.Count;}
	}

	[SerializeField]
	protected List<Brick>
		m_Bricks = new List<Brick> ();

	public List<Brick> M_Bricks {
		get {
			return m_Bricks;
		}
	}

	[SerializeField]
	protected List<Block>
		m_Blocks = new List<Block> ();

	public List<Block> M_Blocks {
		get {
			return m_Blocks;
		}
	}

	public void AddBlock (Block block)
	{
		m_Blocks.Add (block);
		block.M_Parent = this;
	}

	public Block ()
	{

	}

	public Block (Block parent, BlockType type, Vector3 locStartPot, Vector3 locEndPot, int moveDelay, float moveDuration)
	:base(parent,locStartPot,locEndPot,moveDelay,moveDuration)
	{
		this.m_Type = type;
		UpdateWorldPot ();
	}

	public Block (Block parent, BlockType type, Vector3 locStartPot, Vector3 locEndPot, int moveDelay, float moveDuration,
	              List<Brick> bricks, List<Block> blocks)
		:this(parent,type,locStartPot,locEndPot,moveDelay,moveDuration)
	{
		for (int i=0; i<bricks.Count; i++) {
			m_Bricks.Add (bricks [i]);
		}

		for (int i=0; i<blocks.Count; i++) {
			blocks [i].M_Parent = this;
			m_Blocks.Add (blocks [i]);
		}
		UpdateWorldPot ();
	}

	bool isExternalCallUpdate = false;

	public void Set_ExternalCallUpdate (bool isCall)
	{
		isExternalCallUpdate = isCall;
	}

	void Update ()
	{
		if (M_Parent == null && !isExternalCallUpdate)
			OnUpdate ();
	}

	public void OnUpdate ()
	{

		if (CheckMoveIn ())
			return;
		CheckActive ();
		if (!M_Active)
			return;
		CheckMoveActive ();
		CheckMoveOut ();
		UpdateSelf ();
		UpdateChild ();
	}

	public void UpdateSelf ()
	{
		if (!USETWEENER && M_ActiveMove)
			M_Loc_CurPot = Vector3.Lerp (M_Loc_CurPot, M_Loc_EndPot, Time.deltaTime * 5f);
		for (int i=0; i<m_Bricks.Count; i++) {
			m_Bricks [i].OnUpdate ();
		}
	}

	void UpdateChild ()
	{
		for (int i=0; i<m_Blocks.Count; i++) {
			m_Blocks [i].OnUpdate ();
		}
	}

	bool CheckMoveIn ()
	{
		if (HasMoveInCondition () && m_Bricks.Count > 0) {
			if (!m_MoveIn && MoveIn_Condition (this)) {
				StartMoveIn ();
			}
			if (!IsMoveInOver () && !m_MoveIn_Overed)
				return true;
		}
		return false;
	}

	void CheckMoveOut ()
	{
		if (HasMoveOutCondition () && m_Bricks.Count > 0) {
			if (!m_MoveOut && MoveOut_Condition (this)) {
				StartMoveOut ();
			}
		}
	}

	public override void CheckActive_Child ()
	{
		if (M_Active) { 
			for (int i=0; i<m_Bricks.Count; i++) {
				m_Bricks [i].CheckActive ();
			}
		}
	}

	public override void CheckMoveActive_Child ()
	{
		if (M_ActiveMove) {
			for (int i=0; i<m_Bricks.Count; i++) {
				m_Bricks [i].CheckMoveActive ();
			}
		}
	}

	public override void UpdateWorldPot ()
	{
		base.UpdateWorldPot ();

		for (int i=0; i<m_Bricks.Count; i++) {

			m_Bricks [i].UpdateWorldPot ();
		}

		for (int i=0; i<m_Blocks.Count; i++) {
			m_Blocks [i].UpdateWorldPot ();
		}
	}

	public override void Active ()
	{
		base.Active ();
	}

//	public override void ActiveMove ()
//	{
//		DoPosition (M_Loc_EndPot, M_MoveDuration, M_MoveDelay);
//	}

	public override void SetActiveCondition (delegate_ActiveCondition condition)
	{
		for (int i=0; i<m_Blocks.Count; i++) {
			m_Blocks [i].SetActiveCondition (condition);
		}

		for (int i=0; i<m_Bricks.Count; i++) {
			m_Bricks [i].SetActiveCondition (condition);
		}
		base.SetActiveCondition (condition);
	}

	public void IReset ()
	{
		Reset ();
		for (int i=0; i<m_Blocks.Count; i++) {
			m_Blocks [i].IReset ();
		}

		for (int i=0; i<m_Bricks.Count; i++) {
			m_Bricks [i].IReset ();
		}
//		if(M_Parent==null) 
		ComputeMinBoundsX ();// noly need compute once by root
	}

	public void IDestory ()
	{
	}


	#region Debug

	void OnDrawGizmosSelected ()
	{
		Color c = Color.blue;
		c.a = 0.5f;
		Gizmos.color = c;
		UpdateWorldPot ();
		for (int i=0; i<m_Bricks.Count; i++) {
			Gizmos.DrawCube (M_EndPot + m_Bricks [i].M_Loc_StartPot, Vector3.one);
		}
	}


	#endregion


	#region editor

//	public static GameObject cloneBrick;
//
//	public static GameObject GetCloneBrick ()
//	{
//		if (cloneBrick == null) 
//			cloneBrick = Resources.Load ("Brick") as GameObject;
//		return cloneBrick;
//	}
	

	public void AddBlock (BlockType type)
	{
		switch (type) {
		case BlockType.EMPTY:
			AddBlock_Empty (Vector3.zero, Vector3.zero);
			break;
		case BlockType.H:
			AddBlock_H (Vector3.zero, Vector3.zero);
			break;
		case BlockType.HHH:
			AddBlock_HHH ();
			break;
		case BlockType.V:
			AddBlock_V (Vector3.zero, Vector3.zero);
			break;
		case BlockType.VVV:
			AddBlock_VVV ();
			break;
		case BlockType.X:
			AddBlock_X ();
			break;
		case BlockType.NX:
			AddBlock_NX ();
			break;
		}
	}

	public void AddBlock_Empty (Vector3 loc_StartPot, Vector3 moveSpan, int moveDelay=0)
	{
		GameObject go = new GameObject ("Block");
		Block block = go.AddComponent<Block> ();
		go.transform.parent = this.transform;
		block.M_Parent = this;
		block.M_Loc_StartPot = loc_StartPot;
		block.M_MoveSpan = moveSpan;
		block.M_Loc_CurPot = block.M_Loc_StartPot;
		block.M_MoveDelay = moveDelay;
		m_Blocks.Add (block);
	}

	public void AddBlock_H (Vector3 loc_StartPot, Vector3 moveSpan, int moveDelay=0)
	{
		GameObject go = new GameObject ("Block_H");
		H_Block h_Block = go.AddComponent<H_Block> (); 
		go.transform.parent = this.transform;
		h_Block.M_Parent = this;
		h_Block.M_Loc_StartPot = loc_StartPot;
		h_Block.M_MoveSpan = moveSpan;
		h_Block.M_Loc_CurPot = h_Block.M_Loc_StartPot;
		h_Block.M_MoveDelay = moveDelay;


		h_Block.AdjustBrickNum (4);
		m_Blocks.Add (h_Block);

	}

	public void AddBlock_V (Vector3 loc_StartPot, Vector3 moveSpan, int moveDelay=0)
	{
		GameObject go = new GameObject ("Block_V");
		V_Block v_Block = go.AddComponent<V_Block> (); 
		go.transform.parent = this.transform;
		v_Block.M_Parent = this;

		v_Block.M_Loc_StartPot = loc_StartPot;
		v_Block.M_MoveSpan = moveSpan;
		v_Block.M_Loc_CurPot = v_Block.M_Loc_StartPot;
		v_Block.M_MoveDelay = moveDelay;

		v_Block.AdjustBrickNum (4);
		m_Blocks.Add (v_Block);
	}

	void AddBlock_X ()
	{
		GameObject go = new GameObject ("Block_X");
		X_Block x_Block = go.AddComponent<X_Block> (); 
		go.transform.parent = this.transform;
		x_Block.M_Parent = this;
		x_Block.AdjustBrickNum (4);
		m_Blocks.Add (x_Block);	
	}

	void AddBlock_NX ()
	{
		GameObject go = new GameObject ("Block_NX");
		NX_Block nx_Block = go.AddComponent<NX_Block> (); 
		go.transform.parent = this.transform;
		nx_Block.M_Parent = this;
		nx_Block.AdjustBrickNum (4);
		m_Blocks.Add (nx_Block);	
	}

	void AddBlock_HHH ()
	{
		GameObject go = new GameObject ("Block_HHH");
		HHH_Block hhh_Block = go.AddComponent<HHH_Block> (); 
		go.transform.parent = this.transform;
		go.transform.localPosition = Vector3.zero;
		hhh_Block.M_Parent = this;
		hhh_Block.AdjustBlockNum (4);
		m_Blocks.Add (hhh_Block);	
	}

	void AddBlock_VVV ()
	{
		GameObject go = new GameObject ("Block_VVV");
		VVV_Block VVV_Block = go.AddComponent<VVV_Block> (); 
		go.transform.parent = this.transform;
		go.transform.localPosition = Vector3.zero;
		VVV_Block.M_Parent = this;
		VVV_Block.AdjustBlockNum (4);
		m_Blocks.Add (VVV_Block);	
	}

	public override void Delete ()
	{
		if (M_Parent != null) {
			((Block)M_Parent).DeleteBlock (this);
		} else {
			GameObject.DestroyImmediate (this.gameObject);
		}
	}

	public void DeleteBrick (Brick brick)
	{
		m_Bricks.Remove (brick);
		GameObject.DestroyImmediate (brick.gameObject);

	}

	public void RemoveBrick (Brick brick)
	{
		m_Bricks.Remove (brick);
	}

	public void DeleteBlock (Block block)
	{
		m_Blocks.Remove (block);
		GameObject.DestroyImmediate (block.gameObject);
	}

	public void RemoveBlock (Block block)
	{
		m_Blocks.Remove (block);
	}

	public void DeleteAllBrick ()
	{
		for (int i=0; i<m_Bricks.Count; i++) {
			if (m_Bricks [i] != null)
				GameObject.DestroyImmediate (m_Bricks [i].gameObject);
		}
		m_Bricks.Clear ();
	}

	public void DeleteAllBlock ()
	{
		for (int i=0; i<m_Blocks.Count; i++) {
			if (m_Blocks [i] != null)
				GameObject.DestroyImmediate (m_Blocks [i].gameObject);
		}
		m_Blocks.Clear ();
	}

	public void DeleteAllChild ()
	{
		DeleteAllBrick ();
		DeleteAllBlock ();
	}

	public void AddBrick (String name, Vector3 loc_StartPot, Vector3 moveSpan, int moveDelay)
	{
//		GameObject go = GameObject.Instantiate (Block.GetCloneBrick ()) as GameObject;
		GameObject go = ResourceMgr.Instance ().CreateBrick (ResourceMgr.TYPE_EMPTY);
		go.name = name;
		go.transform.parent = this.transform;
	
		Brick brick = go.AddComponent<Brick> ();
		
		brick.M_GO = go;
		brick.M_Parent = this;
		brick.M_Loc_StartPot = loc_StartPot;
		brick.M_MoveSpan = moveSpan;
		brick.M_Loc_CurPot = brick.M_Loc_StartPot;
		brick.M_MoveDelay = moveDelay;
		
		brick.M_GO.SetActive (true);  
//		brick.ActiveMoveCondition =IsMoveOver; 
		brick.M_MoveActiveConditionType = MoveActiveConditionType.PARENT_MOVE_OVER; 
		brick.M_MoveDuration = 1f;
		m_Bricks.Add (brick);

//		Material tmp = new Material (brick.M_GO.GetComponent<Renderer> ().material);// .sharedMaterial);
//		tmp.color = BrickColor.GetRandomColor ().C;
//		brick.M_GO.GetComponent<Renderer> ().material = tmp;
	}

	bool Test ()
	{
		return false;
	}

	public virtual void  AdjustBrickNum (int num)
	{

	}

	public virtual void AdjustBlockNum (int num)
	{
	}
	
	#endregion



	#region move in out
	public override void StartMoveIn ()
	{
		m_MoveIn = true;
		for (int i=0; i<m_Bricks.Count; i++) {
			m_Bricks [i].StartMoveIn ();
		}
	}

	public override bool IsMoveInOver ()
	{
		for (int i=0; i<m_Bricks.Count; i++) {
			if (!m_Bricks [i].IsMoveInOver ()) {
				return false;
			}
		}
		m_MoveIn_Overed = true;
		return true;
	}
	
	public virtual void SetBrickMoveInParam ()
	{

		if (m_Bricks.Count > 0 && HasMoveInCondition ()) {
			m_Bricks.Sort (SortByLeft);
			float t = Get_MoveIn_Duration () / m_Bricks.Count;
			for (int i=0; i<m_Bricks.Count; i++) {
				m_Bricks [i].M_Loc_CurPot = M_Loc_StartPot + M_MoveIn_Span;
				m_Bricks [i].M_MoveIn_Duration_Time = t;
				m_Bricks [i].M_MoveIn_DelayTime = i * t;
			}
		}

		for (int i=0; i<m_Blocks.Count; i++) {
			m_Blocks [i].SetBrickMoveInParam (); 
		}

	}

	public override void StartMoveOut ()
	{
		m_MoveOut = true;
		for (int i=0; i<m_Bricks.Count; i++) {
			m_Bricks [i].StartMoveOut ();
		}
	}

	void SetBrickMoveOutParam ()
	{
		if (m_Bricks.Count > 0 && HasMoveOutCondition ()) {
			m_Bricks.Sort (SortByLeft);
			float t = Get_MoveOut_Duration () / m_Bricks.Count;
			for (int i=0; i<m_Bricks.Count; i++) {
				m_Bricks [i].M_MoveOut_Loc_EndPot = m_Bricks [i].M_Loc_EndPot + M_MoveOut_Span;
				m_Bricks [i].M_MoveOut_Duration_Time = t;
				m_Bricks [i].M_MoveOut_DelayTime = i * t;
			}
		}
		
		for (int i=0; i<m_Blocks.Count; i++) {
			m_Blocks [i].SetBrickMoveOutParam (); 
		}
	}

	public void InitParam ()
	{
		SetBrickMoveInParam ();
		SetBrickMoveOutParam ();
		SetRandomCoin ();
		SetRandomMagnet();
	}

	void SetRandomCoin ()
	{
		for (int i=0; i<m_Bricks.Count; i++) {

			if (MathUtil.IndependentProbability (0.2f) && m_Bricks [i].M_FunctionType == FunctionType.EMPTY) {
				m_Bricks [i].M_IsCoin = true;
			}
		}

		for (int i=0; i<m_Blocks.Count; i++) {
			m_Blocks [i].SetRandomCoin ();
		}
	}

	public void SetRandomMagnet(){
		float minSpacing=10f;
		float lastPotX=0f;
		List<Brick> topsideBricks=GetTopsideBricks();
		for(int i=0;i<topsideBricks.Count;i++){

			if (MathUtil.IndependentProbability (0.1f) && topsideBricks[i].M_CurPot.x-lastPotX>minSpacing){
				lastPotX=topsideBricks[i].M_CurPot.x;
				topsideBricks[i].M_AttachMagnet=true;
			}
		}
	}

	#endregion



	public void ComputeMinBoundsX ()
	{
		for (int i=0; i<m_Blocks.Count; i++) {
			m_Blocks [i].ComputeMinBoundsX ();
		}
		// compute self
		m_Min_StartX = m_Max_StartX = M_CurPot.x;
		m_Min_EndX = m_Max_EndX = M_EndPot.x;

		for (int i=0; i<m_Bricks.Count; i++) {
			if (m_Bricks [i].M_CurPot.x < m_Min_StartX)
				m_Min_StartX = m_Bricks [i].M_CurPot.x;

			if (m_Bricks [i].M_CurPot.x > m_Max_StartX)
				m_Max_StartX = m_Bricks [i].M_CurPot.x;

			if (m_Bricks [i].M_EndPot.x < m_Min_EndX)
				m_Min_EndX = m_Bricks [i].M_EndPot.x;

			if (m_Bricks [i].M_EndPot.x > m_Max_EndX)
				m_Max_EndX = m_Bricks [i].M_EndPot.x;
		}

		for (int i=0; i<m_Blocks.Count; i++) {
			if (m_Blocks [i].m_Min_StartX < m_Min_StartX)
				m_Min_StartX = m_Blocks [i].m_Min_StartX;
			
			if (m_Blocks [i].m_Max_StartX > m_Max_StartX)
				m_Max_StartX = m_Blocks [i].m_Max_StartX;
			
			if (m_Blocks [i].m_Min_EndX < m_Min_EndX)
				m_Min_EndX = m_Blocks [i].m_Min_EndX;
			
			if (m_Blocks [i].m_Max_EndX > m_Max_EndX)
				m_Max_EndX = m_Blocks [i].m_Max_EndX;
		}
	}

	public override void _ChangeFunction ()
	{
		if (M_FunctionType == FunctionType.NONE)
			return;
		for (int i=0; i<m_Bricks.Count; i++) {
			m_Bricks [i].M_FunctionType = M_FunctionType;
		}
	}

	#region update brick resource
	public	void UpdateBrickResource ()
	{
		Brick brick;
		MeshFilter meshFilter;
		MeshRenderer meshRenderer;

		for (int i=0; i<m_Bricks.Count; i++) {
			brick = m_Bricks [i];
			meshFilter = brick.GetComponent<MeshFilter> ();
			if (meshFilter != null)
				GameObject.DestroyImmediate (meshFilter);
			meshRenderer = brick.GetComponent<MeshRenderer> ();
			if (meshRenderer != null)
				GameObject.DestroyImmediate (meshRenderer);

//			if (brick.transform.childCount == 0) {
			brick.SetBrickChild ();
//			}
		}
		for (int i=0; i<m_Blocks.Count; i++) {
			m_Blocks [i].UpdateBrickResource ();
		}
	}

	#endregion

	#region topside brick
	Dictionary<int,Brick> columnBrickMap = new Dictionary<int, Brick> ();

	public List<Brick> GetTopsideBricks ()
	{
		columnBrickMap.Clear ();

		List<Brick> allBricks = new List<Brick> ();
		GetAllBricks (allBricks);
		for (int i=0; i<allBricks.Count; i++) {
			int column = allBricks [i].CurColumn;
			if (columnBrickMap.ContainsKey (column)) {
				Brick b = columnBrickMap [column];
				if (b.M_CurPot.y < allBricks [i].M_CurPot.y) {
					columnBrickMap[column]=allBricks[i];
				}
			} else {
				columnBrickMap.Add (column,allBricks [i]);
			}
		}
		List<Brick> topsideBricks = new List<Brick> (columnBrickMap.Values);
		columnBrickMap.Clear();
		return topsideBricks;
	}

	public void GetAllBricks (List<Brick> allBricks)
	{
		for (int i=0; i<m_Bricks.Count; i++) {
			allBricks.Add (m_Bricks [i]);
		}
		for (int i=0; i<m_Blocks.Count; i++) {
			m_Blocks [i].GetAllBricks (allBricks);
		}
	}

	#endregion

}

public enum BlockType
{
	EMPTY=-1,
	H=0,
	V=1,
	X=2,
	NX=3,
	HHH=4,
	VVV=5
}




