using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public float GameSpeed;

	public static GameManager manager;

	void Awake() {
		manager = this;
	}

	void Start () {
		
	}

	void Update () {
		GameSpeed += 0.001f;
	}
}
