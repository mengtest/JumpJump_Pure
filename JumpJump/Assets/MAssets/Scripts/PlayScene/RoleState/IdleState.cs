﻿using UnityEngine;
using System.Collections;

public class IdleState : StateMachineBehaviour
{

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Debug.Log (" **** idle state enter ****");
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	
//		PlayerController m_PC = PlayGameInstance.INSTANCE.PSC.PC;
//		if (!m_PC.M_OnGround) {
//			animator.SetInteger ("state", (int)PlayerAnimator.AnimatorState.JUMP_UP);
//			return;
//		}
//		if (m_PC.MoveSpeed_X > 0.1f) {
//			animator.SetInteger ("state", (int)PlayerAnimator.AnimatorState.RUN);
//		}
//		animator.GetInteger("state");


	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
