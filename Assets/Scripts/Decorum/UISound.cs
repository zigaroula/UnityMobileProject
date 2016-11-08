using UnityEngine;
using System.Collections;

public class UISound : MonoBehaviour {

	public AudioClip ButtonSound;
	public AudioClip CountdownSound;
	public AudioClip ExplosionSound;

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
