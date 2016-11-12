using UnityEngine;
using System.Collections;

public class ShipSound : MonoBehaviour {

	public GameObject Ship;

	private bool playing;

	void Start() {
		playing = false;
		gameObject.GetComponent<AudioSource> ().Pause ();
	}

	void Update () {
		if (SoundManager.SoundEnabled()) {
			if (!playing && GameManager.manager.GetCurrentGameState () == GameManager.GameState.Game) {
				gameObject.GetComponent<AudioSource> ().UnPause ();
				playing = true;
			}
			if (playing && GameManager.manager.GetCurrentGameState () != GameManager.GameState.Game) {
				gameObject.GetComponent<AudioSource> ().Pause ();
				playing = false;
			}
		} else if (playing) {
			gameObject.GetComponent<AudioSource> ().Pause ();
			playing = false;
		}
		transform.position = new Vector3(Ship.transform.position.x, transform.position.y, transform.position.z);
	}
}
