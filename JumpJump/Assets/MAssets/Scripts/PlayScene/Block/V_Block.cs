
using System;
using System.Collections.Generic;
using UnityEngine;

public class V_Block:Block
{
	
	public V_Block (Block parent, BlockType type, int bickNum, Vector3 locStartPot, Vector3 locEndPot, int moveDelay, float moveDuration, Vector3[] moveSpan)
		:base(parent,type,locStartPot,locEndPot,moveDelay,moveDuration)
	{
		for (int i=0; i<bickNum; i++) {
			Brick brick = BlockManager.Instance ().ObtainBrick ();
			brick.M_Parent = this;
			brick.M_Loc_StartPot = new Vector3 (0, i * Brick.HEIGHT, 0);
			brick.M_Loc_EndPot = brick.M_Loc_StartPot + ((moveSpan == null) ? Vector3.zero : moveSpan [i]);
			brick.M_MoveDelay = i;
			brick.M_GO.GetComponent<Renderer> ().material.color = BrickColor.GetRandomColor ().C;
			brick.M_GO.SetActive (true);
			brick.MoveActiveCondition = this.IsMoveOver;
			brick.M_MoveDuration = 1f;
			m_Bricks.Add (brick);
		}
	}

	public V_Block ()
	{

	}

	public override void AdjustBrickNum (int num)
	{
		
		DeleteAllBrick ();
		
		for (int i=0; i<num; i++) {
			Vector3 locStartPot = new Vector3 (0, i * Brick.WIDTH, 0);
			Vector3 moveSpan = Vector3.zero;
			int moveDelay = i;
			AddBrick ("Brick", locStartPot, moveSpan, moveDelay);
			
		}
	}

	public override void SetBrickMoveInParam ()
	{
		if (m_Bricks.Count > 0 && HasMoveInCondition ()) {
			int moveDirection = Object3d.GetDiriction (M_MoveSpan, Vector3.zero);
			float t = Get_MoveIn_Duration () / m_Bricks.Count;
			for (int i=0; i<m_Bricks.Count; i++) {
				m_Bricks [i].M_Loc_CurPot =m_Bricks [i]. M_Loc_StartPot + M_MoveIn_Span;
				m_Bricks [i].M_MoveIn_Duration_Time = t;
				if ((moveDirection & Object3d.DIRICTION_UP) > 0) {
					m_Bricks [i].M_MoveIn_DelayTime = (m_Bricks.Count - i - 1) * t;
				} else {
					m_Bricks [i].M_MoveIn_DelayTime = i * t;
				}
			}
		}
	}
}