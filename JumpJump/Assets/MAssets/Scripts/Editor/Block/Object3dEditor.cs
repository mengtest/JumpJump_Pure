using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(Object3d))]

public class Object3dEditor : Editor
{


	public void DrawCommonProperties ()
	{
		Object3d object3d = target as Object3d;


//		if (Selection.activeGameObject ==  object3d.gameObject && 
		if (!Application.isPlaying) {
			if(object3d.Update_Editor ()) EditorUtility.SetDirty (object3d);
		}

		if (EditorTools.DrawHeader ("MOVE PROPERTIES")) {
			GUILayout.Space (6f);
			GUI.changed = false;
			Vector3 moveSpan = EditorGUILayout.Vector3Field ("MoveSpan", object3d.M_MoveSpan);
			int moveDuration = EditorGUILayout.IntField ("Duration", (int)object3d.M_MoveDuration);
			int moveDelay = EditorGUILayout.IntField ("Delay", object3d.M_MoveDelay);

			SerializedProperty activeMoveCondition = serializedObject.FindProperty ("m_MoveActiveConditionType");
			EditorGUILayout.PropertyField (activeMoveCondition, new GUIContent ("MoveActiveCondition"), false);

			SerializedProperty activeCondition = serializedObject.FindProperty ("m_ActiveConditionType");
			EditorGUILayout.PropertyField (activeCondition, new GUIContent ("ActiveCondition"), false);

			int ACT_ArrvieTime = EditorGUILayout.IntField ("ACT_ArrvieTime", object3d.M_ACT_ArrvieTime);

			if (GUI.changed) {
				object3d.M_MoveSpan = moveSpan;
				object3d.M_MoveDuration = moveDuration;
				object3d.M_MoveDelay = moveDelay;
				object3d.M_ACT_ArrvieTime = ACT_ArrvieTime;
				EditorUtility.SetDirty (object3d);
			}

			DrawMoveInOutParam ();

			if (EditorTools.DrawHeader ("DEBUG POT")) {
				EditorGUILayout.Vector3Field ("curLocPot", object3d.M_Loc_CurPot);
				EditorGUILayout.Vector3Field ("curPot", object3d.M_CurPot);
				EditorGUILayout.Vector3Field ("startLocPot", object3d.M_Loc_StartPot);
				EditorGUILayout.Vector3Field ("startPot", object3d.M_StartPot);
				EditorGUILayout.Vector3Field ("endLocPot", object3d.M_Loc_EndPot);
				EditorGUILayout.Vector3Field ("endPot", object3d.M_EndPot);
				EditorGUILayout.Vector3Field ("world pot", object3d.transform.position);
			}

			serializedObject.ApplyModifiedProperties ();
			serializedObject.Update ();


		}

//		if (GUILayout.Button ("SAVE")) {
//
//			EditorUtility.SetDirty (object3d);
//		}

		if (GUILayout.Button ("Delete")) {
			object3d.Delete ();
		}
	
	}

	public void DrawMoveInOutParam ()
	{
		
		Object3d object3d = target as Object3d;
		
		if (object3d is Brick)
			return;
		
		if (EditorTools.DrawHeader ("MOVE IN OUT PROPERTIES")) {
			GUILayout.Space (6f);
			GUI.changed = false;
			
			
			SerializedProperty moveInCT = serializedObject.FindProperty ("m_MoveIn_CT");
			EditorGUILayout.PropertyField (moveInCT, new GUIContent ("moveInCT"), false);
			
			Vector3 moveInSpan = EditorGUILayout.Vector3Field ("MoveInSpan", object3d.M_MoveIn_Span);
			int moveInDuration = EditorGUILayout.IntField ("MoveInDuration", object3d.M_MoveIn_Duration);
			int moveIn_Delay = EditorGUILayout.IntField ("MoveIn_Delay", object3d.M_MoveIn_Delay);
			int moveIn_ArriveTime = EditorGUILayout.IntField ("MoveIn_ArriveTime", object3d.M_MoveIn_ArriveTime);
			
			if (GUI.changed) {
				object3d.M_MoveIn_Span = moveInSpan;
				object3d.M_MoveIn_Duration = moveInDuration;
				object3d.M_MoveIn_ArriveTime = moveIn_ArriveTime;
				object3d.M_MoveIn_Delay = moveIn_Delay;
				EditorUtility.SetDirty (object3d);
			}
			
		}
	}



}