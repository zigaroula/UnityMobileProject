using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	private int count = 0;
	public GameObject cubePrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		count = count + 1;
		if (count >= 100) {
			count = 0;
			int space = Random.Range (0, 3);
			switch (space) {
			case 0:
				Instantiate (cubePrefab, new Vector3 (0, 10, 0), Quaternion.identity);
				Instantiate (cubePrefab, new Vector3 (2, 10, 0), Quaternion.identity);
				break;
			case 1:
				Instantiate (cubePrefab, new Vector3 (-2, 10, 0), Quaternion.identity);
				Instantiate (cubePrefab, new Vector3 (2, 10, 0), Quaternion.identity);
				break;
			case 2:
				Instantiate (cubePrefab, new Vector3 (-2, 10, 0), Quaternion.identity);
				Instantiate (cubePrefab, new Vector3 (0, 10, 0), Quaternion.identity);
				break;
			default:
				break;
			}
		}
	}
}
