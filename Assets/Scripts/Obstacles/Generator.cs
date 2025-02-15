﻿using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {
	
	private GameObject[] obstacles;
	private float[] obstaclesProbabilities;
	private int iteration;
	private int obstacleNumber;

	// Use this for initialization
	void Start () {
		InitializeGenerator ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Game) {
			bool newObstacleRequired = true;
			foreach (GameObject obstacle in obstacles) {
				float y = obstacle.transform.position.y;
				float obstacleTop = obstacle.transform.position.y + (obstacle.GetComponent<ObstacleMove>().ObstacleHeight / 2);
				if (obstacleTop >= GameManager.manager.gameBot && y <= 100) {
					newObstacleRequired = false;
					break;
				}
			}
			if (newObstacleRequired) {
				PickOneObstacle ().GetComponent<ObstacleMove> ().StartMoving ();
			}
		}
	}

	public void InitializeGenerator() {
		obstacles = GameObject.FindGameObjectsWithTag ("Obstacle");
		obstacleNumber = obstacles.Length;
		obstaclesProbabilities = new float[obstacleNumber];
		for (int i = 0 ; i < obstacleNumber ; i++) {
			obstaclesProbabilities [i] = 1.0f;
		}
		iteration = 1;
	}

	public void InitializeObstacles() {
		foreach (GameObject obstacle in obstacles) {
			obstacle.GetComponent<ObstacleMove> ().InitializeObstacle ();
		}
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
			if ((randomNumber >= pmin && randomNumber < pmax && pmax != iteration*obstacleNumber) || (randomNumber >= pmin && randomNumber <= pmax && pmax == iteration*obstacleNumber)) { // FIXME : MAY BE BUGGED
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
