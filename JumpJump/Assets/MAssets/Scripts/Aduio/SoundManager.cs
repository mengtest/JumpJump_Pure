using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{

	static SoundManager  g_Instance;

	public static SoundManager  Instance ()
	{
		return g_Instance;
	}

	public AudioSource[] source2Ds;
	public AudioSource[] source3Ds;

	// ui
	public AudioClip btn;
	public AudioClip gold;
	public AudioClip failure;
	public AudioClip success;
	
	// scene
	public AudioClip die;



	 


	//id 2d

	public const int btn_ID = 0;
	public const int gold_ID = 1;
	public const int failure_ID = 2;
	public const int success_ID = 3;

	// id 3d
	public const int die_ID = 100;

	Dictionary<int,AudioClip>  map;
	List<PlaySoundData> waitPlaySoundData2Ds = new List<PlaySoundData> ();
	List<PlaySoundData> waitPlaySoundData3Ds = new List<PlaySoundData> ();
	public static int MAX_NUM = 3;

	AudioSource GetFreeSource3D ()
	{
		for (int i=0; i<source3Ds.Length; i++) {
			if (!source3Ds [i].isPlaying)
				return source3Ds [i];
		}
		return null;
	}

	AudioSource GetFreeSource2D ()
	{
		for (int i=0; i<source2Ds.Length; i++) {
			if (!source2Ds [i].isPlaying)
				return source2Ds [i];
		}
		return null;
	}

	void Awake ()
	{
		g_Instance = this;
		Init ();
		waitPlaySoundData2Ds.Clear ();
		waitPlaySoundData3Ds.Clear ();
	}

	void Init ()
	{
		map = new Dictionary<int, AudioClip> ();
		//2d

		map.Add (btn_ID, btn);
		map.Add (gold_ID, gold);
		map.Add (failure_ID, failure);
		map.Add (success_ID, success);

		//3d
		map.Add (die_ID, die);

	}

	public void PlaySound2D (int audioClipId, float volume)
	{
				
		PlaySound2D (audioClipId, volume, 1f);
	}

	public void PlaySound2D (int audioClipId, float volume, float pitch)
	{
		if (!GameData.Instance ().M_SettingData.m_SoundOn)
			return;
		AudioClip ac = map [audioClipId];
		if (ac != null) {
			if (waitPlaySoundData2Ds.Count < MAX_NUM) {
				PlaySoundData psd = PlaySoundData.Instance ().pools.Obtain ();
				psd.clip = ac;
				psd.volume = volume;
				psd.pitch = pitch;
				waitPlaySoundData2Ds.Add (psd);
			}

		} else {
			DebuggerUtil.LogError (" Sound Manager play2D, the audio clip is null");
		}
	}

	public void PlaySound3D (int audioClipId, Vector3 position, float volume)
	{     
				
		PlaySound3D (audioClipId, position, volume, 1f);
		
	}

	public void PlaySound3D (int audioClipId, Vector3 position, float volume, float pitch)
	{     

		if (!GameData.Instance ().M_SettingData.m_SoundOn)
			return;
		AudioClip ac = map [audioClipId];
		if (ac != null) {
			if (waitPlaySoundData3Ds.Count < MAX_NUM) {
				PlaySoundData psd = PlaySoundData.Instance ().pools.Obtain ();
				psd.clip = ac;
				psd.volume = volume;
				psd.pitch = pitch;
				psd.position = position;
				waitPlaySoundData3Ds.Add (psd);
			}
		} else {
			DebuggerUtil.LogError (" Sound Manager play3D, the audio clip is null");
		}
				 
	}

	void Update ()
	{
		Update_WaitPlaySound2D ();
		Update_WaitPlaySound3D ();
	}

	void Update_WaitPlaySound2D ()
	{
		if (waitPlaySoundData2Ds.Count == 0)
			return;
		AudioSource source = GetFreeSource2D ();
		if (source != null) {
			PlaySoundData psd = waitPlaySoundData2Ds [0];
			psd.Play (source);
			waitPlaySoundData2Ds.RemoveAt (0);
			PlaySoundData.Instance ().pools.Free (psd);
		}
	}
	
	void Update_WaitPlaySound3D ()
	{
		if (waitPlaySoundData3Ds.Count == 0)
			return;
		AudioSource source = GetFreeSource3D ();
		if (source != null) {
			PlaySoundData psd = waitPlaySoundData3Ds [0];
			psd.Play (source);
			waitPlaySoundData3Ds.RemoveAt (0);
			PlaySoundData.Instance ().pools.Free (psd);
		}
	}
		
}

public class PlaySoundData :IPoolable
{
	static PlaySoundData g_Instance;
	
	public static PlaySoundData Instance ()
	{
		if (g_Instance == null)
			g_Instance = new PlaySoundData ();
		return g_Instance;
	}
	
	public	PlaySoundData ()
	{
		InitPools ();
	}
	
	public  ObjectPool<PlaySoundData> pools;
	
	public  void InitPools ()
	{
		pools = new ObjectPool<PlaySoundData> (10, 5);
		pools.NewObject = NewSoundData;
		pools.Init ();
	}
	
	PlaySoundData  NewSoundData ()
	{
		return new PlaySoundData (1f);
	}
	
	PlaySoundData (float volume)
	{

	}

	public void Play (AudioSource source)
	{
		if (clip == null)
			return;
		source.transform.position = position;
		source.clip = clip;
		source.volume = volume;
		source.pitch = pitch;
		source.Play ();
	}
	
	public AudioClip clip;
	public Vector3 position;
	public float volume;
	public float pitch;
	
	public void IDestory ()
	{
		
	}
	
	public void IReset ()
	{
		clip = null;
		position = Vector3.zero;
		volume = 1f;
		pitch = 1f;
	}
}
