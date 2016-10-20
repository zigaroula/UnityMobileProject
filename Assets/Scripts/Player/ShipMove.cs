using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {

	public float MovementSpeed;
	private Vector3 initialPosition;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {
		KeyboardDebug ();
		//TouchControl();
	}

	// Using keyboard for debugging purposes
	private void KeyboardDebug() {
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Game) {
			float speed = GameManager.manager.GetGameSpeed();
			if (Input.GetKey ("left")) {
				transform.Translate (-MovementSpeed * speed, 0, 0);
			} else if (Input.GetKey ("right")) {
				transform.Translate (MovementSpeed * speed, 0, 0);
			}
			ClampPositionToScreen ();
		}
	}

	private void TouchControl() {
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Game) {
			if (Input.touchCount == 1) {
				Touch touch = Input.GetTouch (0);
				float x = touch.position.x / Screen.width;
				float y = touch.position.y / Screen.height;
				float speed = GameManager.manager.GetGameSpeed();
				if (y <= 0.9) {
					transform.Translate ((x > 0.5f ? 1 : -1) * MovementSpeed * speed, 0, 0);
				}
			}
			ClampPositionToScreen ();
		}
	}

	private void ClampPositionToScreen() {
		Vector3 pos = transform.position;
		pos.x =  Mathf.Clamp(transform.position.x, -3.0f, 3.0f);
		transform.position = pos;
	}

	public void InitializeShip() {
		transform.position = new Vector3 (initialPosition.x, initialPosition.y, initialPosition.z);
	}
}
