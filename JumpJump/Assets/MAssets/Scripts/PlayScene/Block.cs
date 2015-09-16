
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

	protected List<Brick> m_Bricks = new List<Brick> ();
	protected List<Block> m_Blocks = new List<Block> ();

	public Block ()
	{
	}

	public Block (Block parent, BlockType type, Vector3 locStartPot, Vector3 locEndPot, float moveDelay, float moveDuration)
	:base(parent,locStartPot,locEndPot,moveDelay,moveDuration)
	{
		this.m_Type = type;
		UpdateWorldPot ();
	}

	public Block (Block parent, BlockType type, Vector3 locStartPot, Vector3 locEndPot, float moveDelay, float moveDuration,
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

	public void Update ()
	{
		CheckActive ();
		if (!M_Active)
			return;
		CheckActiveMove ();
		UpdateSelf ();
		UpdateChild ();
	}

	public void UpdateSelf ()
	{
		if (USETWEENER && M_ActiveMove)
			M_Loc_CurPot = Vector3.Lerp (M_Loc_CurPot, M_Loc_EndPot, Time.deltaTime * 5f);
		for (int i=0; i<m_Bricks.Count; i++) {
			m_Bricks [i].Update ();
		}
	}

	void UpdateChild ()
	{
		for (int i=0; i<m_Blocks.Count; i++) {
			m_Blocks [i].Update ();
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

	public override void CheckActiveMove_Child ()
	{
		if (M_ActiveMove) {
			for (int i=0; i<m_Bricks.Count; i++) {
				m_Bricks [i].CheckActiveMove ();
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

	public override void ActiveMove ()
	{
		DoPosition(M_Loc_EndPot,M_MoveDuration,M_MoveDelay);
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
	public H_Block (Block parent, BlockType type, int bickNum, Vector3 locStartPot, Vector3 locEndPot, float moveDelay, float moveDuration, Vector3[] moveSpan)
		:base(parent,type,locStartPot,locEndPot,moveDelay,moveDuration)
	{
		for (int i=0; i<bickNum; i++) {
			Brick brick = BlockManager.Instance ().ObtainBrick ();
			brick.M_Parent = this;
			brick.M_Loc_StartPot = new Vector3 (i * Brick.WIDTH, 0, 0);
			brick.M_Loc_EndPot = brick.M_Loc_StartPot + ((moveSpan == null) ? Vector3.zero : moveSpan [i]);
			brick.M_Loc_CurPot = brick.M_Loc_StartPot;
			brick.M_MoveDelay = i * Block.MOVE_DELAY_TIME_Cell * 30;
			brick.M_GO.GetComponent<Renderer> ().material.color = BrickColor.GetRandomColor ().C;
			brick.M_GO.SetActive (true);
			brick.ActiveMoveCondition=this.IsMoveOver;
			brick.M_MoveDuration=1f;
			m_Bricks.Add (brick);
		}
	}
}

public class HHH_Block:Block
{
	public HHH_Block (Block parent, BlockType type, int blockNum, int bickNum, Vector3 locStartPot, Vector3 locEndPot, float moveDelay, float moveDuration, Vector3[] moveSpan)
		:base(parent,type,locStartPot,locEndPot,moveDelay,moveDuration)
	{
		Vector3 tmpStartPot = Vector3.zero;
		Vector3 tmpEndPot = Vector3.zero;
		for (int i=0; i<blockNum; i++) {
			tmpStartPot.Set (0, i * Brick.HEIGHT, 0);
			tmpEndPot = tmpStartPot + moveSpan [i];
			Block block = new H_Block (this, BlockType.H, bickNum, tmpStartPot, tmpEndPot, i * Block.MOVE_DELAY_TIME_Cell, GetDiriction (tmpStartPot, tmpEndPot), null);
			m_Blocks.Add (block);
		}
	}
}

public class V_Block:Block
{

	public V_Block (Block parent, BlockType type, int bickNum, Vector3 locStartPot, Vector3 locEndPot, float moveDelay, float moveDuration, Vector3[] moveSpan)
		:base(parent,type,locStartPot,locEndPot,moveDelay,moveDuration)
	{
		for (int i=0; i<bickNum; i++) {
			Brick brick = BlockManager.Instance ().ObtainBrick ();
			brick.M_Parent = this;
			brick.M_Loc_StartPot.Set (0, i * Brick.HEIGHT, 0);
			brick.M_Loc_EndPot = brick.M_Loc_StartPot + ((moveSpan == null) ? Vector3.zero : moveSpan [i]);
			brick.M_MoveDelay = i * Block.MOVE_DELAY_TIME_Cell;
			m_Bricks.Add (brick);
		}
	}
}

public class VVV_Block:Block
{
	public VVV_Block (Block parent, BlockType type, int blockNum, int bickNum, Vector3 locStartPot, Vector3 locEndPot, float moveDelay, float moveDuration, Vector3[] moveSpan)
		:base(parent,type,locStartPot,locEndPot,moveDelay,moveDuration)
	{
		Vector3 tmpStartPot = Vector3.zero;
		Vector3 tmpEndPot = Vector3.zero;
		for (int i=0; i<blockNum; i++) {
			tmpStartPot.Set (i * Brick.WIDTH, 0, 0);
			tmpEndPot = tmpStartPot + moveSpan [i];
			Block block = new V_Block (this, BlockType.V, bickNum, tmpStartPot, tmpEndPot, i * Block.MOVE_DELAY_TIME_Cell, GetDiriction (tmpStartPot, tmpEndPot), null);
			m_Blocks.Add (block);
		}
	}
}

public class X_Block:Block
{

	public X_Block (Block parent, BlockType type, int bickNum, Vector3 locStartPot, Vector3 locEndPot, float moveDelay, float moveDuration, Vector3[] moveSpan)
		:base(parent,type,locStartPot,locEndPot,moveDelay,moveDuration)
	{
		for (int i=0; i<bickNum; i++) {
			Brick brick = BlockManager.Instance ().ObtainBrick ();
			brick.M_Parent = this;
			brick.M_Loc_StartPot.Set (i * Brick.WIDTH, i * Brick.HEIGHT, 0);
			brick.M_Loc_EndPot = brick.M_Loc_StartPot + ((moveSpan == null) ? Vector3.zero : moveSpan [i]);
			brick.M_MoveDelay = i * Block.MOVE_DELAY_TIME_Cell;
			m_Bricks.Add (brick);
		}
	}
}

public class NX_Block:Block
{
	public NX_Block (Block parent, BlockType type, int bickNum, Vector3 locStartPot, Vector3 locEndPot, float moveDelay, float moveDuration, Vector3[] moveSpan)
		:base(parent,type,locStartPot,locEndPot,moveDelay,moveDuration)
	{
		for (int i=0; i<bickNum; i++) {
			Brick brick = BlockManager.Instance ().ObtainBrick ();
			brick.M_Parent = this;
			brick.M_Loc_StartPot.Set (i * Brick.WIDTH, (bickNum - i - 1) * Brick.HEIGHT, 0);
			brick.M_Loc_EndPot = brick.M_Loc_StartPot + ((moveSpan == null) ? Vector3.zero : moveSpan [i]);
			brick.M_MoveDelay = i * Block.MOVE_DELAY_TIME_Cell;
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




