using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private GameObject[] menuUI;
	private GameObject[] gameUI;
	private GameObject[] pauseUI;
	private GameObject[] gameOverUI;
	private GameObject[] fogUI;
	public GameObject ScoreText;
	public GameObject HighestScoreText;
	public GameObject UnpauseText;

	private bool unpausing = false;
	private float unpausingTimer = 3.0f;

	void Start() {
		menuUI = GameObject.FindGameObjectsWithTag ("MenuUI");
		gameUI = GameObject.FindGameObjectsWithTag ("GameUI");
		pauseUI = GameObject.FindGameObjectsWithTag ("PauseUI");
		gameOverUI = GameObject.FindGameObjectsWithTag ("GameOverUI");
		fogUI = GameObject.FindGameObjectsWithTag ("FogUI");
		ShowUI (menuUI);
		HideUI (gameUI);
		HideUI (pauseUI);
		HideUI (gameOverUI);
		HideUI (fogUI);
	}

	void Update() {
		if (unpausing) {
			unpausingTimer -= Time.deltaTime;
			UnpauseText.GetComponent <Text> ().text = Mathf.Ceil (unpausingTimer).ToString();
			if (unpausingTimer <= 0.0f) {
				UnpauseText.GetComponent <Text> ().text = "";
				HideUI (fogUI);
				unpausingTimer = 3.0f;
				unpausing = false;
			}
		}
	}

	public void GameOver() { // Game -> GameOver
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Game) {
			HideUI (fogUI);
			ShowUI (gameOverUI);
		}
	}

	public void StartGame() { // [Menu, Pause, GameOver] -> Game
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Menu ||
			GameManager.manager.GetCurrentGameState () == GameManager.GameState.Pause ||
			GameManager.manager.GetCurrentGameState () == GameManager.GameState.GameOver) {
			HideUI (menuUI);
			HideUI (pauseUI);
			HideUI (gameOverUI);
			HideUI (fogUI);
			ShowUI (gameUI);
		}
	}

	public void PauseGame() { // Game -> Pause
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Game) {
			ShowUI (pauseUI);
			ShowUI (fogUI);
		}
	}

	public void UnpauseGame() { // Pause -> Game
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Pause) {
			HideUI (pauseUI);
			unpausing = true;
		}
	}

	public void GoToMenu() { // [Pause, GameOver] -> Menu
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Pause ||
			GameManager.manager.GetCurrentGameState () == GameManager.GameState.GameOver) {
			HideUI (pauseUI);
			HideUI (gameOverUI);
			HideUI (gameUI);
			HideUI (fogUI);
			ShowUI (menuUI);
		}
	}

	public void UpdateScore(int score) {
		ScoreText.GetComponent<Text> ().text = "<color=#ff0000ff>Score</color> " + score;
	}

	public void UpdateHighestScore(int score) {
		HighestScoreText.GetComponent<Text> ().text = "<color=#ff0000ff>Highscore</color> " + score;
	}

	private void ShowUI(GameObject[] arrayUI) {
		foreach (GameObject element in arrayUI) {
			element.SetActive (true);
		}
	}

	private void HideUI(GameObject[] arrayUI) {
		foreach (GameObject element in arrayUI) {
			element.SetActive (false);
		}
	}
}
