using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class SetParticleSortingLayer : MonoBehaviour {

	public string sortingLayerName="Default";
	public int sortingOrder=0;

	// Use this for initialization
	void Start () {
		Onchanged ();
	}
	#if UNITY_EDITOR
	// Update is called once per frame
	void Update () {
//		if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode) {
//			this.enabled = false;
//		} else {
			Onchanged();
//		}
	}
	#else

	#endif

	private void Onchanged(){
		if (GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName != sortingLayerName ||GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder != sortingOrder) {
			GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = sortingLayerName;
			GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = sortingOrder;
		}
	}
}
