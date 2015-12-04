using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SkyDelayAnimation : SkyBaseAnimationNormal {
	
	public SkyDelayAnimation(){
		this.Init ();
	}

	public SkyDelayAnimation(float delayTime,System.Action startAction,System.Action callback){
		this.Init ();
		this.PlayTime = delayTime;
		this.PlayCallBack.AddCompleteMethod (callback);
		this.PlayCallBack.AddStartMethod (startAction);
	}

	public override void PlayLoop ()
	{
		base.PlayLoop ();
		DelayTimeAction (PlayTime,PlayCallBack);
	}

}
