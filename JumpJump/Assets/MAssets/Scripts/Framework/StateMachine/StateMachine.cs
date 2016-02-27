using System;

namespace MStateMachine
{
	public class StateMachine<Entity>
	{
		public StateMachine ()
		{
		}
	
		Entity owner;
		State<Entity> curState;
		public State<Entity> CurState{
			get {return curState;}
			set {curState=value;}
		}
		State<Entity> preState;
		public State<Entity> PreState{
			get {return preState;}
			set{ preState= value;}
		}
		
		State<Entity> globalState;
		public State<Entity> GlobalState{
			get {return globalState;}
			set{ globalState= value;}
		}
	
		public void Init (Entity entity, State<Entity> curState, State<Entity> preState, State<Entity> globelState)
		{
			this.owner = entity;
			this.curState = curState;
			this.preState = preState;
			this.globalState = globelState;
		}
	
		public void Update ()
		{
			if (globalState != null)
				globalState.Execute (owner);
			if (curState != null)
				curState.Execute (owner);
		}
	
		public void ChangeState (State<Entity> state)
		{
			preState = curState;
			if (curState != null)
				curState.Exit (owner);
			curState = state;
			if (curState != null)
				curState.Enter (owner);
		}
	
		public bool HandleMessage (Message message)
		{
			if (curState != null && curState.OnMessage (owner, message)) {
				return true;
			}
			if (globalState != null && globalState.OnMessage (owner, message)) {
				return true;
			}
			return false;
		}
	
		public void RevertToPreState ()
		{
			ChangeState (preState);
		}
	
	}
	
}

