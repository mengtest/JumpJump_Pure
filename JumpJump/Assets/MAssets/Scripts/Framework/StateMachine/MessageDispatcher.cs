using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MStateMachine
{
	public class MessageDispatcher
	{
		Queue<Message> priorityQ;
	
		public MessageDispatcher ()
		{
			priorityQ = new Queue<Message> ();
		}
	
		public void Discharge (BaseGameEntity receiver, Message message)
		{	
			receiver.handleMessage (message);
		}
	
		public void DispatchMessage (double delay, int sender, int receiver, MessageType msg, System.Object extraInfo)
		{
			Message message = new Message ();
			message.sender = sender;
			message.receiver = receiver;
			message.msg = msg;
			message.dispatchTime = delay;
			message.extraInfo = extraInfo;
			DispatchMessage (delay, message);
		}

		public void DispatchMessage (double delay, Message message)
		{
			if (BaseWorld.entityManager == null)
				return;
			BaseGameEntity receiverEntity = BaseWorld.entityManager.getEntityFromId (message.receiver);
		
			if (delay <= 0) {
				Discharge (receiverEntity, message);
			} else {
				message.dispatchTime = UnityEngine.Time.time + delay;
				priorityQ.Enqueue (message);
			
			}
		
		}
	
		public void DispatchDelayMessage ()
		{
			double curTime = UnityEngine.Time.time;
			if (priorityQ.Count == 0)
				return;
			while (priorityQ.Count>0 && priorityQ.Peek().dispatchTime<curTime && 
		priorityQ.Peek().dispatchTime>0) {
				Message message = priorityQ.Dequeue ();
				if (BaseWorld.entityManager != null) {
					BaseGameEntity receiverEntity = BaseWorld.entityManager.getEntityFromId (message.receiver);
					Discharge (receiverEntity, message);
				}
		
			}
		}
	
		public virtual void  Update ()
		{
			DispatchDelayMessage ();
		}
	
		public void removeMessage (Message message)
		{

		
		}
		
		public void Clear(){
			priorityQ.Clear();
		}
	}
	
}

