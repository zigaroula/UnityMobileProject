using UnityEngine;
using System.Collections;

public class ObstacleMove : MonoBehaviour {

	private bool moving;
	private Vector3 initialPosition;

	// Use this for initialization
	void Start () {
		moving = false;
		initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (moving && (GameManager.manager.GetCurrentGameState() == GameManager.GameState.Game || GameManager.manager.GetCurrentGameState() == GameManager.GameState.GameOver)) {
			float speed = GameManager.manager.GetGameSpeed();
			float y = transform.position.y;
			transform.Translate (0, -0.1f * speed, 0);
			if (y < -10) {
				StopMoving ();
			}
		}
	}

	public void StartMoving() {
		transform.position = new Vector3 (transform.position.x, 10, transform.position.z);
		moving = true;
	}

	public void StopMoving() {
		transform.position = new Vector3 (transform.position.x, 25, transform.position.z);
		moving = false;
	}

	public void InitializeObstacle() {
		transform.position = new Vector3 (initialPosition.x, initialPosition.y, initialPosition.z);
		moving = false;
	}
}
