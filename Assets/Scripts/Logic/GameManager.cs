using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	private float currentGameSpeed = 1.0f;
	private float lastGameSpeed;
	private float currentScore;
	private int highestScore;

	public enum GameState {Menu, Game, Pause, GameOver};
	private GameState currentState;

	private GameObject generator;
	private GameObject ship;

	private UIManager uimanager;

	public static GameManager manager;

	void Awake() {
		manager = this;
	}

	void Start () {
		currentState = GameState.Menu;
		generator = GameObject.FindGameObjectWithTag ("Generator");
		ship = GameObject.FindGameObjectWithTag ("Ship");
		uimanager = GameObject.FindGameObjectWithTag ("Canvas").GetComponent<UIManager> ();
		highestScore = PlayerPrefs.GetInt ("PlayerScore");
		uimanager.UpdateHighestScore (highestScore);
	}

	void Update () {
		if (currentState == GameState.Game) {
			currentGameSpeed += 0.001f; // FIXME : another formula required
			currentScore += currentGameSpeed * 0.02f;
			uimanager.UpdateScore(Mathf.FloorToInt(currentScore));
		}
	}

	public void GameOver() { // Game -> GameOver
		if (currentState == GameState.Game) {
			lastGameSpeed = currentGameSpeed;
			currentGameSpeed = 0.1f;
			int intCurrentScore = Mathf.FloorToInt (currentScore);
			if (intCurrentScore > highestScore) {
				highestScore = intCurrentScore;
				PlayerPrefs.SetInt ("PlayerScore", intCurrentScore);
				PlayerPrefs.Save ();
			}
			uimanager.GameOver ();
			currentState = GameState.GameOver;
		}
	}

	public void StartGame() { // [Menu, Pause, GameOver] -> Game
		if (currentState == GameState.Menu || currentState == GameState.Pause || currentState == GameState.GameOver) {
			currentGameSpeed = 1.0f;
			generator.GetComponent<Generator> ().InitializeObstacles ();
			ship.GetComponent<ShipMove> ().InitializeShip ();
			currentScore = 0;
			uimanager.StartGame ();
			currentState = GameState.Game;
		}
	}

	public void PauseGame() { // Game -> Pause
		if (currentState == GameState.Game) {
			uimanager.PauseGame ();
			currentState = GameState.Pause;
		}
	}

	public void UnpauseGame() { // Pause -> Game
		if (currentState == GameState.Pause) {
			uimanager.UnpauseGame ();
			currentState = GameState.Game;
		}
	}

	public void GoToMenu() { // [Pause, GameOver] -> Menu
		if (currentState == GameState.Pause || currentState == GameState.GameOver) {
			currentGameSpeed = 1.0f;
			generator.GetComponent<Generator> ().InitializeObstacles ();
			ship.GetComponent<ShipMove> ().InitializeShip ();
			uimanager.UpdateHighestScore (highestScore);
			uimanager.GoToMenu ();
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
