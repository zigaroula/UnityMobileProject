using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

	private string soundEnabled;
	private string musicEnabled;

	private bool initializing = true;

	public GameObject SoundCheckBox;
	public GameObject MusicCheckBox;

	void Start () {
		if (PlayerPrefs.GetString ("Sound") == "") {
			PlayerPrefs.SetString ("Sound", "on");
			soundEnabled = "on";
			PlayerPrefs.Save ();
		} else {
			soundEnabled = PlayerPrefs.GetString ("Sound");
		}

		if (PlayerPrefs.GetString ("Music") == "") {
			PlayerPrefs.SetString ("Music", "on");
			musicEnabled = "on";
			PlayerPrefs.Save ();
		} else {
			musicEnabled = PlayerPrefs.GetString ("Music");
		}

		SoundCheckBox.GetComponent<Toggle> ().isOn = (soundEnabled == "on");
		MusicCheckBox.GetComponent<Toggle> ().isOn = (musicEnabled == "on");
		initializing = false;
	}

	void Update () {
		
	}
		
	public void ToggleSound() {
		if (initializing) {
			return;
		}
		soundEnabled = (soundEnabled == "on") ? "off" : "on";
		PlayerPrefs.SetString ("Sound", soundEnabled);
		PlayerPrefs.Save ();
	}

	public void ToggleMusic() {
		if (initializing) {
			return;
		}
		musicEnabled = (musicEnabled == "on") ? "off" : "on";
		PlayerPrefs.SetString ("Music", musicEnabled);
		PlayerPrefs.Save ();
	}

	public void SaveChanges() {
		PlayerPrefs.SetString ("Sound", soundEnabled);
		PlayerPrefs.SetString ("Music", musicEnabled);
		PlayerPrefs.Save ();
	}
}
