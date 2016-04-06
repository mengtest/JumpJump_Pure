using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour
{

	public static readonly string PLAYER_STATE = "PlayerState";
	public static readonly string SAME_STATE = "SameState";
	private Animator animator;
	AnimatorState lastState;

	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator> ();
//		AnimatorClipInfo[] animatorClipInfos = animator.GetCurrentAnimatorClipInfo (0);
//		foreach (AnimatorClipInfo animatorClipInofo in animatorClipInfos) {
////			if(animatorClipInofo.clip.name.Equals("Run")){
//			AnimationEvent animationEvent = new AnimationEvent();
//			animationEvent.functionName = "SetSameTrue";
//			animationEvent.time = 0.1f;
//			animatorClipInofo.clip.AddEvent(animationEvent);
////			}
//		}
		lastState = AnimatorState.IDLE;

//		PlayAnimation (AnimatorState.Run);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

//	public void Reset(){
//		lastState = AnimatorState.Idle;
//		animator.SetFloat (PLAYER_STATE, (int)lastState);
//		animator.SetBool (SAME_STATE, true);
//	}

//	public void PlayAnimation (AnimatorState animatorState)
//	{
//
//		if (lastState != animatorState) {
//			animator.SetFloat (PLAYER_STATE, (int)animatorState);
//			animator.SetBool (SAME_STATE, false);
//			lastState = animatorState;
//			StartCoroutine (SetSameTrue ());
//		}
//	}

	public void PlayAnimation (AnimatorState animatorState)
	{
	
//			if (lastState != animatorState) {
		if (animator == null)
			return;
		animator.SetInteger ("state", (int)animatorState);
		lastState = animatorState;
//				StartCoroutine (SetSameTrue ());
		animator.CrossFade (GetAnimatorHashId(animatorState), 0.2f);
		Debug.Log(" play animtion ="+GetAnimatorHashId(animatorState));
//			}
	}

	public void StopAnimation(){
		animator.Stop();
	}

	public void PlayAnimtion(){
//		animator.StartPlayback();
	}
	
//
	public int GetAnimatorHashId (AnimatorState state)
	{

		switch (state) {
		case AnimatorState.IDLE:
			return Animator.StringToHash ("Base Layer.Idle");
		case AnimatorState.RUN:
			return Animator.StringToHash ("Base Layer.RuN");
		case AnimatorState.JUMP_UP:
			return Animator.StringToHash ("Base Layer.Jump_Up");
		case AnimatorState.JUMP_UP2:
			return Animator.StringToHash ("Base Layer.Jump_Up2");
		case AnimatorState.JUMP_DOWN:
			return Animator.StringToHash ("Base Layer.Jump_Down");
		case AnimatorState.JUMP_ONGROUND:
			return Animator.StringToHash ("Base Layer.Jump_OnGround");

			
		}
		return -1;

	}



	public enum AnimatorState
	{
		IDLE=0,
		RUN=1,
		JUMP_UP=2,
		JUMP_UP2=3,
		JUMP_DOWN=4,
		JUMP_ONGROUND=5
	}

	IEnumerator SetSameTrue ()
	{
		yield return new WaitForSeconds (0.1f);
		animator.SetBool (SAME_STATE, true);
	}

}
