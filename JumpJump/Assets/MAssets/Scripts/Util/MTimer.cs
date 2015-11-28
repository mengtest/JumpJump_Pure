using UnityEngine;
using System.Collections;

public class MTimer
{
	public delegate void OnTimeDelegate ();

	public event OnTimeDelegate OnTime;
	
	float m_TimeSpan = 1f;
	bool m_IsStart = false;
	float m_ElapseTime = 0;
	bool m_Active = false;

	public bool M_Active {
		get { return m_Active;}
	}
	
	public void Init ()
	{
		m_IsStart = false;
		m_ElapseTime = 0f;
		m_Active = false;
	}
	
	public MTimer (float timeSpan)
	{
		this.m_TimeSpan = timeSpan;
		Init ();
	}
	
	public void Update ()
	{
		if (!m_Active)
			return;
		if (!m_IsStart) {
			m_IsStart = true;
			m_ElapseTime = 0;
			if (OnTime != null)
				OnTime ();
		} else {
			m_ElapseTime += Time.deltaTime;
			if (m_ElapseTime > m_TimeSpan) {
				if (OnTime != null)
					OnTime ();
				m_ElapseTime -= m_TimeSpan;
			}
		}
	}
	
	public void Restart (bool onTime_OnStart)
	{
		Restart(onTime_OnStart,m_TimeSpan);
	}

	public void Restart (bool onTime_OnStart, float timeSpan)
	{
		m_Active = true;
		m_IsStart = !onTime_OnStart;
		m_ElapseTime = 0;
		m_TimeSpan = timeSpan;
	}
	
	public void Pause ()
	{
		m_Active = false;
	}
	
	public void Resume ()
	{
		m_Active = true;
	}

	
}
