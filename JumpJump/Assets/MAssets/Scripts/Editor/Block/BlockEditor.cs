using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(Block))]

public class BlockEditor : Object3dEditor
{

	public override void OnInspectorGUI ()
	{
		DrawNums ();
		DrawButtons ();
		DrawCommonProperties ();
	}

	public void DrawNums ()
	{
		Block block = target as Block;
		
		GUILayout.Label (block.M_BlockNum + " block");
		GUILayout.Label (block.M_BrickNum + " brick");


	}

	void DrawButtons ()
	{
		if (EditorTools.DrawHeader ("ADD ")) {
			Block block = target as Block;
			if (GUILayout.Button ("Add  Empty")) {
				block.AddBlock (BlockType.EMPTY);
				EditorUtility.SetDirty (block);
			}
			if (GUILayout.Button ("Add  H")) {
				block.AddBlock (BlockType.H);
				EditorUtility.SetDirty (block);
			}
			if (GUILayout.Button ("Add  HHH")) {
				block.AddBlock (BlockType.HHH);
				EditorUtility.SetDirty (block);
			}
			if (GUILayout.Button ("Add  V")) {
				block.AddBlock (BlockType.V);
				EditorUtility.SetDirty (block);
			}
			if (GUILayout.Button ("Add  VVV")) {
				block.AddBlock (BlockType.VVV);
				EditorUtility.SetDirty (block);
			}
			if (GUILayout.Button ("Add  X")) {
				block.AddBlock (BlockType.X);
				EditorUtility.SetDirty (block);
			}

			if (GUILayout.Button ("Add  NX")) {
				block.AddBlock (BlockType.NX);
				EditorUtility.SetDirty (block);
			}

			if (GUILayout.Button ("Update Brick Resource")) {
				block.UpdateBrickResource ();
				EditorUtility.SetDirty (block);
			}

			GUILayout.Space (10f);
//			GUILayout.Button ("Add Brick");


			GUILayout.Space (10f);
			if (GUILayout.Button ("DelateAll")) {
				block.DeleteAllChild ();
				EditorUtility.SetDirty (block);
			}
		}
	}

	public void DrawAdjust_BrickNum ()
	{
		Block block = target as Block;
		int brickNum = EditorGUILayout.IntField ("BrickNum", block.M_BrickNum);
		if (GUI.changed) {
			block.AdjustBrickNum (brickNum);
			EditorUtility.SetDirty (block);
		}
	}

	public void DrawAdjust_BlockNum ()
	{
		Block block = target as Block;
		int blockNum = EditorGUILayout.IntField ("BrockNum", block.M_BlockNum);
		if (GUI.changed) {
			block.AdjustBlockNum (blockNum);
			EditorUtility.SetDirty (block);
		}
	}

	void UpdateBrickResource ()
	{
		Block block = target as Block;

	}


}
