using UnityEngine;
using System.Collections;

public class KeyboardDebug : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)) {
		} else if (Input.GetKey (KeyCode.Z)) {
			GameManager.manager.StartGame ();
		} else if (Input.GetKey (KeyCode.E)) {
			GameManager.manager.PauseGame ();
		} else if (Input.GetKey (KeyCode.R)) {
			GameManager.manager.UnpauseGame ();
		} else if (Input.GetKey (KeyCode.T)) {
			GameManager.manager.GoToMenu ();
		}
	}
}
