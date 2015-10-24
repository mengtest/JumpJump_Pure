
using System;
using System.Collections.Generic;
using UnityEngine;


public class VVV_Block:Block
{
	public VVV_Block (Block parent, BlockType type, int blockNum, int bickNum, Vector3 locStartPot, Vector3 locEndPot, int moveDelay, float moveDuration, Vector3[] moveSpan)
		:base(parent,type,locStartPot,locEndPot,moveDelay,moveDuration)
	{
		Vector3 tmpStartPot = Vector3.zero;
		Vector3 tmpEndPot = Vector3.zero;
		for (int i=0; i<blockNum; i++) {
			tmpStartPot.Set (i * Brick.WIDTH, 0, 0);
			tmpEndPot = tmpStartPot + moveSpan [i];
			Block block = new V_Block (this, BlockType.V, bickNum, tmpStartPot, tmpEndPot, i , GetDiriction (tmpStartPot, tmpEndPot), null);
			m_Blocks.Add (block);
		}
	}

	public VVV_Block ()
	{
		
	}
	
	public override void AdjustBlockNum (int num)
	{
		
		DeleteAllBlock ();
		
		for (int i=0; i<num; i++) {
			Vector3 locStartPot =  new Vector3 (i * Brick.WIDTH, 0, 0);
			Vector3 moveSpan = Vector3.zero;
			int moveDelay = i ;
			AddBlock_V(locStartPot,moveSpan,moveDelay);
			
		}
	}

}
