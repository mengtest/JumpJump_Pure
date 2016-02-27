using System;
namespace MStateMachine{
	public struct Message
{

	public int sender;
	public int receiver;
	public MessageType msg;
	public double dispatchTime;
	public Object extraInfo;
}

public enum MessageType
{
	MSG_SLEEP,
	MSG_WORK,
	MSG_GOHOME,
	MSG_GOHOMEOK,
	MSG_GOOUT,
	MSG_GOOUTOK
}

}



