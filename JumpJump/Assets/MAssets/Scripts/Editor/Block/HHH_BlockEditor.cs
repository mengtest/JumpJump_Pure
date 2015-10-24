using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(HHH_Block))]

public class HHH_BlockEditor : BlockEditor
{
	
	public override void OnInspectorGUI ()
	{
		DrawNums();
		DrawAdjust_BlockNum();
		DrawCommonProperties();
	}
	
	
	
}
