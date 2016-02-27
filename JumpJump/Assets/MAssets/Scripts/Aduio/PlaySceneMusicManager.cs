using UnityEngine;
using System.Collections;

public class PlaySceneMusicManager : MonoBehaviour
{
	public AudioSource audioSource;
	public AudioClip playMusic1;
	public AudioClip playMusic2;

	// Use this for initialization
	void Start ()
	{
		InitMusicTimer ();
		audioSource.clip = playMusic1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (musicTimer != null)
			musicTimer.Update ();

	}

	MTime musicTimer;

	void InitMusicTimer ()
	{
		if (musicTimer == null) {
			musicTimer = new MTime (30f, true);
			musicTimer.OnTime += OnChangeMusic;
			musicTimer.Init ();
		}

	}

	int count = 0;

	void OnChangeMusic ()
	{
		audioSource.clip = (count % 2 == 0) ? playMusic1 : playMusic2;
		audioSource.Play ();
		count++;
	}

	public void PlayMusic ()
	{
		if (!GameData.Instance ().M_SettingData.m_MusicOn)
			return;
		musicTimer.Restart (false);
		audioSource.Play ();
	}

	public void PauseMusic ()
	{
		musicTimer.Pause ();
		audioSource.Pause ();
	}

	public void ResumeMusic ()
	{
		if (!GameData.Instance ().M_SettingData.m_MusicOn)
			return;
		musicTimer.Resume ();
		audioSource.Play ();
	}
}
