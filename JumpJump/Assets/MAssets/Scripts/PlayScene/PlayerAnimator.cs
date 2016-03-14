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
		lastState = AnimatorState.Idle;

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

	public void PlayAnimation (AnimatorState animatorState)
	{

		if (lastState != animatorState) {
			animator.SetFloat (PLAYER_STATE, (int)animatorState);
			animator.SetBool (SAME_STATE, false);
			lastState = animatorState;
			StartCoroutine (SetSameTrue ());
		}
	}


	public enum AnimatorState
	{
		Idle=0,
		Run=1,
		Jump_up=2,
		Jump_down=3,
		Jump_onGround=4
	}

	IEnumerator SetSameTrue ()
	{
		yield return new WaitForSeconds (0.1f);
		animator.SetBool (SAME_STATE, true);
	}

}
