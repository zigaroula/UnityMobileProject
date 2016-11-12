using UnityEngine;
using System.Collections;

public class AsteroidObstacle : MonoBehaviour {

	private float speed;

	void Update () {
		speed = GameManager.manager.GetGameSpeed ();
		transform.Rotate(new Vector3(speed*2.0f, speed*1.0f, -speed));
	}
}
