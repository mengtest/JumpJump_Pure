using UnityEngine;
using System.Collections;

public class ActionSequence : BaseActionSequence
{
	

	public override void AppendAction (IAction skyAction)
	{
		AnimationSequence.Add (skyAction);
		setAction (skyAction);
	}

	public override  void AddHead (IAction skyAction)
	{
		AnimationSequence.Insert (0, skyAction);
		setAction (skyAction);
	}
	
	private void setAction (IAction skyAction)
	{
		skyAction.ParentAction = null;
		PlayTime += skyAction.PlayTime;
		if (ParentAction != null)
			this.ParentAction.ReComputePlaytime ();
	}

	public override void RemoveAction (IAction skyAction)
	{
		AnimationSequence.Remove (skyAction);
		skyAction.ParentAction = null;
		PlayTime -= skyAction.PlayTime;
		if (ParentAction != null)
			this.ParentAction.ReComputePlaytime ();
	}
	
	public override void PlayLoop ()
	{
		base.PlayLoop ();
		if (AnimationSequence.Count > 0) {
			AnimationSequence [0].Play ();
		} else {
			PlayCallBack.OnCompleteMethod ();
		}
	}
	
	public  override void PlayNext (IAction skyAction)
	{
		if (AnimationSequence.Contains (skyAction)) {
			int index = AnimationSequence.IndexOf (skyAction);
			if (index < AnimationSequence.Count - 1) {
				AnimationSequence [index + 1].Play ();
			} else {
				PlayCallBack.OnCompleteMethod ();
			}
		}
	}

	public override void ReComputePlaytime ()
	{
		PlayTime = 0;
		foreach (IAction skyAction in AnimationSequence) {
			PlayTime += skyAction.PlayTime;
		}
	}
}
