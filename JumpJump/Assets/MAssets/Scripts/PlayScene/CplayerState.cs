using UnityEngine;
using System.Collections;

#region Idle
public class Idle_State :State<CPlayer>
{
	public override void Execute (CPlayer player)
	{
		player.Idle_Execute ();
	}
	
	public override void Enter (CPlayer player)
	{
		player.Idle_Enter ();
	}
	
	public override void Exit (CPlayer player)
	{
		player.Idle_Exit ();
	}
	
	public override string ToString ()
	{
		return string.Format ("[Idle_State]");
	}
}
#endregion






#region Run
public class Run_State :State<CPlayer>
{
	public override void Execute (CPlayer player)
	{
		player.Run_Execute ();
	}
	
	public override void Enter (CPlayer player)
	{
		player.Run_Enter ();
	}
	
	public override void Exit (CPlayer player)
	{
		player.Run_Exit ();
	}
	
	public override string ToString ()
	{
		return string.Format ("[Run_State]");
	}
}
#endregion

#region Jump_Up
public class Jump_Up_State :State<CPlayer>
{
	public override void Execute (CPlayer player)
	{
		player.Jump_Up_Execute ();
	}
	
	public override void Enter (CPlayer player)
	{
		player.Jump_Up_Enter ();
	}
	
	public override void Exit (CPlayer player)
	{
		player.Jump_Up_Exit ();
	}
	
	public override string ToString ()
	{
		return string.Format ("[Jump_Up_State]");
	}
}

#endregion


#region Jump_Down
public class Jump_Down_State :State<CPlayer>
{
	public override void Execute (CPlayer player)
	{
		player.Jump_Down_Execute ();
	}
	
	public override void Enter (CPlayer player)
	{
		player.Jump_Down_Enter ();
	}
	
	public override void Exit (CPlayer player)
	{
		player.Jump_Down_Exit ();
	}
	
	public override string ToString ()
	{
		return string.Format ("[Jump_Down_State]");
	}
}

#endregion


#region jump_OnGround
public class Jump_OnGround_State :State<CPlayer>
{
	public override void Execute (CPlayer player)
	{
		player.Jump_OnGround_Execute ();
	}
	
	public override void Enter (CPlayer player)
	{
		player.Jump_OnGround_Enter ();
	}
	
	public override void Exit (CPlayer player)
	{
		player.Jump_OnGround_Exit ();
	}
	
	public override string ToString ()
	{
		return string.Format ("[Jump_OnGround_State]");
	}
}

#endregion



