using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(Brick))]

public class BrickEditor : Object3dEditor
{
	
	public override void OnInspectorGUI ()
	{
		DrawCommonProperties();
	}
	
	
	
}

