
using System;
using System.Collections.Generic;
using UnityEngine;

public class Block : Object3d , IPoolable
{
	public const float MOVE_DELAY_TIME = 0.02F;
	private Block m_Parent;

	public Block M_Parent {
		get {
			return m_Parent;
		}
		set {
			m_Parent = value;
			UpdateWorldPot ();
		}
	}

	protected BlockType m_Type;
	protected int m_BlockNum;
	protected int m_BrickNum;
	protected Vector3 m_StartPot;

	public Vector3 M_StartPot {
		get {
			return m_StartPot;
		}
	}

	protected Vector3 m_EndPot;

	public Vector3 M_EndPot {
		get {
			return m_EndPot;
		}
	}

	protected Vector3 m_CurPot;

	public Vector3 M_CurPot {
		get {
			return m_CurPot;
		}
	}

	private Vector3 m_Loc_CurPot;

	public Vector3 M_Loc_CurPot {
		get {
			return m_Loc_CurPot;
		}
		set {
			m_Loc_CurPot = value;
			UpdateWorldPot ();
		}
	}
	
	private Vector3 m_Loc_StartPot;

	public Vector3 M_Loc_StartPot {
		get {
			return m_Loc_StartPot;
		}
		set {
			m_Loc_StartPot = value;
			UpdateWorldPot ();
		}
	}

	private Vector3 m_Loc_EndPot;

	public Vector3 M_Loc_EndPot {
		get {
			return m_Loc_EndPot;
		}
		set {
			m_Loc_EndPot = value;
			UpdateWorldPot ();
		}
	}

	protected float m_MoveDelay = 0f;
	protected List<Brick> m_Bricks = new List<Brick> ();
	protected List<Block> m_Blocks = new List<Block> ();
	protected int m_Dirction;
	public const int DIRICTION_LEFT = 1;
	public const int DIRICTION_RIGHT = 1 << 2;
	public const int DIRICTION_UP = 1 << 3;
	public const int DIRICTION_DOWN = 1 << 4;
	bool m_ActiveMove = false;
	bool m_Active = false;
	float m_Time = 0;

	public Block ()
	{
	}

	public Block (Block parent, BlockType type, int blockNum, int brickNum, Vector3 locStartPot, Vector3 locEndPot, float moveDelay, int moveDirction)
	{
		this.m_Parent = parent;
		this.m_Type = type;
		this.m_BlockNum = blockNum;
		this.m_BrickNum = brickNum;
		this.m_Loc_StartPot = locStartPot;
		this.m_Loc_EndPot = locEndPot;
		this.m_MoveDelay = moveDelay;
		this.m_Dirction = moveDirction;
		UpdateWorldPot ();
	}

	public Block (Block parent, BlockType type, int blockNum, int brickNum, Vector3 locStartPot, Vector3 locEndPot, float moveDelay, int moveDirction,
	              List<Brick> bricks, List<Block> blocks)
		:this(parent,type,blockNum,brickNum,locStartPot,locEndPot,moveDelay,moveDirction)
	{
		for (int i=0; i<bricks.Count; i++) {
			m_Bricks.Add (bricks [i]);
		}

		for (int i=0; i<blocks.Count; i++) {
			blocks [i].m_Parent = this;
			m_Blocks.Add (blocks [i]);
		}
		UpdateWorldPot ();
	}

	public void Update ()
	{
		CheckActive ();
		CheckActiveMove ();
		if (!m_Active)
			return;
		UpdateMove ();
		UpdateChild ();
	}

	void UpdateChild ()
	{
		for (int i=0; i<m_Blocks.Count; i++) {
			m_Blocks [i].Update ();
		}
	}

	void UpdateMove ()
	{
		if (!m_ActiveMove)
			return;
		if (m_Time < m_MoveDelay) {
			m_Time += Time.deltaTime;
		} else {
			M_Loc_CurPot = Vector3.Lerp (m_Loc_CurPot, m_Loc_EndPot, Time.deltaTime * 5f);
			for (int i=0; i<m_Bricks.Count; i++) {
				m_Bricks [i].Update ();
			}
		}
	}

