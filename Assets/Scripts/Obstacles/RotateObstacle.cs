using UnityEngine;
using System.Collections;

public class RotateObstacle : MonoBehaviour {

	public float RotationSpeed;

	void Update () {
		transform.Rotate (new Vector3 (0, 0, RotationSpeed));
	}
}
