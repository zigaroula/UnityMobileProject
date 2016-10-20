using UnityEngine;
using System.Collections;

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
	public GameObject MenuUI;
	public GameObject GameUI;
	public GameObject PauseUI;
	public GameObject GameOverUI;

	void Start() {
		MenuUI.SetActive (true);
		GameUI.SetActive (false);
		PauseUI.SetActive (false);
		GameOverUI.SetActive (false);
	}

	void Update() {

	}

	public void GameOver() { // Game -> GameOver
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Game) {
			GameOverUI.SetActive (true);
		}
	}

	public void StartGame() { // [Menu, Pause, GameOver] -> Game
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Menu ||
			GameManager.manager.GetCurrentGameState () == GameManager.GameState.Pause ||
			GameManager.manager.GetCurrentGameState () == GameManager.GameState.GameOver) {
			MenuUI.SetActive (false);
			PauseUI.SetActive (false);
			GameOverUI.SetActive (false);
			GameUI.SetActive (true);
		}
	}

	public void PauseGame() { // Game -> Pause
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Game) {
			PauseUI.SetActive (true);
		}
	}

	public void UnpauseGame() { // Pause -> Game
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Pause) {
			PauseUI.SetActive (false);
		}
	}

	public void GoToMenu() { // [Pause, GameOver] -> Menu
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Pause ||
			GameManager.manager.GetCurrentGameState () == GameManager.GameState.GameOver) {
			PauseUI.SetActive (false);
			GameOverUI.SetActive (false);
			MenuUI.SetActive (true);
		}
	}
}
