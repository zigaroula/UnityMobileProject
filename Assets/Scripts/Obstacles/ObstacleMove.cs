using UnityEngine;
using System.Collections;

public class ObstacleMove : MonoBehaviour {

	private bool moving;
	private Vector3 initialPosition;
	public float ObstacleHeight;

	// Use this for initialization
	void Start () {
		moving = false;
		initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (moving && (GameManager.manager.GetCurrentGameState() == GameManager.GameState.Game || GameManager.manager.GetCurrentGameState() == GameManager.GameState.GameOver)) {
			float speed = GameManager.manager.GetGameSpeed();
			float obstacleTop = transform.position.y + (ObstacleHeight / 2);
			transform.Translate (0, -0.1f * speed, 0);
			if (obstacleTop < -12) {
				StopMoving ();
			}
		}
	}

	public void StartMoving() {
		transform.position = new Vector3 (transform.position.x, 12 + (ObstacleHeight / 2), transform.position.z);
		moving = true;
	}

	public void StopMoving() {
		transform.position = new Vector3 (initialPosition.x, initialPosition.y, initialPosition.z);
		moving = false;
	}

	public void InitializeObstacle() {
		transform.position = new Vector3 (initialPosition.x, initialPosition.y, initialPosition.z);
		moving = false;
	}
}
