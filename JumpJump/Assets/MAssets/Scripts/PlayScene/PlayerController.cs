using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public Vector3 moveSpeed = new Vector3 (3, 0, 0);
	Vector3 m_InitMoveSpeed;
	Rigidbody m_rigidbody;
	bool m_OnGround = false;
	SphereCollider m_sphereCollider;
	Vector3 m_InitPot;
	Vector3 m_InitRotate;
	int m_JumpTimes = 0;
	int m_MaxJumpTimes = 1;
	public Vector3 m_Gravity = new Vector3 (0, -9.8f, 0);

	// Use this for initialization
	void Start ()
	{
		m_rigidbody = GetComponent<Rigidbody> ();
		m_sphereCollider = GetComponent<SphereCollider> ();

		m_InitPot = transform.position;
		m_InitRotate = transform.eulerAngles;

		m_rigidbody.angularVelocity = Vector3.right * 0.1f;

		Physics.gravity = m_Gravity;
		m_InitMoveSpeed = moveSpeed;

		m_sphereCollider.material = Resources.Load ("PhysicMaterial/Ice") as PhysicMaterial;
	}
	
	// Update is called once per frame
	void Update ()
	{
		CheckGrounded ();
		CheckFailed ();
	}

	void FixedUpdate ()
	{


		Vector3 velocityChange = Vector3.right * GetMoveSpeed () - m_rigidbody.velocity;
		velocityChange.y = 0;
		m_rigidbody.AddForce (velocityChange, ForceMode.VelocityChange);


		m_OnGround = false;
	}

	float GetMoveSpeed ()
	{
		return m_OnGround ? moveSpeed.x : moveSpeed.x * 0.9f;
	}

	void OnCollisionEnter (Collision collsion)
	{	
		CheckFaild (collsion);
		m_OnGround = true;
//		m_JumpTimes = 0;
		OnFunction (collsion);
//		m_JumpValidateTime=0;

	}

	void OnCollisionStay (Collision collsion)
	{
		CheckFaild (collsion);
		m_OnGround = true;
//		m_JumpValidateTime=0;
	}

	void OnCollisionExit (Collision collsion)
	{
		Brick brick = collsion.gameObject.GetComponent<Brick> ();
		if (brick != null) {
			brick.M_IsPlayerCollideExit = true;
		}
//		m_OnGround=false;
	}

	float m_JumpValidateTime = 0;
	
	public void OnTouchDown ()
	{

		if (m_JumpTimes < m_MaxJumpTimes) { 
			if (m_JumpTimes == 0 && (m_JumpValidateTime += Time.deltaTime) > 0.03f) {
				return;
			}
			Vector3 v = m_rigidbody.velocity;
			v.y = moveSpeed.y;
			m_rigidbody.velocity = v;
			m_JumpTimes++;
		}
	}
	
	Ray ray;

	void CheckGrounded ()
	{
		Vector3 origin = m_sphereCollider.bounds.center;
		ray.origin = origin;
		ray.direction = Vector3.down;
		float dst = m_sphereCollider.radius + 0.05f;
//		if (Physics.Raycast (ray, dst) && m_rigidbody.velocity.y<0 ) {
//			m_JumpTimes = 0;
//		} else {
//		}


		Debug.DrawLine (ray.origin, ray.origin + ray.direction * dst, Color.red);


		if (m_rigidbody.velocity.y > 0)
			return;

		int lay = 1 << LayerMask.NameToLayer ("Player");

		RaycastHit hit;
		bool check1 = Physics.Raycast (ray, out hit, dst, ~lay);
//		if (check1) {
//			m_JumpTimes = 0;
//			m_JumpValidateTime = 0;
//			Debug.Log (" hit pot=" + hit.point + " hit name=" + hit.collider.name + " m_rigidbody.velocity.y=" + m_rigidbody.velocity.y);
//		}

		origin.x=m_sphereCollider.bounds.center.x+m_sphereCollider.radius;
		ray.origin=origin;
		bool check2 = Physics.Raycast (ray, out hit, dst, ~lay);
//		if (check2) {
//			m_JumpTimes = 0;
//			m_JumpValidateTime = 0;
//			Debug.Log (" hit pot=" + hit.point + " hit name=" + hit.collider.name + " m_rigidbody.velocity.y=" + m_rigidbody.velocity.y);
//		}
		origin.x=m_sphereCollider.bounds.center.x+m_sphereCollider.radius;
				ray.origin=origin;
		bool check3 = Physics.Raycast (ray, out hit, dst, ~lay);
		if (check1 || check2 || check3) {
			m_JumpTimes = 0;
			m_JumpValidateTime = 0;
//			Debug.Log (" hit pot=" + hit.point + " hit name=" + hit.collider.name + " m_rigidbody.velocity.y=" + m_rigidbody.velocity.y);
		}




//		bool check1=Physics.Raycast(ray,dst,~lay);
//		origin.x=m_sphereCollider.bounds.center.x-m_sphereCollider.radius;
//		ray.origin=origin;
//		bool check2=Physics.Raycast(ray,dst,~lay);
//		origin.x=m_sphereCollider.bounds.center.x+m_sphereCollider.radius;
//		ray.origin=origin;
//		bool check3=Physics.Raycast(ray,dst,~lay);
//
//		if(check1 || check2 || check3){
//			m_JumpTimes=0;
//			m_JumpValidateTime=0; Debug.Log(" ******** dst="+dst+" ray.origion"+ray.origin);
//		}

	}

	void CheckFaild (Collision collision)
	{
		float threasholdHeight = m_sphereCollider.bounds.center.y - m_sphereCollider.radius * 0.4f;
		
		for (int i=0; i<collision.contacts.Length; i++) {
			ContactPoint cp = collision.contacts [i]; 
			if (cp.point.y > threasholdHeight && cp.point.x > m_sphereCollider.bounds.center.x) { 
				OnFaild ();
				break;
			}
		}
	}

	void OnFaild ()
	{
		PlayGameInstance.INSTANCE.OnGameResult ();
	}
	
	void CheckFailed ()
	{
		if (transform.position.y < -0.1f) {
			OnFaild ();
		}
	}

	public void Reset ()
	{
		transform.position = m_InitPot;
		transform.rotation = Quaternion.Euler (m_InitRotate);
		m_OnGround = false;
		SetEmptyFunction ();
		m_JumpTimes = 0;
		m_JumpValidateTime = 0;
		m_rigidbody.velocity = Vector3.zero;


	}

	public bool TriggerBlockActive_ArriveTime (Object3d o3d)
	{
		return moveSpeed.x * o3d.Get_ACT_ArriveTime () >= o3d.transform.position.x - transform.position.x;
	}

	public bool TriggerBlockMoveIn_ArriveTime (Object3d o3d)
	{
		return moveSpeed.x * o3d.Get_MoveIn_ArriveTime () >= o3d.transform.position.x - transform.position.x;
	}

	public bool TriggerBlockMoveOut_LeaveTime (Object3d o3d)
	{
		return moveSpeed.x * o3d.Get_MoveOut_LeaveTime () <= transform.position.x - o3d.M_Max_EndX;
	}

	void OnFunction (Collision collsion)
	{
		Brick brick = collsion.gameObject.GetComponent<Brick> ();
		if (brick != null) {
			switch (brick.M_FunctionType) {
			case FunctionType.EMPTY:
				break;
			case FunctionType.REMOVE:
				SetEmptyFunction ();
				break;
			case FunctionType.JUMP_HEIGHTER:
				moveSpeed.y = m_InitMoveSpeed.y * 1.2f;
				break;
			}
		}
	}

	void SetEmptyFunction ()
	{
		moveSpeed = m_InitMoveSpeed;
	}
	
}
