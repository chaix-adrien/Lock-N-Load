using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_AudioPlayer : MonoBehaviour {
	private AudioSource source;
	void Start () {
		source = GetComponent<AudioSource>();
	}
	public void play(AudioClip clip) {
		source.PlayOneShot(clip);
	}
}
