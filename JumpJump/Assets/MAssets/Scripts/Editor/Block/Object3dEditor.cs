using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(Object3d))]

public class Object3dEditor : Editor
{


	public void DrawCommonProperties ()
	{
		Object3d object3d = target as Object3d;


		if (!Application.isPlaying) {
			if (object3d.Update_Editor ())
				EditorUtility.SetDirty (object3d);
		}

		if (EditorTools.DrawHeader ("PROPERTIES")) {
			GUILayout.Space (6f);
			DrawMoveParma ();

			DrawMoveInOutParam ();

			DrawFunctionParma ();

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

	public void DrawFunctionParma ()
	{
		if (EditorTools.DrawHeader ("FUNCTION PROPERTIES")) {
			GUILayout.Space (6f);
			Object3d object3d = target as Object3d;
			FunctionType oldType = object3d.M_Old_FunctionType;
			SerializedProperty m_FunctionType = serializedObject.FindProperty ("m_FunctionType");
			EditorGUILayout.PropertyField (m_FunctionType, new GUIContent ("FunctionType"), false);
		
			if (oldType != object3d.M_FunctionType) {
				object3d._ChangeFunction ();
				if (object3d as Block) {
					Block block = (Block)object3d;
					for (int i=0; i<block.M_Bricks.Count; i++) {
						EditorUtility.SetDirty (block.M_Bricks [i]);
					}
				}
				Debug.Log ("ddf");
			}
			object3d.M_Old_FunctionType = object3d.M_FunctionType;
		}
	}

	public void DrawMoveParma ()
	{
		if (EditorTools.DrawHeader ("MOVE PROPERTIES")) {
			Object3d object3d = target as Object3d;
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

			if (object3d.M_MoveIn_CT != MoveIn_ConditionType.EMPTY) {
			

				Vector3 moveInSpan = EditorGUILayout.Vector3Field ("MoveInSpan", object3d.M_MoveIn_Span);
				int moveInDuration = EditorGUILayout.IntField ("MoveInDuration", object3d.M_MoveIn_Duration);
				int moveIn_ArriveTime = EditorGUILayout.IntField ("MoveIn_ArriveTime", object3d.M_MoveIn_ArriveTime);
				int moveIn_Delay = EditorGUILayout.IntField ("MoveIn_Delay", object3d.M_MoveIn_Delay);
			
				if (GUI.changed) {
					object3d.M_MoveIn_Span = moveInSpan;
					object3d.M_MoveIn_Duration = moveInDuration;
					object3d.M_MoveIn_ArriveTime = moveIn_ArriveTime;
					object3d.M_MoveIn_Delay = moveIn_Delay;
					EditorUtility.SetDirty (object3d);
				}
			}

			GUILayout.Space (16f);
			GUI.changed = false;
			SerializedProperty moveOutCT = serializedObject.FindProperty ("m_MoveOut_CT");
			EditorGUILayout.PropertyField (moveOutCT, new GUIContent ("moveOutCT"), false);
			
			if (object3d.M_MoveOut_CT != MoveOut_ConditionType.EMPTY) {
				Vector3 moveOutSpan = EditorGUILayout.Vector3Field ("MoveOutSpan", object3d.M_MoveOut_Span);
				int moveOutDuration = EditorGUILayout.IntField ("MoveOutDuration", object3d.M_MoveOut_Duration);
				int moveOut_LeaveTime = EditorGUILayout.IntField ("MoveOut_LeaveTime", object3d.M_MoveOut_LeaveTime);
				int moveOut_Delay = EditorGUILayout.IntField ("MoveOut_Delay", object3d.M_MoveOut_Delay);
			
				if (GUI.changed) {
					object3d.M_MoveOut_Span = moveOutSpan;
					object3d.M_MoveOut_Duration = moveOutDuration;
					object3d.M_MoveOut_LeaveTime = moveOut_LeaveTime;
					object3d.M_MoveOut_Delay = moveOut_Delay;
					EditorUtility.SetDirty (object3d);
				}
			}
			
		}
	}



}