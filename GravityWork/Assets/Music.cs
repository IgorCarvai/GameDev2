using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

	private AudioSource source_music;
	public AudioClip[] music;

	void Awake (){
		source_music = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		if (source_music.isPlaying == false) {
			source_music.PlayOneShot (music [Random.Range (0, music.Length)], 0.25F);

		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
