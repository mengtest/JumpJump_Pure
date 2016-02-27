using MStateMachine;

public class Property<T>
{
	protected	float startTime = 0;
	protected	float duration = 0;
	protected	float mValue = 0;
	protected	bool active = false;
	
	public Property ()
	{
	}
		
	public virtual void Execute (T  t)
	{
	}
	
	public virtual void Enter (T  t)
	{
		
	}
	
	public virtual void Exit (T  t)
	{
	}
	
	public virtual string ToString(T t){
		return "";
	}
	
	
	public void SetValue (float mValue)
	{
		this.mValue = mValue;
	}
	
	public float GetValue(){
		return this.mValue;
	}
	
	public void SetDuration(float duration){
		this.duration=duration;
	}
	
	public float GetDuration(){
		return duration;
	}
	
	public void SetValueAndDuration(float mValue,float duration){
		this.mValue=mValue;
		this.duration=duration;
	}
	
	public void Init(){
		 startTime = 0;
		 duration = 0;
		 mValue = 0;
		 active = false;
	}
	
}