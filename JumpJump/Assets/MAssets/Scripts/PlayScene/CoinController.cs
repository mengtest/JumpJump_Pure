using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour,IPoolable
{

	public UpcastAnimation upCastAnimation;
	public TweenPosition tweenPosition;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void LateUpdate(){
		if(m_Animation) UpdateAniamtionTargetPot();
	}

	public void IDestory ()
	{

	}
	
	public void IReset ()
	{
		this.gameObject.SetActive(false);
		m_Animation=false;
	}

	bool m_Animation=false;

	public void StartAnimation(Vector3 worldPot){
		if(m_Animation) return;
		this.gameObject.SetActive(true);
		worldPot.z-=0.5f;
		this.gameObject.transform.position=worldPot;
		m_Animation=true;
		upCastAnimation.StartAnimation();
		upCastAnimation.OnFinished=StartAnimation_TweenPosition;

	}

	void StartAnimation_TweenPosition(){

		tweenPosition.from=transform.position;
		tweenPosition.to=PlayGameInstance.INSTANCE.PSC.PC.transform.position;
		tweenPosition.ResetToBeginning();
		tweenPosition.PlayForward();
		tweenPosition.SetOnFinished(EndAnimation);
	}

	void EndAnimation(){
		m_Animation=false;
		ResourceMgr.Instance().FreeCoinController(this);
		SoundManager.Instance().PlaySound2D(SoundManager.gold_ID,1f);
		PlayGameInstance.INSTANCE.AddCoin(1);
	}

	void UpdateAniamtionTargetPot(){
		tweenPosition.to=PlayGameInstance.INSTANCE.PSC.PC.transform.position;
	}



}
