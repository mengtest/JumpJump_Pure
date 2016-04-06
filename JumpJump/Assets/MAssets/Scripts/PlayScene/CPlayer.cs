using UnityEngine;
using System.Collections;
using MStateMachine;

public class CPlayer
{

	#region base
	public class AnimationNames
	{
		public	string idle = "idle";
		public	string run = "run";
		public  string jump_up = "jump_a";
		public  string jump_down = "jump_b";
		public  string jump_onGround = "jump_c";
	}

	public AnimationNames animationNames;

	#region statemachain
	StateMachine<CPlayer> baseStateMachine;
	public static Idle_State		 g_Idle_State = new Idle_State ();
	public static Run_State		     g_Run_state = new Run_State ();
	public static Jump_Up_State 		 g_Jump_Up_State = new Jump_Up_State ();
	public static Jump_Up2_State 		g_Jump_Up2_State = new Jump_Up2_State ();
	public static Jump_Down_State 		 g_Jump_Down_State = new Jump_Down_State ();
	public static Jump_OnGround_State		 g_Jump_OnGround_State = new Jump_OnGround_State ();

	public State<CPlayer>  CurState {
		get { return baseStateMachine.CurState;}
		set { baseStateMachine.CurState = value;}
	}
	
	public void ChangeBaseState (State<CPlayer> state)
	{
		baseStateMachine.ChangeState (state);
	}
	
	public bool IsState (State<CPlayer> state)
	{
		return CurState == state;
	}

	#endregion
	#endregion


	PlayerController m_PC;

	public CPlayer (PlayerController pC)
	{
		this.m_PC = pC;
		baseStateMachine = new StateMachine<CPlayer> ();
		Init ();
	}

	public void Init ()
	{
		baseStateMachine.Init (this, g_Idle_State, null, null);
		baseStateMachine.ChangeState(g_Idle_State);
		if(m_PC.playerAnimator!=null) m_PC.playerAnimator.PlayAnimtion();
	}

	public void Update ()
	{

		baseStateMachine.Update ();
	}



	#region idle
	
	public  void Idle_Execute ()
	{
//		if (!m_PC.M_OnGround) {
		if (m_PC.UnGround) {
			ChangeBaseState (g_Jump_Up_State);
			return;
		}
		if (m_PC.MoveSpeed_X > 0.1f) {
			ChangeBaseState (g_Run_state);
		}
	}

	public  void Idle_Enter ()
	{
	
		if(m_PC.playerAnimator!=null)
		m_PC.playerAnimator.PlayAnimation (PlayerAnimator.AnimatorState.IDLE);

		Debug.Log (" idle enter");
	}

	public  void Idle_Exit ()
	{

	}
#endregion

	#region run

	public  void Run_Execute ()
	{
//		if (!m_PC.M_OnGround) {
		if (m_PC.UnGround) {
			ChangeBaseState (g_Jump_Up_State);
			return;
		}
		if (m_PC.MoveSpeed_X < 0.01f) {
			ChangeBaseState (g_Idle_State);
		}
	}
	
	public  void Run_Enter ()
	{
		m_PC.playerAnimator.PlayAnimation (PlayerAnimator.AnimatorState.RUN);
		m_PC.OpenRunEffect();
		Debug.Log (" run enter");
	}
	
	public  void Run_Exit ()
	{
		m_PC.CloseRunEffect();
		
	}
	#endregion

	#region jump_Up

	public  void Jump_Up_Execute ()
	{
		if (m_PC.MoveSpeed_Y < 0) {
			ChangeBaseState (g_Jump_Down_State);
		}
//		Debug.Log(" y="+m_PC.MoveSpeed_Y);
	}
	
	public  void Jump_Up_Enter ()
	{
		m_PC.playerAnimator.PlayAnimation (PlayerAnimator.AnimatorState.JUMP_UP);
		Debug.Log (" jump up enter");
	}
	
	public  void Jump_Up_Exit ()
	{
		
	}

	#endregion


	#region jump_Up2

	public void OnJump_Up2 ()
	{
		ChangeBaseState (g_Jump_Up2_State);
	}

	public  void Jump_Up2_Execute ()
	{
		if (m_PC.MoveSpeed_Y < 0) {
			ChangeBaseState (g_Jump_Down_State);
		}
	}
	
	public  void Jump_Up2_Enter ()
	{
		m_PC.playerAnimator.PlayAnimation (PlayerAnimator.AnimatorState.JUMP_UP2);
		Debug.Log (" jump up2 enter");
	}
	
	public  void Jump_Up2_Exit ()
	{
		
	}
	
	#endregion




	#region jump_Down
	
	public  void Jump_Down_Execute ()
	{
		if (m_PC.M_OnGround) {
			ChangeBaseState (g_Jump_OnGround_State);
		}
	}
	
	public  void Jump_Down_Enter ()
	{
		m_PC.playerAnimator.PlayAnimation (PlayerAnimator.AnimatorState.JUMP_DOWN);
		Debug.Log (" jump down enter");
	}
	
	public  void Jump_Down_Exit ()
	{
		
	}
	
	#endregion

	#region jump_OnGround
	
	public  void Jump_OnGround_Execute ()
	{
		ChangeBaseState (g_Idle_State);
	}
	
	public  void Jump_OnGround_Enter ()
	{
		m_PC.playerAnimator.PlayAnimation (PlayerAnimator.AnimatorState.JUMP_ONGROUND);
		m_PC.OpenRunDownEffect();
		Debug.Log (" jump on ground enter");
	}
	
	public  void Jump_OnGround_Exit ()
	{
		
	}
	
	#endregion

}