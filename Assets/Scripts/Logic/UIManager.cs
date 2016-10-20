using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	/*
	public GameObject StartButton;
	public GameObject PauseButton;
	public GameObject UnpauseButton;
	public GameObject RestartButton;
	public GameObject MenuButton;
	public GameObject RestartGOButton;
	public GameObject MenuGOButton;
	public GameObject ScoreText;
	public GameObject ScoreGOText;
	*/
	private GameObject[] menuUI;
	private GameObject[] gameUI;
	private GameObject[] pauseUI;
	private GameObject[] gameOverUI;
	public GameObject ScoreText;
	public GameObject HighestScoreText;

	void Start() {
		menuUI = GameObject.FindGameObjectsWithTag ("MenuUI");
		gameUI = GameObject.FindGameObjectsWithTag ("GameUI");
		pauseUI = GameObject.FindGameObjectsWithTag ("PauseUI");
		gameOverUI = GameObject.FindGameObjectsWithTag ("GameOverUI");
		ShowUI (menuUI);
		HideUI (gameUI);
		HideUI (pauseUI);
		HideUI (gameOverUI);
	}

	void Update() {
		
	}

	public void GameOver() { // Game -> GameOver
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Game) {
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
			ShowUI (gameUI);
		}
	}

	public void PauseGame() { // Game -> Pause
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Game) {
			ShowUI (pauseUI);
		}
	}

	public void UnpauseGame() { // Pause -> Game
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Pause) {
			HideUI (pauseUI);
		}
	}

	public void GoToMenu() { // [Pause, GameOver] -> Menu
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Pause ||
			GameManager.manager.GetCurrentGameState () == GameManager.GameState.GameOver) {
			HideUI (pauseUI);
			HideUI (gameOverUI);
			HideUI (gameUI);
			ShowUI (menuUI);
		}
	}

	public void UpdateScore(int score) {
		ScoreText.GetComponent<Text> ().text = "Score : " + score;
	}

	public void UpdateHighestScore(int score) {
		HighestScoreText.GetComponent<Text> ().text = "Highest Score : " + score;
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
