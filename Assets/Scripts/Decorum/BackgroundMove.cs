using UnityEngine;
using System.Collections;

public class BackgroundMove : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0, 0, -0.05f * GlobalVar.GameSpeed);
		if (transform.position.y <= -26) {
			transform.position = new Vector3(transform.position.x, transform.position.y + GetComponent<Renderer>().bounds.size.y*2, transform.position.z);
		}
	}
}
