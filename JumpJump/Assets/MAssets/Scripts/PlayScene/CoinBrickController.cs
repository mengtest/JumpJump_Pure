using UnityEngine;
using System.Collections;

public class CoinBrickController : MonoBehaviour
{
	public GameObject coinObj;
	public TweenRotation coinTweenRotation;

	void Awake(){
		int randomY=Random.Range(0,360);
		Vector3 from =new Vector3(0,randomY,0);
		Vector3 to=new Vector3(0,randomY+360,0);
		coinTweenRotation.from=from;
		coinTweenRotation.to=to;

	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void IReset ()
	{
		coinObj.SetActive (true);
	}

	public void HideCoin ()
	{
		coinObj.SetActive (false);
	}
}
