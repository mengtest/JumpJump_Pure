using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(V_Block))]

public class V_BlockEditor : BlockEditor
{
	
	public override void OnInspectorGUI ()
	{
		DrawNums();
		DrawAdjust_BrickNum();
		DrawCommonProperties();
	}
	
	
	
}

