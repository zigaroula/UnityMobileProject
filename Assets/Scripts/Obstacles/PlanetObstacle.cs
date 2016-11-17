using UnityEngine;
using System.Collections;

public class PlanetObstacle : MonoBehaviour {

	private float rotateSpeed;
	private int rotateDirection;

	void Start() {
		rotateSpeed = Random.Range (0.5f, 5.0f);
		rotateDirection = (Random.Range (0, 2) == 0 ? 1 : -1);
	}

	void Update () {
		transform.Rotate (new Vector3 (0, rotateDirection * rotateSpeed, 0));
	}
}
