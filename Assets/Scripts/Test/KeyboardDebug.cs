using UnityEngine;
using System.Collections;

public class KeyboardDebug : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR
			if (Input.GetKey (KeyCode.A)) {
				PlayerPrefs.DeleteKey ("PlayerScore");
				PlayerPrefs.DeleteKey ("Sound");
				PlayerPrefs.DeleteKey ("Music");
			}

			if (Input.GetKey (KeyCode.Z)) {
				print(PlayerPrefs.GetString ("Sound"));
			}
		#endif
	}
}
