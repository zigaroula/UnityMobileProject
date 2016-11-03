using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {

	public float MovementSpeed;
	private Vector3 initialPosition;
	private float xTar;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
		xTar = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		float x;
		x = AlternateKeyboardDebug ();
		//x = AlternateTouchControl ();
		if (x > -10) {
			xTar = x;
		}
		ProceedMovement (xTar);
		ClampPositionToScreen ();
	}

	private float AlternateKeyboardDebug() {
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Game) {
			float y = Input.mousePosition.y / Screen.height;
			if (y <= 0.9) {
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast (ray, out hit)) {
					return 2*hit.point.x;
				}
			}
		}
		return -10;
	}

	private float AlternateTouchControl() {
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Game) {
			if (Input.touchCount == 1) {
				Touch touch = Input.GetTouch (0);
				float y = touch.position.y / Screen.height;
				if (y <= 0.9) {
					RaycastHit hit;
					Ray ray = Camera.main.ScreenPointToRay(touch.position);
					if (Physics.Raycast (ray, out hit)) {
						return 2*hit.point.x;
					}
				}
			}
		}
		return -10;
	}

	private void ProceedMovement(float x) {
		float speed = GameManager.manager.GetGameSpeed();
		float xVelocity = 0.0f;
		float currentX = transform.position.x;
		float finalX = x;
		float targetX = Mathf.SmoothDamp (currentX, finalX, ref xVelocity, 0.05f/speed);
		transform.position = new Vector3 (targetX, transform.position.y, transform.position.z);
		float rotationAngle = 180.0f * (currentX-finalX)/6.0f;
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, rotationAngle, transform.eulerAngles.z);
	}

	private void ClampPositionToScreen() {
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Game) {
			Vector3 pos = transform.position;
			pos.x = Mathf.Clamp (transform.position.x, -3.0f, 3.0f);
			transform.position = pos;
		}
	}

	public void InitializeShip() {
		transform.position = new Vector3 (initialPosition.x, initialPosition.y, initialPosition.z);
		transform.rotation = Quaternion.identity;
		xTar = 0.0f;
	}
}
