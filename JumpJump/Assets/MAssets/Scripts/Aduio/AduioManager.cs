using UnityEngine;
using System.Collections;

public class AduioManager  {

	public AudioSource Play(AudioSource source,AudioClip clip, Transform emitter)    
	{    
		return Play(source,clip, emitter, 1f, 1f);    
	} 

	public AudioSource Play(AudioSource source,AudioClip clip, Transform emitter, float volume)    
	{    
		return Play(source,clip, emitter, volume, 1f);    
	}    

	public AudioSource Play(AudioSource source, AudioClip clip, Transform emitter, float volume, float pitch)    
	{    	  
		source.clip = clip;    
		source.volume = volume;    
		source.pitch = pitch;    
		source.Play ();      
		return source;    
	}    

	public AudioSource Play(AudioSource source,AudioClip clip, Vector3 point)    
	{    
		return Play(source,clip, point, 1f, 1f);    
	}    

	public AudioSource Play(AudioSource source,AudioClip clip, Vector3 point, float volume)    
	{    
		return Play(source,clip, point, volume, 1f);    
	}    

	public AudioSource Play(AudioSource source,AudioClip clip, Vector3 point, float volume, float pitch)    
	{     
	
		source.clip = clip;    
		source.volume = volume;    
		source.pitch = pitch;    
		source.Play();    
		return source;    
	} 
}
