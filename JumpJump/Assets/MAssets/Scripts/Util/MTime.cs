using UnityEngine;
using System.Collections;

public class MTime
{
		public delegate void OnTimeDelegate ();

		public event OnTimeDelegate OnTime;
	
		float timeSpan = 1f;
		bool isStart = false;
		float elapseTime = 0;
		bool active = false;
		bool ignoreTimeScale = false;

		public bool Active {
				get { return active;}
		}
	
		public void Init ()
		{
				isStart = false;
				elapseTime = 0f;
				active = false;
		}
	
		public MTime (float timeSpan)
		{
				this.timeSpan = timeSpan;
				this.ignoreTimeScale = false;
		}

		public MTime (float timeSpan, bool ingroTiemScale)
		{
				this.timeSpan = timeSpan;
				this.ignoreTimeScale = ingroTiemScale;
		}
	
		public void Update ()
		{
				if (!active)
						return;
				if (!isStart) {
						isStart = true;
						elapseTime = 0;
						if (OnTime != null)
								OnTime ();
				} else {
						elapseTime += ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime;
						if (elapseTime > timeSpan) {
								if (OnTime != null)
										OnTime ();
								elapseTime -= timeSpan;
						}
				}
		}
	
		public void Restart (bool onTime_OnStart)
		{
				active = true;
				isStart = !onTime_OnStart;
				elapseTime = 0;
		}
	
		public void Pause ()
		{
				active = false;
		}
	
		public void Resume ()
		{
				active = true;
		}

	
}
