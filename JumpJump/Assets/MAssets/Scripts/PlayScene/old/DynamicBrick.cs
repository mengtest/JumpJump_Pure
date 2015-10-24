using UnityEngine;
using System.Collections;

public class DynamicBrick :IPoolable
{
	GameObject go;
	
	public GameObject Go {
		get { return go;}
	}
	
	BrickColor bColor;
	
	public BrickColor BColor {
		get { return bColor;}
		set { bColor = value;}
	}
	
	Vector3 initPot;
	
	public Vector3 InitPot {
		get { return initPot;}
		set { initPot = value;}
	}
	
	Vector3 targetPot;
	
	public Vector3 TargetPot {
		get { return targetPot;}
		set { targetPot = value;}
	}
	
	float time = 0;
	float delayTime = 0;
	
	public float DelayTime {
		get { return delayTime;}
		set { delayTime = value;}
	}
	
	bool comming;
	
	public bool Comming {
		get{ return comming;}
		set{ comming = value;}
	}
	
	bool over = false;
	
	public bool Over {
		get { return over;}
		set { over = value;}
	}
	
	float moveTime = 0;
	int frame = 0;
	int leastFrame = 0;
	
	public int LeastFrame {
		get { return leastFrame;}
		set { leastFrame = value;}
	}
	
	bool useConstantSpeed = true;
	
	public bool UseConstantSpeed {
		get { return useConstantSpeed;}
		set { useConstantSpeed = value;}
	}
	
	float maxMovetime = 0;
	
	public float MaxMoveTime {
		set { maxMovetime = value;}
	}
	
	public DynamicBrick (GameObject go)
	{
		this.go = go;
	}
	
	public void Update ()
	{
		if (over)
			return;
		
		if (comming) {
			if (time > delayTime && frame > leastFrame) {
				if (!go.activeSelf)
					go.SetActive (true);
				
				if (useConstantSpeed)
					UpdatePot_ConstantSpeed ();
				else
					UpdatePot_VaryingSpeed ();
			}
		} else {
			if (time > delayTime && frame > leastFrame) {
				
				if (useConstantSpeed)
					UpdatePot_ConstantSpeed ();
				else
					UpdatePot_VaryingSpeed ();
				
				if (over && go.activeSelf)
					go.SetActive (false);
			}
		}
		
		time += Time.deltaTime;
		frame++;
	}
	
	void UpdatePot_VaryingSpeed ()
	{
		Vector3 curPot = this.go.transform.localPosition;
		this.go.transform.localPosition = Vector3 .Lerp (curPot, targetPot, Time.deltaTime * 5);
		if ((curPot - targetPot).sqrMagnitude < 0.001f * 0.001f)
			over = true;
		
	}
	
	void UpdatePot_ConstantSpeed ()
	{
		if (moveTime < maxMovetime) {
			moveTime += Time.deltaTime * 0.8f;
		} else {
			moveTime = maxMovetime;
			over = true;
		}
		this.go.transform.localPosition = Vector3 .Lerp (initPot, targetPot, Mathf.Clamp (moveTime / maxMovetime, 0f, 1f));
	}
	
	public void IReset ()
	{
		time = 0;
		delayTime = 0;
		comming = true;
		over = false;
		moveTime = 0;
		frame = 0;
		leastFrame = 0;
		useConstantSpeed = false;
		maxMovetime = 1f;
		
	}

	public void IDestory ()
	{
		if (go != null) {
			GameObject.Destroy (go);
			go = null;
		}
	}

}
