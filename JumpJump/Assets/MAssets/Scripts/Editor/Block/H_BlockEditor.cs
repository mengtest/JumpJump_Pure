using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(H_Block))]

public class H_BlockEditor : BlockEditor
{

	public override void OnInspectorGUI ()
	{
		DrawNums();
		DrawAdjust_BrickNum();
		DrawCommonProperties();

	}



}

