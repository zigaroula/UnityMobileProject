using UnityEngine;
using System.Collections;

public class CubeMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0, -0.1f * GlobalVar.GameSpeed, 0);
		float y = transform.position.y;
		if (y < -10) {
			Destroy(gameObject);
		}
	}
}
