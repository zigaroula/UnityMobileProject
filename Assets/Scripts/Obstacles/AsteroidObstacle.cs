using UnityEngine;
using System.Collections;

public class AsteroidObstacle : MonoBehaviour {

	private float speed;
	private float xSpeed;
	private float ySpeed;
	private float zSpeed;

	void Start() {
		xSpeed = Random.Range (-2.0f, 2.0f);
		ySpeed = Random.Range (-2.0f, 2.0f);
		zSpeed = Random.Range (-2.0f, 2.0f);
	}

	void Update () {
		speed = GameManager.manager.GetGameSpeed ();
		transform.Rotate(new Vector3(speed*xSpeed, speed*ySpeed, speed*zSpeed));
	}
}
