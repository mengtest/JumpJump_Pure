
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Object3d :MonoBehaviour
{
//	public const float MOVE_DELAY_TIME_Cell = 0.5F;

	#region direction
	protected int m_Dirction;
	public const int DIRICTION_LEFT = 1;
	public const int DIRICTION_RIGHT = 1 << 2;
	public const int DIRICTION_UP = 1 << 3;
	public const int DIRICTION_DOWN = 1 << 4;

	public static int GetDiriction (Vector3 startPot, Vector3 endPot)
	{
		int direction = 0;
		direction |= (startPot.x < endPot.x) ? DIRICTION_RIGHT : 0;
		direction |= (startPot.x > endPot.x) ? DIRICTION_LEFT : 0;
		direction |= (startPot.y < endPot.y) ? DIRICTION_UP : 0;
		direction |= (startPot.y > endPot.y) ? DIRICTION_DOWN : 0;
		
		return direction;
		
	}
	#endregion

	#region menber
	[SerializeField]
	private Object3d
		m_Parent;
	
	public Object3d M_Parent {
		get {
			return m_Parent;
		}
		set {
			m_Parent = value;
			UpdateWorldPot ();
		}
	}

	[SerializeField]
	Vector3
		m_StartPot;

	public Vector3 M_StartPot {
		get {
			return m_StartPot;
		}
		set {
			m_StartPot = value;
		}
	}

	[SerializeField]
	Vector3
		m_EndPot;

	public Vector3 M_EndPot {
		get {
			return m_EndPot;
		}
		set {
			m_EndPot = value;
		}
	}

	public Vector3 M_CurPot {
		get {
			return transform.position;
		}
		set {
			transform.position = value;
		}
	}

	[SerializeField]
	Vector3
		m_Loc_StartPot;

	public Vector3 M_Loc_StartPot {
		get {
			return m_Loc_StartPot;
		}
		set {
			m_Loc_StartPot = value;
			m_Loc_EndPot = m_Loc_StartPot + m_MoveSpan;
		}
	}

	[SerializeField]
	Vector3
		m_Loc_EndPot;

	public Vector3 M_Loc_EndPot {
		get {
			return m_Loc_EndPot;
		}
		set {
			m_Loc_EndPot = value;
		}
	}

	public Vector3 M_Loc_CurPot {
		get {
			return transform.localPosition;
		}
		set {
			transform.localPosition = value;
		}
	}

	[SerializeField]
	Vector3
		m_MoveSpan = Vector3.zero;

	public Vector3 M_MoveSpan {
		get {
			return m_MoveSpan;
		}
		set {
			m_MoveSpan = value;
			m_Loc_EndPot = m_Loc_StartPot + m_MoveSpan;
		}
	}

	[SerializeField]
	int
		m_MoveDelay = 0;

	public int M_MoveDelay {
		get {
			return m_MoveDelay;
		}
		set {
			m_MoveDelay = value;
		}
	}

	public float GetMoveDelayTime ()
	{
		return m_MoveDelay * PlayGameInstance.INSTANCE.PSC.moveDelayTime_Unit;
	}

	[SerializeField]
	float
		m_MoveDuration = 0f;

	public float M_MoveDuration {
		get {
			return m_MoveDuration;
		}
		set {
			m_MoveDuration = value;
		}
	}

	public float GetMoveDurationTime ()
	{
		return m_MoveDuration * PlayGameInstance.INSTANCE.PSC.moveDurationTime_Unit;
	}

	float m_Time = 0;

	public float M_Time {
		get {
			return m_Time;
		}
		set {
			m_Time = value;
		}
	}

	bool m_ActiveMove = false;
	
	public bool M_ActiveMove {
		get {
			return m_ActiveMove;
		}
		set {
			m_ActiveMove = value;
		}
	}
	
	bool m_Active = false;
	
	public bool M_Active {
		get {
			return m_Active;
		}
		set {
			m_Active = value;
		}
	}
	#endregion

	#region tweener

	public const bool USETWEENER = true;
	Tweener tweener;

	public Tweener DoPosition (Vector3 endValue, float duration, float delay=0)
	{
		return DOTween.To (() => this.M_Loc_CurPot, delegate (Vector3 x) {
			this.M_Loc_CurPot = x;
		}, endValue, duration).SetDelay (delay).SetTarget (this);
	}

	public bool IsMoveOver ()
	{

		return  (m_Parent != null ? m_Parent.IsMoveOver () : true) && Vector3.SqrMagnitude (M_Loc_CurPot - M_Loc_EndPot) < 0.01f * 0.01f;
	}
	#endregion
	
	public Object3d ()
	{

	}
	
	public Object3d (Object3d parent, Vector3 locStartPot, Vector3 locEndPot, int moveDelay, float moveDuration=1f)
	{
		this.m_Parent = parent;
		this.m_Loc_StartPot = locStartPot;
		this.m_Loc_EndPot = locEndPot;
		this.M_Loc_CurPot = locStartPot;
		this.m_MoveDelay = moveDelay;
		this.m_MoveDuration = moveDuration;
		this.m_Dirction = GetDiriction (locStartPot, locEndPot);
	}

	public virtual void UpdateWorldPot ()
	{
		if (m_Parent != null)
			UpdateWorldPot (m_Parent);
		else {
			m_StartPot = m_Loc_StartPot;
			m_EndPot = m_Loc_EndPot;
		}
	
	}

	void UpdateWorldPot (Object3d parent)
	{
		if (parent != null) {
			m_StartPot = m_Parent.M_StartPot + m_Loc_StartPot;
			m_EndPot = m_Parent.M_EndPot + m_Loc_EndPot;
			UpdateWorldPot (parent.m_Parent);
		}
	}








	#region editor
	public  bool Update_Editor ()
	{
		bool change=ListenerParentChange ();
		Vector3 pot = transform.localPosition;
		M_Loc_StartPot = pot;
		UpdateWorldPot ();
		return change;
	}

	[SerializeField]
	 Transform oldParent;

	bool ListenerParentChange ()
	{
//		if(this.name=="Blocks_Head_1"){
//			Debug.Log(" mparent="+ ((m_Parent==null) ? "null ":m_Parent.name));
//		}
		if (oldParent != transform.parent) { 
			// remove old parent refrence
			if (m_Parent != null && m_Parent is Block) {
//				Debug.Log (" remove old parent= " + m_Parent.name);

				Block parentBlock = (Block)m_Parent;

				if (this is Brick) {
					parentBlock.RemoveBrick ((Brick)this);
				} else if (this is Block) {
					parentBlock.RemoveBlock ((Block)this);
				}
			}
		

			// add to new parent

			Block newParentBlock = null;
			if (transform.parent != null) 
				newParentBlock = transform.parent.GetComponent<Block> ();

			if (newParentBlock != null) {
				m_Parent = newParentBlock;
				if (this is Brick) {
					newParentBlock.M_Bricks.Add ((Brick)this);
				} else if (this is Block) {
					newParentBlock.M_Blocks.Add ((Block)this);
				}
			} else {
				m_Parent = null;
			}
			return true;
		}
//		Debug.Log (" name=" + newParentBlock.m_Time);

		oldParent = transform.parent;
		return false;
	}

	public virtual void Delete ()
	{

	}
	

	#endregion
	

	#region condition
	[SerializeField]
	MoveActiveConditionType
		m_MoveActiveConditionType = MoveActiveConditionType.PARENT_MOVE_OVER;

	void Init_MoveActiveCondition ()
	{
		switch (m_MoveActiveConditionType) {
		case MoveActiveConditionType.EMPTY:
			MoveActiveCondition = null;
			break;
		case MoveActiveConditionType.PARENT_MOVE_OVER:
			if (m_Parent != null) 
				MoveActiveCondition = m_Parent.IsMoveOver;
			else
				MoveActiveCondition = null;
			break;

		}
	}

	public MoveActiveConditionType M_MoveActiveConditionType {
		get { return m_MoveActiveConditionType;}
		set { m_MoveActiveConditionType = value;}
	}

	public delegate bool delegate_MoveActiveCondition ();
	
	public delegate_MoveActiveCondition
		MoveActiveCondition;
	
	public void CheckMoveActive ()
	{
		if ((MoveActiveCondition == null || MoveActiveCondition ()) && CheckDelayTime ()) {
			if (!m_ActiveMove) {
				ActiveMove ();
				m_ActiveMove = true;
			}
		} 
		CheckMoveActive_Child ();
	}

	public virtual void CheckMoveActive_Child ()
	{

	}

	public virtual void ActiveMove ()
	{

		tweener = DoPosition (M_Loc_EndPot, M_MoveDuration);
		tweener.Restart ();
	}

	public bool CheckDelayTime ()
	{
		return (m_Time += Time.deltaTime) >= GetMoveDelayTime ();

	}

	[SerializeField]
	ActiveConditionType
		m_ActiveConditionType = ActiveConditionType.EMPTY;
	[SerializeField]
	int
		m_ACT_ArriveTime = 1;

	public int M_ACT_ArrvieTime {
		get { return m_ACT_ArriveTime;}
		set { m_ACT_ArriveTime = value;}
	}

	public float Get_ACT_ArriveTime ()
	{
		return PlayGameInstance.INSTANCE.PSC.ConditionTime_Unit * m_ACT_ArriveTime;
	}

	
	public delegate bool delegate_ActiveCondition (Object3d o3d);
	
	public delegate_ActiveCondition ActiveCondition;
	
	public void CheckActive ()
	{

		if (ActiveCondition == null || ActiveCondition (this)) {
			if (!m_Active) {
				Active ();
				m_Active = true;
			}
		} 
		CheckActive_Child ();
	}

	public virtual void CheckActive_Child ()
	{

	}

	public virtual void Active ()
	{

	}

	public virtual void  SetActiveCondition (delegate_ActiveCondition  condition)
	{
		if (m_ActiveConditionType != ActiveConditionType.EMPTY)
			this.ActiveCondition = condition;
	}

	void Init_ActiveCondition ()
	{
		switch (m_ActiveConditionType) {
		case ActiveConditionType.EMPTY:
			break;
		case ActiveConditionType.TARGET_ARRIVE_TIME:
			ActiveCondition = PlayGameInstance.INSTANCE.PSC.PC.TriggerBlockActive_ArriveTime;
			break;
		}
	}

	public delegate_ActiveCondition MoveIn_Condition;
	[SerializeField]
	MoveIn_ConditionType
		m_MoveIn_CT = MoveIn_ConditionType.EMPTY;

	public MoveIn_ConditionType M_MoveIn_CT {
		get {
			return m_MoveIn_CT;
		}
		set {
			m_MoveIn_CT = value;
		}
	}

	[SerializeField]
	int
		m_MoveIn_ArriveTime = 1;

	public int M_MoveIn_ArriveTime {
		get {
			return m_MoveIn_ArriveTime;
		}
		set {
			m_MoveIn_ArriveTime = value;
		}
	}

	public float Get_MoveIn_ArriveTime(){
		return PlayGameInstance.INSTANCE.PSC.ConditionTime_Unit * m_MoveIn_ArriveTime;
	}

	[SerializeField]
	int
		m_MoveIn_Duration = 1;// all the bricks of block time

	public int M_MoveIn_Duration {
		get {
			return m_MoveIn_Duration;
		}
		set {
			m_MoveIn_Duration = value;
		}
	}

	public float Get_MoveIn_Duration ()
	{
		return m_MoveIn_Duration * PlayGameInstance.INSTANCE.PSC.moveDurationTime_Unit;
	}

	float m_MoveIn_Duration_Time = 0f;

	public float M_MoveIn_Duration_Time {
		get {
			return m_MoveIn_Duration_Time;
		}
		set {
			m_MoveIn_Duration_Time = value;
		}
	}

	[SerializeField]
	Vector3
		m_MoveIn_Span = Vector3.zero;

	public Vector3 M_MoveIn_Span {
		get {
			return m_MoveIn_Span;
		}
		set {
			m_MoveIn_Span = value;
		}
	}

	void Init_MoveIn_Condition ()
	{
		switch (m_MoveIn_CT) {
		case MoveIn_ConditionType.EMPTY:
			break;
		case MoveIn_ConditionType.TARGET_ARRIVE_TIME:
			MoveIn_Condition = PlayGameInstance.INSTANCE.PSC.PC.TriggerBlockMoveIn_ArriveTime;
			break;
		}
	}

	public delegate_ActiveCondition MoveOut_Condition;

	public void Init_Condition ()
	{
		Init_MoveActiveCondition ();
		Init_ActiveCondition ();
		Init_MoveIn_Condition ();
	}

	public virtual bool IsMoveInOver ()
	{
		return Vector3.SqrMagnitude (M_Loc_CurPot - M_Loc_StartPot) < 0.01f * 0.01f;
	}

	public bool HasMoveInCondition ()
	{
		return m_MoveIn_CT != MoveIn_ConditionType.EMPTY;
	}

	float m_MoveIn_DelayTime = 0f;

	public float M_MoveIn_DelayTime {
		get {
			return m_MoveIn_DelayTime;
		}
		set {
			m_MoveIn_DelayTime = value;
		}
	}

	[SerializeField]
	int m_MoveIn_Delay=1;

	public int M_MoveIn_Delay {
		get {
			return m_MoveIn_Delay;
		}
		set {
			m_MoveIn_Delay = value;
		}
	}
	public float Get_MoveIn_DelayTime(){
		return PlayGameInstance.INSTANCE.PSC.moveDelayTime_Unit*m_MoveIn_Delay;
	}

	protected	bool m_MoveIn = false;
	protected   bool m_MoveIn_Overed = false;

	public virtual void StartMoveIn ()
	{
//		DoPosition (m_Loc_StartPot, m_MoveIn_Duration_Time, m_MoveIn_DelayTime+Get_MoveIn_DelayTime()).Restart ();
//		Debug.Log("deyla="+(m_MoveIn_DelayTime+Get_MoveIn_DelayTime())+"  dd="+m_MoveIn_Delay);
		DoPosition (m_Loc_StartPot, m_MoveIn_Duration_Time, m_MoveIn_DelayTime+m_Parent. Get_MoveIn_DelayTime()).Restart ();
	}


	#endregion


	void Awake ()
	{
	}

	void Start ()
	{
		Init_Condition ();
	}

	public void Reset ()
	{
		if (tweener != null)
			tweener.Pause ();

		m_Time = 0;
		m_Active = false;
		m_ActiveMove = false;
		M_Loc_CurPot = M_Loc_StartPot;
		UpdateWorldPot ();

		m_MoveIn=false;
		m_MoveIn_Overed=false;

	
	}



}

public enum MoveActiveConditionType
{
	EMPTY=0,
	PARENT_MOVE_OVER=1,
}

public enum ActiveConditionType
{
	EMPTY=0,
	TARGET_ARRIVE_TIME=1,
}

public enum MoveIn_ConditionType
{
	EMPTY=0,
	TARGET_ARRIVE_TIME=1,
}

public enum MoveOut_ConditionType
{
	EMPTY=0,
	TARGET_LEAVE_TIME=1,
}






