using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class M3DUtil
{


		public static void DestoryAllChild (GameObject parent)
		{
				Transform [] tfs = parent.GetComponentsInChildren<Transform> ();
				for (int i=0; i<tfs.Length; i++) {
						if ( tfs[i]!=null &&  tfs [i].name != parent.name)
								GameObject.DestroyImmediate (tfs [i].gameObject);
				}
		}

		public static void SetItemVisible (MonoBehaviour behavior, bool visible, bool all)
		{
				if (behavior == null) {
						return;
				}
				SetItemVisible (behavior.gameObject, visible, all);
//				if (behavior.gameObject.active == visible)
//						return;
//				if (all)
//						behavior.gameObject.SetActiveRecursively (visible);
//				else
//						behavior.gameObject.active = visible;
		}

		public static void SetItemVisible (GameObject gameObject, bool visible, bool all)
		{
				if (gameObject == null) {
						return;
				}
				if (gameObject.active == visible)
						return;
				if (all)
						gameObject.SetActiveRecursively (visible);
				else
						gameObject.active = visible;
		}
	
		public static void Destory_ListGameObjects (List<GameObject> objects)
		{
				if (objects == null)
						return;
				for (int i=0; i<objects.Count; i++) {
						if (objects [i] != null)
								GameObject.DestroyImmediate (objects [i]);
				}
				objects.Clear ();
		}
	
		public static bool IsInteract (Vector3 f, Vector3 t, int lay)
		{

				Vector3 direction = t - f;	
				return  Physics.Raycast (f, direction, direction.magnitude, lay);
//	
		
		}
	
		public static  string GetTrueName (string name)
		{
				return name.Replace ("(Clone)", "");
		}
	
		public static bool IsInBoxColliderBounds (BoxCollider collider, Vector3 pot)
		{
				Vector3 locPot = collider.transform.InverseTransformPoint (pot);
				Vector3 dPot = locPot - collider.center;
				if (Mathf.Abs (dPot.x) < collider.extents.x && Mathf.Abs (dPot.y) < collider.extents.y && Mathf.Abs (dPot.z) < collider.extents.z) {
						return true;
				}
				return false;
		}

		public static bool IsInBoxColliderBounds_XZ (BoxCollider collider, Vector3 pot)
		{
				Vector3 locPot = collider.transform.InverseTransformPoint (pot);
				Vector3 dPot = locPot - collider.center;
				if (Mathf.Abs (dPot.x) < collider.extents.x && Mathf.Abs (dPot.z) < collider.extents.z) {
						return true;
				}
				return false;
		}

		
	
		public static GameObject GetParentOfTheChild (Transform childTf, string parentName)
		{
				if (childTf.name == parentName)
						return childTf.gameObject;
				while (childTf.parent!=null) {
						childTf = childTf.parent;
						if (childTf.name == parentName)
								return childTf.gameObject;
				}
				return null;
		}
	
		public static bool IsTheChildOfParent (Transform childTf, string parentName)
		{
				//if self name is the parent, return false;
				if (childTf.name == parentName)
						return false;
		
				//check parent 
				while (childTf.parent!=null) {
						childTf = childTf.parent;
						if (childTf.name == parentName)
								return true;
				}
				return false;
		}

		public static 	Transform GetObjectParent (Transform tf)
		{
				while (tf.parent!=null) {
						tf = tf.parent;
				}
				return tf;
		}
	
		public static Transform GetObjectChild (Transform parent, string name)
		{
				Transform [] tfs = parent.GetComponentsInChildren<Transform> ();
				for (int i=0; i<tfs.Length; i++) {
						if (tfs [i].name == name)
								return tfs [i];
				}
				return null;
		}
	
		public static  bool IsInViewFeild (Vector3 selfDir, Vector3 toTargetDir, float feildAnagle)
		{
		
				float cos = Vector3.Dot (toTargetDir.normalized, selfDir.normalized);
				if (cos > Mathf.Cos (feildAnagle * 0.5f * Mathf.Deg2Rad))
						return true;
				return false;
		}
	
		public static bool IsInValidateAttackRange (Vector3 selfPot, Vector3 targetPot, float vDst, Vector3 selfDir, Vector3 targetDir, float feildAngle)
		{ 
				return (selfPot - targetPot).sqrMagnitude < vDst * vDst && IsInViewFeild (selfDir, targetDir, feildAngle);
		}
	
		public static void AddEffect (GameObject parent, GameObject prefab, Vector3 positionSpan)
		{
				GameObject instanceEffect = 
			(GameObject)Object.Instantiate (prefab);
		
				instanceEffect.transform.parent = parent.transform;
				instanceEffect.transform.localPosition = Vector3.zero;
				instanceEffect.transform.localRotation = Quaternion.identity;
				instanceEffect.transform.localScale = Vector3.one;
				instanceEffect.transform.localPosition += positionSpan;
				instanceEffect.transform.parent = null;
		}

		public static void AddEffect_World (GameObject parent, GameObject prefab, Vector3 worldPot)
		{
				GameObject instanceEffect = 
			(GameObject)Object.Instantiate (prefab);
		
				instanceEffect.transform.parent = parent.transform;
				instanceEffect.transform.localPosition = Vector3.zero;
				instanceEffect.transform.localRotation = Quaternion.identity;
				instanceEffect.transform.localScale = Vector3.one;
				instanceEffect.transform.position = worldPot;
				instanceEffect.transform.parent = null;
		}
	
		public static void InitLocalTf (Transform tf)
		{
				tf.localPosition = Vector3.zero;
				tf.localRotation = Quaternion.identity;
				tf.localScale = Vector3.one;
		}
	
	
	
	
	#region ngui
	
		public static  Vector3 ScreenPos_to_NGUIPos (Vector3 screenPos)
		{  
				Vector3 uiPos = UICamera.currentCamera.ScreenToWorldPoint (screenPos);  
				uiPos = UICamera.currentCamera.transform.InverseTransformPoint (uiPos);  
				return uiPos;  
		}
   
		public static  Vector3 ScreenPos_to_NGUIPos (Vector2 screenPos)
		{  
				return ScreenPos_to_NGUIPos (new Vector3 (screenPos.x, screenPos.y, 0f));  
		}  
	#endregion
}
