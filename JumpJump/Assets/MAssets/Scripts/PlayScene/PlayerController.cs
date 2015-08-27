using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public Vector3 v = new Vector3 (2.5f, 6f, 0);
	Rigidbody rb;
	Renderer rd;

	int jumpTimes=0;
	int maxJumpTimes=1;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> (); 
		rd=GetComponent<Renderer>();

		transform.localPosition = new Vector3 (0, 3, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			OnTouchDown ();
		}
	}

	void OnCollisionEnter (Collision collsion)
	{	
		rb.velocity = v;
		jumpTimes=0;
		DynamicBrick db=collsion.gameObject.GetComponent<DynamicBrickController>().DB;
		rd.material.color=db.BColor.C;

		PlaySceneController.instance.BM.GetLeaveBricks(db);

	}


	public void OnTouchDown ()
	{
		if (jumpTimes < maxJumpTimes) {
			rb.velocity = v;
		}
		jumpTimes++;
	}

}
