using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(VVV_Block))]

public class VVV_BlockEditor : BlockEditor
{
	
	public override void OnInspectorGUI ()
	{
		DrawNums();
		DrawAdjust_BlockNum();
		DrawCommonProperties();
	}
	
}
