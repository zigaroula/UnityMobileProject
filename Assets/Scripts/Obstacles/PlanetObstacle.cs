using UnityEngine;
using System.Collections;

public class PlanetObstacle : MonoBehaviour {

	public float RotateSpeed;

	void Update () {
		transform.Rotate (new Vector3 (0, RotateSpeed, 0));
	}
}
