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
	public static Jump_State 		 g_Jump_State = new Jump_State ();

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


	
	public  void Idle_Execute ()
	{
	
	}

	public  void Idle_Enter ()
	{
	
	}

	public  void Idle_Exit ()
	{

	}

	public  void Run_Execute ()
	{
		
	}
	
	public  void Run_Enter ()
	{
		
	}
	
	public  void Run_Exit ()
	{
		
	}

	public  void Jump_Execute ()
	{
		
	}
	
	public  void Jump_Enter ()
	{
		
	}
	
	public  void Jump_Exit ()
	{
		
	}


}