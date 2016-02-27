using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public Vector3 moveSpeed = new Vector3 (3, 0, 0);
	Vector3 m_InitMoveSpeed;
	Rigidbody m_Rigidbody;
	Renderer m_Renderer;
	Material m_InitMtl;
	bool m_OnGround = false;
	SphereCollider m_sphereCollider;
	Vector3 m_InitPot;
	Vector3 m_InitRotate;
	int m_JumpTimes = 0;
	int m_MaxJumpTimes = 1;
	Vector3 m_OldVelocity;
	public Vector3 m_Gravity = new Vector3 (0, -9.8f, 0);
	public GameObject magnetEffect;

	float GetMoveSpeed ()
	{
		return m_OnGround ? moveSpeed.x : moveSpeed.x * 0.9f;
	}
	
	#region init and reset
	void Init ()
	{
		m_Rigidbody = GetComponent<Rigidbody> ();
		m_sphereCollider = GetComponent<SphereCollider> ();
		m_Renderer = GetComponent<Renderer> ();
		m_InitMtl = m_Renderer.material;
		
		m_InitPot = transform.position;
		m_InitRotate = transform.eulerAngles;
		
		m_Rigidbody.angularVelocity = Vector3.right * 0.1f;
		
		Physics.gravity = m_Gravity;
		m_InitMoveSpeed = moveSpeed;
		
		m_sphereCollider.material = Resources.Load ("PhysicMaterial/Ice") as PhysicMaterial;

		m_Unbeatable_Timer = new MTimer (1f);
		m_Unbeatable_Timer.OnTime += OnUnbeatable_Over;

		m_Skill_Timer = new MTimer (10f);
		m_Skill_Timer.OnTime += OnSkill_Over;

		InitMagnetTimer();
		m_MagnetRange=m_InitMagnetRange;


	}

	float m_InitMagnetRange=1.1f;
	public void Reset ()
	{
		transform.position = m_InitPot;
		transform.rotation = Quaternion.Euler (m_InitRotate);
		m_OnGround = false;
		SetEmptyFunction ();
		m_JumpTimes = 0;
		m_JumpValidateTime = 0;
		m_Rigidbody.velocity = Vector3.zero;
		m_Renderer.material = m_InitMtl;
		m_Unbeatable_Timer.Pause ();
		m_IsUnbeatable = false;
		vel_Type = VEL_TYPE_NOMAL;
		moveSpeed = m_InitMoveSpeed;
		m_Skill_Timer.Pause ();
		m_AttachMagnetTimer.Pause();
		PlayGameInstance.INSTANCE.PSC.PC.m_MagnetRange=m_InitMagnetRange;
		magnetEffect.SetActive(false);
	}

	#endregion

	#region base
	// Use this for initialization
	void Start ()
	{
		Init ();
	}
	
	// Update is called once per frame
	void Update ()
	{

		CheckGrounded ();
		CheckFailed ();
		m_OldVelocity = m_Rigidbody.velocity;
		m_Unbeatable_Timer.Update ();
		m_Skill_Timer.Update ();
		m_AttachMagnetTimer.Update();

		tmp = transform.position;
		tmp.z = 0;
		transform.position = tmp;

	}

	Vector3 tmp;

	void FixedUpdate ()
	{

		if (m_IsUnbeatable) {
			FixUpdate_Unbeatable ();
			return;
		}

		Vector3 velocityChange = Vector3.right * GetMoveSpeed () - m_Rigidbody.velocity;
		velocityChange.y = 0;
		m_Rigidbody.AddForce (velocityChange, ForceMode.VelocityChange);


		m_OnGround = false;
	}

	void OnCollisionEnter (Collision collsion)
	{	
		CheckFailed (collsion);
		m_OnGround = true;
		OnFunction (collsion);
	}
	
	void OnCollisionStay (Collision collsion)
	{
		CheckFailed (collsion);
		m_OnGround = true;
	}
	
	void OnCollisionExit (Collision collsion)
	{
		Brick brick = collsion.gameObject.GetComponent<Brick> ();
		if (brick != null) {
			brick.M_IsPlayerCollideExit = true;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (m_IsUnbeatable) {
			Brick brick = other.gameObject.GetComponent<Brick> ();
			UnbeatableOnBrick (brick);
		}
//		if(other.name=="Magnet"){
//			AttachMagnet();
//			MagnetController mC=other.gameObject.GetComponent<MagnetController>();
//			mC.gameObject.SetActive(false);
//		}
	}

	void OnTriggerStay (Collider other)
	{
		if (m_IsUnbeatable) {
			Brick brick = other.gameObject.GetComponent<Brick> ();
			UnbeatableOnBrick (brick);
		}
	}

	void OnTriggerExit (Collider other)
	{

	}
	
	
	#endregion

	#region event
	float m_JumpValidateTime = 0;
	public float m_Max_JumpValidateTime = 0.05f;
	
	public void OnTouchDownScreen ()
	{
		
		if (m_JumpTimes < m_MaxJumpTimes) { 
			if (m_JumpTimes == 0 && (m_JumpValidateTime += Time.deltaTime) > m_Max_JumpValidateTime) {
				return;
			}
			Vector3 v = m_Rigidbody.velocity;
			v.y = moveSpeed.y;
			m_Rigidbody.velocity = v;
			m_JumpTimes++;
		}
	}

	MTimer m_Skill_Timer;
	float m_Skill_Duration = 10f;
	int vel_Type = 0;
	const  int VEL_TYPE_NOMAL = 0;
	const int VEL_TYPE_SPEEDUP = 1;
	const int VEL_TYPE_SPEEDDOWN = 2;

	void UpdateVel ()
	{
		if (m_IsUnbeatable)
			return;
		switch (vel_Type) {
		case VEL_TYPE_NOMAL:
			moveSpeed.x = m_InitMoveSpeed.x;
			break;
		case VEL_TYPE_SPEEDUP:
			moveSpeed.x = m_InitMoveSpeed.x * 1.5f;
			break;
		case VEL_TYPE_SPEEDDOWN:
			moveSpeed.x = m_InitMoveSpeed.x * 0.5f;
			break;
		}
	}

	public void OnSkill_SpeedUp ()
	{
		vel_Type = VEL_TYPE_SPEEDUP;
		UpdateVel ();
		m_Skill_Timer.Restart (false, m_Skill_Duration);

//		moveSpeed.x = m_InitMoveSpeed.x * 1.5f;
		DebuggerUtil.Log (" start on skill speed up");
	}
	
	public void OnSkill_SlownDown ()
	{
		vel_Type = VEL_TYPE_SPEEDDOWN;
		UpdateVel ();
		m_Skill_Timer.Restart (false, m_Skill_Duration);
//		moveSpeed.x = m_InitMoveSpeed.x * 0.5f;
		DebuggerUtil.Log (" start on skill slown down");
	}

	void OnSkill_Over ()
	{
		vel_Type = VEL_TYPE_NOMAL;
		UpdateVel ();
		m_Skill_Timer.Pause ();
	}

	#endregion

	#region check
	Ray ray;
	
	void CheckGrounded ()
	{
		Vector3 origin = m_sphereCollider.bounds.center;
		ray.origin = origin;
		ray.direction = Vector3.down;
		float dst = m_sphereCollider.radius + 0.1f;
		
		if (m_Rigidbody.velocity.y > 0)
			return;
		
		int lay = 1 << LayerMask.NameToLayer ("Player");
		
		RaycastHit hit;
		bool check1 = Physics.Raycast (ray, out hit, dst, ~lay);
		
		
		origin.x = m_sphereCollider.bounds.center.x + m_sphereCollider.radius;
		ray.origin = origin;
		bool check2 = Physics.Raycast (ray, out hit, dst, ~lay);
		
		origin.x = m_sphereCollider.bounds.center.x - m_sphereCollider.radius;
		ray.origin = origin;
		bool check3 = Physics.Raycast (ray, out hit, dst, ~lay);
		if (check1 || check2 || check3) {
			m_JumpTimes = 0;
			m_JumpValidateTime = 0;
			//			Debug.Log (" hit pot=" + hit.point + " hit name=" + hit.collider.name + " m_Rigidbody.velocity.y=" + m_Rigidbody.velocity.y);
		}
		
	}
	
	void CheckFailed (Collision collision)
	{
		
		
		float threasholdHeight = m_sphereCollider.bounds.center.y - m_sphereCollider.radius * 0.4f;
		float threasholdHeight2 = m_sphereCollider.bounds.center.y + m_sphereCollider.radius * 0.4f;
		
		for (int i=0; i<collision.contacts.Length; i++) {
			ContactPoint cp = collision.contacts [i]; 
			if ((cp.point.y > threasholdHeight && cp.point.x > m_sphereCollider.bounds.center.x) || 
				(m_IsUnbeatable && cp.point.y > threasholdHeight2)) {
//				if (m_IsUnbeatable) {
//					Brick brick = collision.gameObject.GetComponent<Brick> ();
//					UnbeatableOnBrick (brick);
//					return;
//				}
				////// if brick is collided by unbeatable collide, not check 
				Brick brick = collision.gameObject.GetComponent<Brick> ();
				if (brick.M_OnUnbeatableCollided) {
					return;
				}
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
		float minY = m_IsUnbeatable ? -1f : - 0.5f;
		if (transform.position.y < minY) {
			OnFaild ();
		}
	}
	#endregion


	#region delegate function
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

	#endregion 


	#region function
	void OnFunction (Collision collsion)
	{
		Brick brick = collsion.gameObject.GetComponent<Brick> ();
		if (brick != null) {
//			Debug.Log (" type=" + brick.M_FunctionType);
			switch (brick.M_FunctionType) {
			case FunctionType.EMPTY:
				return;
			case FunctionType.REMOVE:
				SetEmptyFunction ();
				GameData.Instance ().M_RunningData.M_RoleState = "Normal";
				break;
			case FunctionType.JUMP_HEIGHTER:
				SetEmptyFunction ();
				moveSpeed.y = m_InitMoveSpeed.y * 1.2f;
				GameData.Instance ().M_RunningData.M_RoleState = "Higher";
				break;
			case FunctionType.JUMP_TWICE:
				SetEmptyFunction ();
				m_MaxJumpTimes = 2;
				GameData.Instance ().M_RunningData.M_RoleState = "Twice";
				break;
			case FunctionType.SPEED_UP:
				SetEmptyFunction ();
				moveSpeed.x = m_InitMoveSpeed.x * 1.2f;
				break;
			case FunctionType.SPEED_DOWN:
				SetEmptyFunction ();
				moveSpeed.x = m_InitMoveSpeed.x * 0.8f;
				break;
			case FunctionType.UNBEATABLE:
				SetEmptyFunction ();
				OnUnbeatable ();
				GameData.Instance ().M_RunningData.M_RoleState = "Unbeatable";
				break;
			}
			if (brick.M_FunctionType == FunctionType.REMOVE) {
				m_Renderer.material=m_InitMtl;
			} else {
				m_Renderer.material = brick.GetComponentInChildren<Renderer> ().material;
			}
//			m_Renderer.material = brick.GetComponent<Renderer> ().material;

			CheckOnCoinBrick (brick);
		}
	}

	void SetEmptyFunction ()
	{
		moveSpeed.y = m_InitMoveSpeed.y;
		m_MaxJumpTimes = 1;
	}
	#endregion


	#region unbeatable 

	public float m_Unbeatable_SpeedRate = 2f;
	bool m_IsUnbeatable = false;
	public float m_Unbeatable_Duration = 2f;
	MTimer m_Unbeatable_Timer;

	void OnUnbeatable ()
	{
		m_Unbeatable_Timer.Restart (false, m_Unbeatable_Duration);
		m_IsUnbeatable = true;
		m_Rigidbody.useGravity = false;
		OpenKinematicAndTrigger ();
	}

	void OnUnbeatable_Over ()
	{
		m_Unbeatable_Timer.Pause ();
		m_IsUnbeatable = false;
		m_Rigidbody.useGravity = true;
		m_Rigidbody.velocity = Vector3.up * m_InitMoveSpeed.y;
		CloseKinematicAndTrigger ();

		GameData.Instance ().M_RunningData.M_RoleState = "Normal";
		m_Renderer.material = m_InitMtl;

		UpdateVel ();
	}

	Vector3 m_Unbeatable_Vec = Vector3.zero;

	void FixUpdate_Unbeatable ()
	{
		m_Unbeatable_Vec.x = m_InitMoveSpeed.x * m_Unbeatable_SpeedRate;
		this.transform.localPosition += m_Unbeatable_Vec * Time.deltaTime;

//		Vector3 velocityChange = m_Unbeatable_Vec - m_Rigidbody.velocity;
//		m_Rigidbody.AddForce (velocityChange, ForceMode.VelocityChange);
	}

	void UnbeatableOnBrick (Brick brick)
	{
		if (brick != null) {

			Block parent = (Block)brick.M_Parent;
			Brick b;
			Rigidbody rg;
			for (int i=0; i<parent.M_Bricks.Count; i++) {
				b = parent.M_Bricks [i];
				rg = b.GetComponent<Rigidbody> ();
				if (rg == null) {
					rg = b.gameObject.AddComponent<Rigidbody> ();
				}
			}

			rg = brick.GetComponent<Rigidbody> ();
			if (rg == null) {
				rg = brick.gameObject.AddComponent<Rigidbody> ();
			}
			//						rg.AddExplosionForce (50, brick.transform.position, 1f);
			Vector3 dir = brick.transform.position - this.transform.position;
			rg.AddForce (dir * 10, ForceMode.VelocityChange);
			this.m_Rigidbody.velocity = m_OldVelocity;
			m_JumpTimes = 0;
			m_JumpValidateTime = 0;
			brick.OnUnbeatableCollided ();
		}
	}

	void OpenKinematicAndTrigger ()
	{
		m_Rigidbody.isKinematic = true;
		m_sphereCollider.isTrigger = true;
	}

	void CloseKinematicAndTrigger ()
	{
		m_Rigidbody.isKinematic = false;
		m_sphereCollider.isTrigger = false;
	}

	#endregion


	#region coin
	void CheckOnCoinBrick (Brick brick)
	{
		if (brick.M_IsCoin) {
			// get coin
			brick.M_IsCoin = false;
		}

	}

	public float m_MagnetRange =1f;// 0.6f;

	void CheckMagnetRange ()
	{

	}

	#endregion

	#region magnet
	MTimer m_AttachMagnetTimer;
	float magnetDuration=5f;

	void InitMagnetTimer(){
		m_AttachMagnetTimer=new MTimer(magnetDuration);
		m_AttachMagnetTimer.Init();
		m_AttachMagnetTimer.OnTime+=OnAttachMagnetOver;
	}

	public void AttachMagnet(){
		m_AttachMagnetTimer.Restart(false);
		PlayGameInstance.INSTANCE.PSC.PC.m_MagnetRange=5f;
		magnetEffect.SetActive(true);
	}

	void OnAttachMagnetOver(){
		m_AttachMagnetTimer.Pause();
		PlayGameInstance.INSTANCE.PSC.PC.m_MagnetRange=m_InitMagnetRange;
		magnetEffect.SetActive(false);
	}


	#endregion
	
}
