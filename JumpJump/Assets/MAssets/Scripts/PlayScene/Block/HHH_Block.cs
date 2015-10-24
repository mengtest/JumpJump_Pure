
using System;
using System.Collections.Generic;
using UnityEngine;


public class HHH_Block:Block
{
	public HHH_Block (Block parent, BlockType type, int blockNum, int bickNum, Vector3 locStartPot, Vector3 locEndPot, int moveDelay, float moveDuration, Vector3[] moveSpan)
		:base(parent,type,locStartPot,locEndPot,moveDelay,moveDuration)
	{
		Vector3 tmpStartPot = Vector3.zero;
		Vector3 tmpEndPot = Vector3.zero;
		for (int i=0; i<blockNum; i++) {
			tmpStartPot.Set (0, i * Brick.HEIGHT, 0);
			tmpEndPot = tmpStartPot + moveSpan [i];
			Block block = new H_Block (this, BlockType.H, bickNum, tmpStartPot, tmpEndPot, i , GetDiriction (tmpStartPot, tmpEndPot), null);
			m_Blocks.Add (block);
		}
	}


	public HHH_Block ()
	{

	}
	
	public override void AdjustBlockNum (int num)
	{
		
		DeleteAllBlock ();
		
		for (int i=0; i<num; i++) {
			Vector3 locStartPot =  new Vector3 (0, i * Brick.HEIGHT, 0);
			Vector3 moveSpan = Vector3.zero;
			int moveDelay = i;
			AddBlock_H(locStartPot,moveSpan,moveDelay);

		}
	}

}
