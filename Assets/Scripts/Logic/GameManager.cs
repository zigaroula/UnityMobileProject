using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public float gameTop = 6;
	public float gameLeft = -3;
	public float gameRight = 3;
	public float gameBot = -6;

	private float gameTime = 0.0f;
	private float gameSpeedIncrease = Mathf.Exp(1);
	private float currentGameSpeed = 1.0f;
	private float lastGameSpeed;

	private float currentScore;
	private int highestScore;

	public enum GameState {Menu, Game, Pause, GameOver};
	private GameState currentState;

	private bool unpausing = false;
	private float unpausingTimer = 3.0f;

	private GameObject generator;
	private GameObject ship;
	public GameObject Explosion;

	private UIManager uimanager;
	public GPGSHandler gpgs;

	public static GameManager manager;

	void Awake() {
		manager = this;
	}

	void Start () {
		computeGameCoordinates ();
		currentState = GameState.Menu;
		generator = GameObject.FindGameObjectWithTag ("Generator");
		ship = GameObject.FindGameObjectWithTag ("Ship");
		uimanager = GameObject.FindGameObjectWithTag ("Canvas").GetComponent<UIManager> ();
		highestScore = PlayerPrefs.GetInt ("PlayerScore");
		uimanager.UpdateHighestScore (highestScore);
	}

	void Update () {
		if (currentState == GameState.Game) {
			gameTime += Time.deltaTime;
			gameSpeedIncrease += Time.deltaTime/2;
			currentGameSpeed = Mathf.Log(gameSpeedIncrease);
			currentScore = (currentScore >= 999999?999999:Mathf.Pow(gameTime, 2));
			uimanager.UpdateScore(Mathf.FloorToInt(currentScore));
		}

		if (unpausing) {
			unpausingTimer -= Time.deltaTime;
			if (unpausingTimer <= 0.0f) {
				currentGameSpeed = lastGameSpeed;
				currentState = GameState.Game;
				unpausingTimer = 3.0f;
				unpausing = false;
			}
		}

		if (Input.GetKeyDown (KeyCode.Escape) && !unpausing) {
			PauseGame ();
			uimanager.RequestQuitGame ();
		}
	}

	public void GameOver() { // Game -> GameOver
		if (currentState == GameState.Game) {
			lastGameSpeed = currentGameSpeed;
			currentGameSpeed = 0.1f;
			Explosion.GetComponent<Explosion> ().PlayExplosion ();
			Camera.main.GetComponent<UISound> ().Explosion ();
			ship.GetComponent<ShipMove>().GameOver();
			int intCurrentScore = Mathf.FloorToInt (currentScore);
			uimanager.GameOver ();
			uimanager.UpdateFinalScore (intCurrentScore, highestScore);
			gpgs.PostScore (intCurrentScore);
			gpgs.UnlockScoreAchievements (intCurrentScore);
			gpgs.IncrementPlayCount ();
			if (intCurrentScore > highestScore) {
				highestScore = intCurrentScore;
				PlayerPrefs.SetInt ("PlayerScore", intCurrentScore);
				PlayerPrefs.Save ();
			}
			currentState = GameState.GameOver;
		}
	}

	public void StartGame() { // [Menu, Pause, GameOver] -> Game
		if (currentState == GameState.Menu || currentState == GameState.Pause || currentState == GameState.GameOver) {
			AdManager.HideBanner ();
			currentGameSpeed = 1.0f;
			gameSpeedIncrease = Mathf.Exp(1);
			gameTime = 0.0f;
			generator.GetComponent<Generator> ().InitializeObstacles ();
			ship.GetComponent<ShipMove> ().InitializeShip ();
			currentScore = 0;
			uimanager.StartGame ();
			currentState = GameState.Game;
		}
	}

	public void PauseGame() { // Game -> Pause
		if (currentState == GameState.Game) {
			AdManager.ShowBanner ();
			lastGameSpeed = currentGameSpeed;
			currentGameSpeed = 0.0f;
			uimanager.PauseGame ();
			currentState = GameState.Pause;
		}
	}

	public void UnpauseGame() { // Pause -> Game
		if (currentState == GameState.Pause) {
			AdManager.HideBanner ();
			unpausing = true;
			uimanager.UnpauseGame ();
		}
	}

	public void GoToMenu() { // [Pause, GameOver] -> Menu
		if (currentState == GameState.Pause || currentState == GameState.GameOver) {
			AdManager.ShowBanner ();
			currentGameSpeed = 1.0f;
			gameSpeedIncrease = Mathf.Exp(1);
			gameTime = 0.0f;
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

	private void computeGameCoordinates() {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(0, 0));
		if (Physics.Raycast (ray, out hit)) {
			gameBot =  2*hit.point.y;
			gameRight = 2 * hit.point.x;
		}
		ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width, Screen.height));
		if (Physics.Raycast (ray, out hit)) {
			gameTop =  2*hit.point.y;
			gameLeft = 2 * hit.point.x;
		}
	}
}
