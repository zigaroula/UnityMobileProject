using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {

	public float MovementSpeed;

	// Use this for initialization
	void Start () {

	}

	// Using keyboard for debugging purposes
	void Update() {
		float speed = GameManager.manager.GameSpeed;
		if (Input.GetKey ("left")) {
			transform.Translate (-MovementSpeed * speed, 0, 0);
		} else if (Input.GetKey ("right")) {
			transform.Translate (MovementSpeed * speed, 0, 0);
		}
		ClampPositionToScreen ();
	}

	private void ClampPositionToScreen() {
		Vector3 pos = transform.position;
		pos.x =  Mathf.Clamp(transform.position.x, -3.0f, 3.0f);
		transform.position = pos;
	}

	/*
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch (0);
			float x = touch.position.x / Screen.width;
			float y = touch.position.y / Screen.height;
			if (y <= 0.9) {
				transform.Translate ((x > 0.5f ? 1 : -1) * 0.1f * GlobalVar.GameSpeed, 0, 0);
			}
		}
		ClampPositionToScreen ();
	}
	*/
}
