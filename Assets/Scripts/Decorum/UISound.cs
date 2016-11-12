using UnityEngine;
using System.Collections;

public class UISound : MonoBehaviour {

	public AudioClip ButtonSound;
	public AudioClip CountdownSound;
	public AudioClip ExplosionSound;

	private bool bgmPlaying;

	void Start () {
		bgmPlaying = true;
	}

	void Update() {
		if (SoundManager.MusicEnabled () && !bgmPlaying) {
			gameObject.GetComponent<AudioSource> ().Play ();
			bgmPlaying = true;
		} else if (!SoundManager.MusicEnabled () && bgmPlaying) {
			gameObject.GetComponent<AudioSource> ().Stop ();
			bgmPlaying = false;
		}
	}

	public void ClickButton() {
		if (SoundManager.SoundEnabled ()) {
			gameObject.GetComponent<AudioSource> ().PlayOneShot (ButtonSound);
		}
	}

	public void CountDown() {
		if (SoundManager.SoundEnabled ()) {
			gameObject.GetComponent<AudioSource> ().PlayOneShot (CountdownSound);
		}
	}

	public void Explosion() {
		if (SoundManager.SoundEnabled ()) {
			gameObject.GetComponent<AudioSource> ().PlayOneShot (ExplosionSound);
		}
	}
}
