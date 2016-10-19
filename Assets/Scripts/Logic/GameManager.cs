using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public float currentGameSpeed = 1.0f;
	private float lastGameSpeed;

	public enum GameState {Menu, Game, Pause, GameOver};
	private GameState currentState;

	private GameObject generator;
	private GameObject ship;

	public static GameManager manager;

	void Awake() {
		manager = this;
	}

	void Start () {
		currentState = GameState.Menu;
		generator = GameObject.FindGameObjectWithTag ("Generator");
		ship = GameObject.FindGameObjectWithTag ("Ship");
	}

	void Update () {
		if (currentState == GameState.Game) {
			currentGameSpeed += 0.001f; // FIXME : another formula required
		}
	}

	public void GameOver() { // Game -> GameOver
		if (currentState == GameState.Game) {
			lastGameSpeed = currentGameSpeed;
			currentGameSpeed = 0.1f;
			currentState = GameState.GameOver;
		}
	}

	public void StartGame() { // [Menu, Pause, GameOver] -> Game
		if (currentState == GameState.Menu || currentState == GameState.Pause || currentState == GameState.GameOver) {
			currentGameSpeed = 1.0f;
			generator.GetComponent<Generator> ().InitializeObstacles ();
			ship.GetComponent<ShipMove> ().InitializeShip ();
			currentState = GameState.Game;
		}
	}

	public void PauseGame() { // Game -> Pause
		if (currentState == GameState.Game) {
			currentState = GameState.Pause;
		}
	}

	public void UnpauseGame() { // Pause -> Game
		if (currentState == GameState.Pause) {
			currentState = GameState.Game;
		}
	}

	public void GoToMenu() { // [Pause, GameOver] -> Menu
		if (currentState == GameState.Pause || currentState == GameState.GameOver) {
			currentGameSpeed = 1.0f;
			generator.GetComponent<Generator> ().InitializeObstacles ();
			ship.GetComponent<ShipMove> ().InitializeShip ();
			currentState = GameState.Menu;
		}
	}

	public GameState GetCurrentGameState() {
		return currentState;
	}

	public float GetGameSpeed() {
		return currentGameSpeed;
	}
}