	void UpdateWorldPot ()
	{
		if (m_Parent != null) {
			m_StartPot = m_Parent.m_StartPot + m_Loc_StartPot;
			m_EndPot = m_Parent.m_EndPot + m_Loc_EndPot;
			m_CurPot = m_Parent.m_CurPot + m_Loc_CurPot;
		} else {
			m_StartPot = m_Loc_StartPot;
			m_EndPot = m_Loc_EndPot;
			m_CurPot = m_Loc_CurPot;
		}

		for (int i=0; i<m_Bricks.Count; i++) {
			m_Bricks [i].UpdateWorldPot ();
		}

		for (int i=0; i<m_Blocks.Count; i++) {
			m_Blocks [i].UpdateWorldPot ();
		}
	}
	

	public delegate bool delegate_ActiveMoveCondition ();

	public delegate_ActiveMoveCondition ActiveMoveCondition;

	public void CheckActiveMove ()
	{

		if (ActiveMoveCondition == null) {
			ActiveMove ();
		} else if (ActiveMoveCondition ()) {
			ActiveMove ();
		}
	}

	void ActiveMove ()
	{
		m_ActiveMove = true;
		for (int i=0; i<m_Blocks.Count; i++) {
			m_Blocks [i].CheckActiveMove ();
		}
	}

	public delegate bool delegate_ActiveCondition ();

	public delegate_ActiveCondition ActiveCondition;

	public void CheckActive ()
	{
		if (ActiveCondition == null) {
			Active ();
		} else if (ActiveCondition ()) {
			Active ();
		}
	}

	public void Active ()
	{
		m_Active = true;
		for (int i=0; i<m_Blocks.Count; i++) {
			m_Blocks [i].CheckActive ();
		}
	}

	public static int GetDiriction (Vector3 startPot, Vector3 endPot)
	{
		int direction = 0;
		direction |= (startPot.x < endPot.x) ? DIRICTION_RIGHT : 0;
		direction |= (startPot.x > endPot.x) ? DIRICTION_LEFT : 0;
		direction |= (startPot.y < endPot.y) ? DIRICTION_UP : 0;
		direction |= (startPot.y > endPot.y) ? DIRICTION_DOWN : 0;

		return direction;

	}

	public void Reset ()
	{
	}

	public void Destory ()
	{
	}
}

public class H_Block : Block
{
	public H_Block (Block parent, BlockType type, int bickNum, Vector3 locStartPot, Vector3 locEndPot, float moveDelay, int moveDirction, Vector3[] moveSpan)
		:base(parent,type,0,bickNum,locStartPot,locEndPot,moveDelay,moveDirction)
	{
		for (int i=0; i<bickNum; i++) {
			Brick brick = BlockManager.Instance().ObtainBrick ();
			brick.M_Block=this;
			brick.M_Loc_StartPot=new Vector3(i * Brick.WIDTH, 0, 0);
			brick.M_Loc_EndPot = brick.M_Loc_StartPot + ((moveSpan == null) ? Vector3.zero : moveSpan [i]);
			brick.M_Loc_CurPot=brick.M_Loc_StartPot;
			brick.M_MoveDelay=i*Block.MOVE_DELAY_TIME*30;
			brick.M_GO.GetComponent<Renderer>().material.color=BrickColor.GetRandomColor().C;
			brick.M_GO.SetActive(true);
			m_Bricks.Add (brick);
		}
	}
}

public class HHH_Block:Block
{
	public HHH_Block (Block parent, BlockType type, int blockNum, int bickNum, Vector3 locStartPot, Vector3 locEndPot, float moveDelay, int moveDirction, Vector3[] moveSpan)
		:base(parent,type,blockNum,bickNum,locStartPot,locEndPot,moveDelay,moveDirction)
	{
		Vector3 tmpStartPot = Vector3.zero;
		Vector3 tmpEndPot = Vector3.zero;
		for (int i=0; i<blockNum; i++) {
			tmpStartPot.Set (0, i * Brick.HEIGHT, 0);
			tmpEndPot = tmpStartPot + moveSpan [i];
			Block block = new H_Block (this, BlockType.H, bickNum, tmpStartPot, tmpEndPot, i * Block.MOVE_DELAY_TIME, GetDiriction (tmpStartPot, tmpEndPot), null);
			m_Blocks.Add (block);
		}
	}
}

