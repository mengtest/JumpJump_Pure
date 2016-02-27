using UnityEngine;
using System.Collections;

public class MenuSceneMusicManager : MonoBehaviour
{

		public AudioSource source;

		public void PlayMusic ()
		{
		if (!GameData.Instance ().M_SettingData.m_MusicOn)
						return;
				source.Play ();
		}

		public void PauseMusic ()
		{
				source.Pause ();
		}

		public void ResumeMusic ()
		{

				source.Play ();
		}
}
