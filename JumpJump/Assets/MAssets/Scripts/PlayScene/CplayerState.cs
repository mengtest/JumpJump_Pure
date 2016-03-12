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

#region Jump
public class Jump_State :State<CPlayer>
{
	public override void Execute (CPlayer player)
	{
		player.Jump_Execute ();
	}
	
	public override void Enter (CPlayer player)
	{
		player.Jump_Enter ();
	}
	
	public override void Exit (CPlayer player)
	{
		player.Jump_Exit ();
	}
	
	public override string ToString ()
	{
		return string.Format ("[Jump_State]");
	}
}

#endregion


