using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayMusic : MonoBehaviour {


	public AudioClip titleMusic;					//Assign Audioclip for title music loop
	public AudioClip mainMusic;						//Assign Audioclip for main 
	public AudioMixerSnapshot volumeDown;			//Reference to Audio mixer snapshot in which the master volume of main mixer is turned down
	public AudioMixerSnapshot volumeUp;				//Reference to Audio mixer snapshot in which the master volume of main mixer is turned up


	private AudioSource musicSource;				//Reference to the AudioSource which plays music
	private float resetTime = .01f;					//Very short time used to fade in near instantly without a click


	void Awake () 
	{
		//Get a component reference to the AudioSource attached to the UI game object
		musicSource = GetComponent<AudioSource> ();
		//Call the PlayLevelMusic function to start playing music
	}


	public void PlayLevelMusic()
	{
		//This switch looks at the last loadedLevel number using the scene index in build settings to decide which music clip to play.

		musicSource.clip = Resources.Load<AudioClip>("Audio/Music/gamemusic");
		

		//Fade up the volume very quickly, over resetTime seconds (.01 by default)
		FadeUp (resetTime);
		//Play the assigned music clip in musicSource
		musicSource.Play ();
	}
	
		public void PlayMenuMusic()
	{
		//This switch looks at the last loadedLevel number using the scene index in build settings to decide which music clip to play.

		musicSource.clip = Resources.Load<AudioClip>("Audio/Music/itsit");
		

		//Fade up the volume very quickly, over resetTime seconds (.01 by default)
		FadeUp (resetTime);
		//Play the assigned music clip in musicSource
		musicSource.Play ();
	}
	
	
	//Used if running the game in a single scene, takes an integer music source allowing you to choose a clip by number and play.


	//Call this function to very quickly fade up the volume of master mixer
	public void FadeUp(float fadeTime)
	{
		//call the TransitionTo function of the audioMixerSnapshot volumeUp;
		volumeUp.TransitionTo (fadeTime);
	}

	//Call this function to fade the volume to silence over the length of fadeTime
	public void FadeDown(float fadeTime)
	{
		//call the TransitionTo function of the audioMixerSnapshot volumeDown;
		volumeDown.TransitionTo (fadeTime);
	}
}
