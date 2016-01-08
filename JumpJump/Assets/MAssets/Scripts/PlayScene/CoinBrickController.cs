using UnityEngine;
using System.Collections;

public class CoinBrickController : MonoBehaviour
{
	public GameObject coinObj;

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
