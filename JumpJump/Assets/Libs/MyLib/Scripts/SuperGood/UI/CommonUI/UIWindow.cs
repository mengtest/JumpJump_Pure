using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum UIAnimation
{
	NOAnimation,
	Scale,
	Left,
	Custom,
	Right,
	Top,
	Bottom,
	Animator
}

public enum UIDisplayState
{
	Disable =0,
	Showining=1,
	Normal=2,
	Showouting=3
}

public class UIWindow : MonoBehaviour
{
	Animator animator;
	public bool DestoryOnQuit = false;
	public UIAnimation UIAnimationIn = UIAnimation.NOAnimation;
	public ActionCurve AniDurationIn = ActionCurve.Linear;
	public float AppearTime = 0.5f;
	public UIAnimation UIAnimationOut = UIAnimation.NOAnimation;
	public ActionCurve AniDurationOut = ActionCurve.Linear;
	public float DisappearTime = 0.5f;
	public bool AutoQuit = false;
	public float DisplayTime = 2;
	public UIDisplayState MUIDisplayState;

	void Awake ()
	{
		Init ();
	}

	void Start ()
	{
		ShowIn ();
	}

	public virtual void Init ()
	{
		animator = gameObject.GetComponent<Animator> ();

		MUIDisplayState = UIDisplayState.Disable;
		initPosition = this.transform.localPosition;
		OutAction = new ActionCallBack ();
		InAction = new ActionCallBack ();
		InAction.SetStartMethod (() => {
			MUIDisplayState = UIDisplayState.Showining;});
		InAction.SetCompleteMethod (() => {
			MUIDisplayState = UIDisplayState.Normal;});

		OutAction.SetStartMethod (() => {
			MUIDisplayState = UIDisplayState.Showouting;});
		OutAction.SetCompleteMethod (() => {
			MUIDisplayState = UIDisplayState.Disable;});

		InAction.AddCompleteMethod (TickUntilQuit);

	}

	public float GetTotalDisplayTime ()
	{
		return AppearTime + DisappearTime + DisplayTime;
	}

	private void TickUntilQuit ()
	{
		if (AutoQuit) {
			DelayAction skyDelayAnimation = ActionAnimator.delayTo (DisplayTime, ShowOut);
			skyDelayAnimation.Play ();
		}
	}

	public virtual void ShowIn ()
	{
		if (MUIDisplayState == UIDisplayState.Disable) {
			gameObject.SetActive (true);
			this.transform.localPosition = initPosition;
			showInAnimation ();
		}
	}

	private void showInAnimation ()
	{
		switch (UIAnimationIn) {
		case UIAnimation.Scale:
			scaleIn ();
			break;
		case UIAnimation.Bottom:
			bottomIn ();
			break;
		case UIAnimation.Top:
			topIn ();
			break;
		case UIAnimation.Left:
			leftIn ();
			break;
		case UIAnimation.Right:
			rightIn ();
			break;
		case UIAnimation.Custom:
			customIn ();
			break;
		case UIAnimation.Animator:
			animatorIn ();
			break;
		default:
			noAnimationIn ();
			break;
		}
	}

	private void noAnimationIn ()
	{
		if (InAction.OnStartMethod != null) {
			InAction.OnStartMethod ();
		}

		transform.localScale = Vector3.one;

		if (InAction.OnCompleteMethod != null) {
			InAction.OnCompleteMethod ();
		}
	}

	private void scaleIn ()
	{
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.localScale = Vector3.one;
		ActionAnimator.scaleFrom (gameObject, AppearTime, Vector3.zero, AniDurationIn, InAction);
	}

	private void bottomIn ()
	{
		RectTransform rectTransform = transform as RectTransform;
		transform.localScale = Vector3.one;
		ActionAnimator.moveFrom (gameObject, AppearTime, new Vector3 (initPosition.x, -Screen.height, 0), true, AniDurationIn, InAction);
	}

	private void topIn ()
	{
		RectTransform rectTransform = transform as RectTransform;
		transform.localScale = Vector3.one;
		ActionAnimator.moveFrom (gameObject, AppearTime, new Vector3 (initPosition.x, Screen.height, 0), true, AniDurationIn, InAction);
	}

