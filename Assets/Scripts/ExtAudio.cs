using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
//using UnityEngine.AudioModule;

public class ExtAudio : MonoBehaviour
{
	public AudioMixer mainMixer;
    public static AudioSource sounding;
    public static AudioClip bombPickup;
	public static AudioClip fuse;
    public static AudioClip boom;
    public static AudioClip healthPickup;
	public static AudioClip bulletFire;
    public static AudioClip bulletImpact;
	
	void Start() {
		sounding = gameObject.AddComponent<AudioSource>();
		sounding.outputAudioMixerGroup = mainMixer.FindMatchingGroups("SoundFx")[0];
		bombPickup = Resources.Load<AudioClip>("Audio/FX/bombCollect");
		fuse = Resources.Load<AudioClip>("Audio/FX/bombFuse");
		boom = Resources.Load<AudioClip>("Audio/FX/bigBoom");
		healthPickup = Resources.Load<AudioClip>("Audio/FX/healthPickup");
		bulletFire = Resources.Load<AudioClip>("Audio/FX/teabag_gun_1");
		bulletImpact = Resources.Load<AudioClip>("Audio/FX/teabag_gun_2");
	}
	
}
