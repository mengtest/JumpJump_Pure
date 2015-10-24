using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public Vector3 moveSpeed = new Vector3 (3, 0, 0);
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
	}
	
	// Update is called once per frame
	void Update ()
	{
		CheckGrounded ();
		CheckFailed ();
	}

	void FixedUpdate ()
	{


		Vector3 velocityChange = Vector3.right * moveSpeed.x - m_rigidbody.velocity;
		velocityChange.y = 0;
		m_rigidbody.AddForce (velocityChange, ForceMode.VelocityChange);


//		m_OnGround = false;
	}

	void OnCollisionEnter (Collision collsion)
	{	
		CheckFaild (collsion);
		m_OnGround=true;
		m_JumpTimes = 0;
	}

	void OnCollisionStay (Collision collsion)
	{
		CheckFaild (collsion);
		m_OnGround = true;
	}

	void OnCollsionExit(Collision collsion){

	}
	
	public void OnTouchDown ()
	{
		if ( m_JumpTimes < m_MaxJumpTimes) {

			m_rigidbody.AddForce (Vector3.up * moveSpeed.y, ForceMode.VelocityChange);
			m_JumpTimes++;
		}
	}
	
	Ray ray;

	void CheckGrounded ()
	{
		ray.origin = m_sphereCollider.bounds.center;
		ray.direction = Vector3.down;
		float dst = m_sphereCollider.radius + 0.2f;
		if (Physics.Raycast (ray, dst)) {
			m_OnGround = true;
//			m_JumpTimes = 0;
		} else {
			m_OnGround = false;
		}
	}

	void CheckFaild (Collision collision)
	{
		float threasholdHeight = m_sphereCollider.bounds.center.y - m_sphereCollider.radius * 0.5f;
		
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
		if (transform.position.y < 0) {
			OnFaild ();
		}
	}

	public void Reset ()
	{
		transform.position = m_InitPot;
		transform.rotation = Quaternion.Euler (m_InitRotate);
		m_OnGround = false;

	}

	public bool TriggerBlockActive_ArriveTime (Object3d o3d)
	{
		return moveSpeed.x * o3d.Get_ACT_ArriveTime () >= o3d.transform.position.x - transform.position.x;
	}

	public bool TriggerBlockMoveIn_ArriveTime (Object3d o3d)
	{
		return moveSpeed.x * o3d.Get_MoveIn_ArriveTime () >= o3d.transform.position.x - transform.position.x;
	}
	
}