	private void leftIn ()
	{
		RectTransform rectTransform = transform as RectTransform;
		transform.localScale = Vector3.one;
		ActionAnimator.moveFrom (gameObject, AppearTime, new Vector3 (-Screen.width, initPosition.y, 0), true, AniDurationIn, InAction);
	}

	private void rightIn ()
	{
		RectTransform rectTransform = transform as RectTransform;
		transform.localScale = Vector3.one;
		ActionAnimator.moveFrom (gameObject, AppearTime, new Vector3 (Screen.width, initPosition.y, 0), true, AniDurationIn, InAction);
	}

	private  void animatorIn ()
	{
		InAction.OnStartMethod ();
		if (animator != null) {
			animator.speed = 1f / AppearTime;
			animator.SetInteger ("state", (int)UIDisplayState.Showining);
			StartCoroutine (Open ());
		}
	}

	IEnumerator Open ()
	{
		yield return new WaitForSeconds (AppearTime);
		InAction.OnCompleteMethod ();
		animator.SetInteger ("state", (int)UIDisplayState.Normal);
	}

	protected virtual void customIn ()
	{
	}

	public virtual void ShowOut ()
	{
		if (MUIDisplayState == UIDisplayState.Normal) {
			setQuitAction ();
			shwoOutAnimation ();
		}
	}

	private void setQuitAction ()
	{
		if (DestoryOnQuit) {
			OutAction.AddCompleteMethod (() => {
				Destroy (gameObject);
			}
			);
		} else {
			OutAction.AddCompleteMethod (() => {
				gameObject.SetActive (false);
			}
			);
		}
	}

	private void shwoOutAnimation ()
	{
		switch (UIAnimationOut) {
		case UIAnimation.Scale:
			scaleOut ();
			break;
		case UIAnimation.Bottom:
			bottomOut ();
			break;
		case UIAnimation.Top:
			topOut ();
			break;
		case UIAnimation.Left:
			leftOut ();
			break;
		case UIAnimation.Right:
			rightOut ();
			break;
		case UIAnimation.Custom:
			customOut ();
			break;
		case UIAnimation.Animator:
			animatorOut ();
			break;
		default:
			noAnimationOut ();
			break;
		}

	}

	private void noAnimationOut ()
	{
		if (OutAction.OnStartMethod != null) {
			OutAction.OnStartMethod ();
		}
		if (OutAction.OnCompleteMethod != null) {
			OutAction.OnCompleteMethod ();
		}
	}

	private void scaleOut ()
	{
		ActionAnimator.scaleTo (gameObject, DisappearTime, Vector3.zero, AniDurationOut, OutAction);
	}

	private void bottomOut ()
	{
		ActionAnimator.moveTo (gameObject, DisappearTime, new Vector3 (initPosition.x, -Screen.height, 0), true, AniDurationOut, OutAction);
	}

	private void topOut ()
	{
		ActionAnimator.moveTo (gameObject, DisappearTime, new Vector3 (initPosition.x, Screen.height, 0), true, AniDurationOut, OutAction);
	}

	private void leftOut ()
	{
		ActionAnimator.moveTo (gameObject, DisappearTime, new Vector3 (-Screen.width, initPosition.y, 0), true, AniDurationOut, OutAction);
	}
	
	private void rightOut ()
	{
		ActionAnimator.moveTo (gameObject, DisappearTime, new Vector3 (Screen.width, initPosition.y, 0), true, AniDurationOut, OutAction);
	}

	protected virtual void customOut ()
	{
	}

	private void animatorOut ()
	{   
		OutAction.OnStartMethod ();
		if (animator != null) {
			animator.speed = 1f / DisappearTime;
			animator.SetInteger ("state", (int)UIDisplayState.Showouting);
			StartCoroutine (Closed ());
		}
	}
	


	IEnumerator Closed ()
	{
		yield return new WaitForSeconds (DisappearTime);
		animator.SetInteger ("state", (int)UIDisplayState.Disable);
		OutAction.OnCompleteMethod ();
	}

	public ActionCallBack InAction = null;
	public ActionCallBack OutAction = null;
	private Vector3 initPosition;
}

