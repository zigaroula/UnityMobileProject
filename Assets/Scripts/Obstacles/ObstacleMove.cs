using UnityEngine;
using System.Collections;

public class ObstacleMove : MonoBehaviour {

	private bool moving;
	private Vector3 initialPosition;
	public float ObstacleHeight;

	public enum ObstacleType {Saw, Laser, Box, Z, Asteroid, Sun, Planet};
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
			transform.Translate (0, -4.0f * speed * Time.deltaTime, 0, Space.World);
			if (obstacleTop < GameManager.manager.gameBot) {
				StopMoving ();
			}
		}
	}

	public void StartMoving() {
		transform.position = new Vector3 (transform.position.x, GameManager.manager.gameTop + (ObstacleHeight / 2), transform.position.z);
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
		if (Type == ObstacleType.Box || Type == ObstacleType.Z) {
			float rotate = (Random.Range (0.0f, 1.0f) >= 0.5 ? 1 : 0);
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, rotate * 180, transform.eulerAngles.z);
		} else if (Type == ObstacleType.Asteroid) {
			int pos = Random.Range (0, 3);
			if (pos == 0) {
				transform.position = new Vector3 (0.0f, transform.position.y, transform.position.z);
			} else if (pos == 1) {
				transform.position = new Vector3 (2.0f, transform.position.y, transform.position.z);
			} else if (pos == 2) {
				transform.position = new Vector3 (-2.0f, transform.position.y, transform.position.z);
			}
		} else if (Type == ObstacleType.Sun) {
			int pos = Random.Range (0, 2);
			transform.position = new Vector3 ((pos==1 ? -1 : 1) * 9, transform.position.y, transform.position.z);
		}
	}
}
