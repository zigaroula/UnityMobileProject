using UnityEngine;
using System.Collections;

public class ObstacleMove : MonoBehaviour {

	private bool moving;
	private Vector3 initialPosition;
	public float ObstacleHeight;

	public enum ObstacleType {Saw, Laser, Box, Z};
	public ObstacleType Type;

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
		specialBehaviour ();
	}

	public void StopMoving() {
		transform.position = new Vector3 (initialPosition.x, initialPosition.y, initialPosition.z);
		moving = false;
	}

	public void InitializeObstacle() {
		transform.position = new Vector3 (initialPosition.x, initialPosition.y, initialPosition.z);
		moving = false;
	}

	private void specialBehaviour() {
		if (Type == ObstacleType.Box) {
			float rotate = (Random.Range (0.0f, 1.0f)>=0.5?1:0);
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotate*180, transform.eulerAngles.z);
		}
	}
}
