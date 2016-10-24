using UnityEngine;
using System.Collections;

public class SawObstacle : MonoBehaviour {

	private bool moveLeft;

	// Use this for initialization
	void Start () {
		moveLeft = true;
	}
	
	// Update is called once per frame
	void Update () {
		float speed = GameManager.manager.GetGameSpeed ();
		transform.Translate ((moveLeft ? 1 : -1) * 0.05f * speed, 0.0f, 0.0f);
		if (transform.position.x >= 3.0f) {
			moveLeft = false;
		} else if (transform.position.x <= -3.0f) {
			moveLeft = true;
		}
	}
}
