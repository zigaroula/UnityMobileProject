using UnityEngine;
using System.Collections;

public class ObstacleMove : MonoBehaviour {

	private bool moving;

	// Use this for initialization
	void Start () {
		moving = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			float speed = GameManager.manager.GameSpeed;
			transform.Translate (0, -0.1f * speed, 0);
			float y = transform.position.y;
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
}
