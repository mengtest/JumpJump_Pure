using UnityEngine;
using System.Collections;

public class UpcastAnimation : MonoBehaviour
{
	
	Vector3 initPot;
	Vector3 initScale;
	Vector3 curPot;
	Vector3 curScale;

	public delegate  void Delegate_OnFinished();
	public Delegate_OnFinished  OnFinished;

	void Awake(){
		initPot = transform.localPosition;
		initScale = transform.localScale;
	}
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdatePotAndScale ();
	}
	
	public float minVel = 0f;
	public float maxVel = 20f;
	public float minAngle = -20f;
	public float maxAngle = 20f;
	public float dynamicTime = 1f;
	public float gravity = 10f;
	public float startScale = 1f;
	public float endScale = 2f;
	float x, y;
	float vel, angle, duration, s;
	float vx, vy;
	
	void GetRandom ()
	{
		vel = Random.Range (minVel, maxVel);
		angle = Random.Range (minAngle, maxAngle);
		vx = vel * Mathf.Sin (Mathf.Deg2Rad * angle);
		vy = vel * Mathf.Cos (Mathf.Deg2Rad * angle);
		duration = 1.2f*vy / gravity + dynamicTime;
	}
	
	float t = 0;
	bool start = false;
	
	void UpdatePotAndScale ()
	{
		if (!start)
			return;
		

		if (t < duration) {
			curPot.x = initPot.x + vx * t;
			curPot.y = initPot.y + vy * t - 0.5f * gravity * t * t;	
			curPot.z=initPot.z;
			s = Mathf.Lerp (startScale, endScale, t / duration);
		} else {
			s = endScale;
			start = false;
			if(OnFinished!=null) OnFinished();
		}
		
		curScale = s * initScale;
		
		transform.localPosition = curPot;
		transform.localScale = curScale;

		t += Time.deltaTime;

//		DebuggerUtil.Log(" cur pot="+curPot +" init pot="+initPot +" t="+t +" h="+(vy * t - 0.5f * gravity * t * t));
	}
	


	public void StartAnimation ()
	{
		start = true;
		t=0;
		GetRandom ();
		initPot = transform.localPosition;
		transform.localScale = initScale;
		DebuggerUtil.Log(" init pot="+initPot);
	}

	
	
	public bool IsAnimationing(){
		return start;
	}

	
	
}
