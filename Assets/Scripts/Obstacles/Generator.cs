using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	public GameObject obstacle1;
	public GameObject obstacle2;
	public GameObject obstacle3;

	private GameObject[] obstacles;
	private float[] obstaclesProbabilities;
	private int iteration;
	private int obstacleNumber;

	private GameObject[] instantiatedObstacles;

	// Use this for initialization
	void Start () {
		InitializeGenerator ();
	}
	
	// Update is called once per frame
	void Update () {
		instantiatedObstacles = GameObject.FindGameObjectsWithTag ("Obstacle");
		bool newObstacleRequired = true;
		foreach (GameObject obstacle in instantiatedObstacles) {
			if (obstacle.transform.position.y >= -5) {
				newObstacleRequired = false;
				break;
			}
		}
		if (newObstacleRequired) {
			Instantiate (PickOneObstacle ());
		}

		GlobalVar.GameSpeed += 0.001f;
	}

	public void InitializeGenerator() {
		obstacles = new GameObject[3];
		obstaclesProbabilities = new float[3];
		obstacles [0] = obstacle1;
		obstacles [1] = obstacle2;
		obstacles [2] = obstacle3;
		obstacleNumber = obstacles.Length;
		for (int i = 0 ; i < obstacleNumber ; i++) {
			obstaclesProbabilities [i] = 1.0f;
		}
		iteration = 1;
	}

	private GameObject PickOneObstacle() {
		float randomNumber = Random.Range (0.0f, iteration*obstacleNumber*1.0f);
		for (int i = 0; i < obstacleNumber; i++) {
			float pmin, pmax;
			if (i == 0) {
				pmin = 0;
				pmax = obstaclesProbabilities [0];
			} else {
				float prob = 0.0f;
				for (int j = 0; j < i; j++) {
					prob += obstaclesProbabilities [j];
				}
				pmin = prob;
				pmax = prob + obstaclesProbabilities [i];
			}
			if (randomNumber >= pmin && randomNumber < pmax) { // FIXME : MAY BE BUGGED
				for (int j = 0; j < obstacleNumber; j++) {
					obstaclesProbabilities [j] += 1.0f;
				}
				iteration++;
				for (int j = 0; j < obstacleNumber; j++) {
					if (i != j) {
						obstaclesProbabilities [j] += (obstaclesProbabilities [i] / (obstacleNumber - 1));
					}
				}
				obstaclesProbabilities [i] = 0.0f;
				return obstacles[i];
			}
		}
		return obstacles [0];
	}
}
