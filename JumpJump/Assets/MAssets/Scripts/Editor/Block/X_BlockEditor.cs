using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(X_Block))]

public class X_BlockEditor : BlockEditor
{
	
	public override void OnInspectorGUI ()
	{
		DrawNums();
		DrawAdjust_BrickNum();
		DrawCommonProperties();
	}
	
	
	
}