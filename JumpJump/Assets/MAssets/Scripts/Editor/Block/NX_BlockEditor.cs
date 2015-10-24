using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(NX_Block))]

public class NX_BlockEditor : BlockEditor
{
	
	public override void OnInspectorGUI ()
	{
		DrawNums();
		DrawAdjust_BrickNum();
		DrawCommonProperties();
	}
	
	
	
}