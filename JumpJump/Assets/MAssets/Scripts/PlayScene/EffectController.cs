using UnityEngine;
using System.Collections;

public class EffectController : MonoBehaviour
{

	public 	ParticleSystem particleSystem; 

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	
	}

	public void PlayEffect ()
	{
		this.gameObject.SetActive (true);
		particleSystem.Play ();
	}

	public void CloseEffect ()
	{
		this.gameObject.SetActive (false);
	}

	void LateUpdate ()
	{
		
		if (!particleSystem.IsAlive ())
//			Object.Destroy (this.gameObject);    
			CloseEffect ();
	}
}
