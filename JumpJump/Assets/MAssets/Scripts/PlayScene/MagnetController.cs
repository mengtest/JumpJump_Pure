using UnityEngine;
using System.Collections;

public class MagnetController : MonoBehaviour ,IPoolable
{
	#region IPoolable implementation

	public void IReset ()
	{
		this.gameObject.SetActive (false);
		this.transform.parent=null;
	}

	public void IDestory ()
	{

	}

	#endregion



	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter (Collider other)
	{
	
		if(other.name=="Player"){
			PlayerController pC=other.gameObject.GetComponent<PlayerController>();
			pC.AttachMagnet();
			this.gameObject.SetActive(false);
		}
	}


}