public class V_Block:Block
{

	public V_Block (Block parent, BlockType type, int bickNum, Vector3 locStartPot, Vector3 locEndPot, float moveDelay, int moveDirction, Vector3[] moveSpan)
		:base(parent,type,0,bickNum,locStartPot,locEndPot,moveDelay,moveDirction)
	{
		for (int i=0; i<bickNum; i++) {
			Brick brick =  BlockManager.Instance().ObtainBrick ();
			brick.M_Block=this;
			brick.M_Loc_StartPot.Set (0, i * Brick.HEIGHT, 0);
			brick.M_Loc_EndPot = brick.M_Loc_StartPot + ((moveSpan == null) ? Vector3.zero : moveSpan [i]);
			brick.M_MoveDelay=i*Block.MOVE_DELAY_TIME;
			m_Bricks.Add (brick);
		}
	}
}

public class VVV_Block:Block
{
	public VVV_Block (Block parent, BlockType type, int blockNum, int bickNum, Vector3 locStartPot, Vector3 locEndPot, float moveDelay, int moveDirction, Vector3[] moveSpan)
		:base(parent,type,blockNum,bickNum,locStartPot,locEndPot,moveDelay,moveDirction)
	{
		Vector3 tmpStartPot = Vector3.zero;
		Vector3 tmpEndPot = Vector3.zero;
		for (int i=0; i<blockNum; i++) {
			tmpStartPot.Set (i * Brick.WIDTH, 0, 0);
			tmpEndPot = tmpStartPot + moveSpan [i];
			Block block = new V_Block (this, BlockType.V, bickNum, tmpStartPot, tmpEndPot, i * Block.MOVE_DELAY_TIME, GetDiriction (tmpStartPot, tmpEndPot), null);
			m_Blocks.Add (block);
		}
	}
}

public class X_Block:Block
{

	public X_Block (Block parent, BlockType type, int bickNum, Vector3 locStartPot, Vector3 locEndPot, float moveDelay, int moveDirction, Vector3[] moveSpan)
		:base(parent,type,0,bickNum,locStartPot,locEndPot,moveDelay,moveDirction)
	{
		for (int i=0; i<bickNum; i++) {
			Brick brick = BlockManager.Instance().ObtainBrick ();
			brick.M_Block=this;
			brick.M_Loc_StartPot.Set (i * Brick.WIDTH, i * Brick.HEIGHT, 0);
			brick.M_Loc_EndPot = brick.M_Loc_StartPot + ((moveSpan == null) ? Vector3.zero : moveSpan [i]);
			brick.M_MoveDelay=i*Block.MOVE_DELAY_TIME;
			m_Bricks.Add (brick);
		}
	}
}

public class NX_Block:Block
{
	public NX_Block (Block parent, BlockType type, int bickNum, Vector3 locStartPot, Vector3 locEndPot, float moveDelay, int moveDirction, Vector3[] moveSpan)
		:base(parent,type,0,bickNum,locStartPot,locEndPot,moveDelay,moveDirction)
	{
		for (int i=0; i<bickNum; i++) {
			Brick brick =  BlockManager.Instance().ObtainBrick ();
			brick.M_Block=this;
			brick.M_Loc_StartPot.Set (i * Brick.WIDTH, (bickNum - i - 1) * Brick.HEIGHT, 0);
			brick.M_Loc_EndPot = brick.M_Loc_StartPot + ((moveSpan == null) ? Vector3.zero : moveSpan [i]);
			brick.M_MoveDelay=i*Block.MOVE_DELAY_TIME;
			m_Bricks.Add (brick);
		}
	}
}

public enum BlockType
{
	H=0,
	V=1,
	X=2,
	NX=3,
	HHH=4,
	VVV=5
}




