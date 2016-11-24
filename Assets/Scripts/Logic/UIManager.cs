using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private GameObject[] menuUI;
	private GameObject[] gameUI;
	private GameObject[] pauseUI;
	private GameObject[] gameOverUI;
	private GameObject[] fogUI;
	private GameObject[] infoUI;
	private GameObject[] settingsUI;
	private GameObject[] exitUI;
	public GameObject ScoreText;
	public GameObject HighestScoreText;
	public GameObject UnpauseText;
	public GameObject ScoreGOText;
	public GameObject MainCamera;
	public GameObject MenuGOButton;
	public GameObject RestartGOButton;
	public Image MenuGOImage;
	public Image RestartGOImage;

	private bool unpausing = false;
	private float unpausingTimer = 3.0f;
	private int lastUnpausingTimer = 0;

	private int gameOverCount;
	private bool gameOvering = false;
	private bool gameOverAdRequested = false;
	private float gameOveringTimer = 2.0f;
	private float timeElapsed = 0.0f;

	void Start() {
		menuUI = GameObject.FindGameObjectsWithTag ("MenuUI");
		gameUI = GameObject.FindGameObjectsWithTag ("GameUI");
		pauseUI = GameObject.FindGameObjectsWithTag ("PauseUI");
		gameOverUI = GameObject.FindGameObjectsWithTag ("GameOverUI");
		fogUI = GameObject.FindGameObjectsWithTag ("FogUI");
		infoUI = GameObject.FindGameObjectsWithTag ("InfoUI");
		settingsUI = GameObject.FindGameObjectsWithTag ("SettingsUI");
		exitUI = GameObject.FindGameObjectsWithTag ("ExitUI");
		ShowUI (menuUI);
		HideUI (gameUI);
		HideUI (pauseUI);
		HideUI (gameOverUI);
		HideUI (fogUI);
		HideUI (infoUI);
		HideUI (settingsUI);
		HideUI (exitUI);
	}

	void Update() {
		if (unpausing) {
			unpausingTimer -= Time.deltaTime;
			if (lastUnpausingTimer != Mathf.Ceil (unpausingTimer) && Mathf.Ceil (unpausingTimer) != 0.0f) {
				MainCamera.GetComponent<UISound> ().CountDown ();
				lastUnpausingTimer = (int) Mathf.Ceil (unpausingTimer);
			}
			UnpauseText.GetComponent <Text> ().text = Mathf.Ceil (unpausingTimer).ToString();
			if (unpausingTimer <= 0.0f) {
				UnpauseText.GetComponent <Text> ().text = "";
				HideUI (fogUI);
				unpausingTimer = 3.0f;
				unpausing = false;
			}
		}

		print (RestartGOButton.GetComponentInChildren<Image> ().color);

		if (gameOvering) {
			gameOveringTimer -= Time.deltaTime;
			MenuGOButton.GetComponent<Button> ().interactable = false;
			RestartGOButton.GetComponent<Button> ().interactable = false;
			Color fadingColor = new Color (1, 1, 1, (gameOveringTimer>=1.0f?0.0f:Mathf.Clamp((1.0f-gameOveringTimer), 0.0f, 1.0f)));
			MenuGOButton.GetComponent<Image> ().color = fadingColor;
			MenuGOImage.color = fadingColor;
			RestartGOButton.GetComponent<Image> ().color = fadingColor;
			RestartGOImage.color = fadingColor;

			if (gameOverAdRequested && gameOveringTimer <= 1.0f) {
				AdManager.AskRequestInterstitial ();
				gameOverCount++;
				if (gameOverCount >= 3 && timeElapsed >= 90.0f) {
					AdManager.GameOver ();
					timeElapsed = 0.0f;
					gameOverCount = 0;
				}
				gameOverAdRequested = false;
			}

			if (gameOveringTimer <= 0.0f) {
				MenuGOButton.GetComponent<Button> ().interactable = true;
				RestartGOButton.GetComponent<Button> ().interactable = true;
				gameOveringTimer = 2.0f;
				gameOvering = false;
			}
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			ShowUI (exitUI);
		}

		timeElapsed += Time.deltaTime;
	}

	public void GameOver() { // Game -> GameOver
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Game) {
			HideUI (fogUI);
			HideUI (gameUI);
			ShowUI (gameOverUI);
			gameOvering = true;
			gameOverAdRequested = true;
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

	public void ShowInfo() {
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Menu) {
			ShowUI (infoUI);
		}
	}

	public void HideInfo() {
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Menu) {
			HideUI (infoUI);
		}
	}

	public void ShowSettings() {
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Menu) {
			ShowUI (settingsUI);
		}
	}

	public void HideSettings() {
		if (GameManager.manager.GetCurrentGameState () == GameManager.GameState.Menu) {
			HideUI (settingsUI);
		}
	}

	public void UpdateScore(int score) {
		string scoreString = "";
		if (score < 10) {
			scoreString = "000" + score;
		} else if (score < 100) {
			scoreString = "00" + score;
		} else if (score < 1000) {
			scoreString = "0" + score;
		} else {
			scoreString = "" + score;
		}
		ScoreText.GetComponent<Text> ().text = "<color=#ff0000ff>Score</color> " + scoreString;
	}

	public void UpdateHighestScore(int score) {
		string scoreString = "";
		if (score < 10) {
			scoreString = "000" + score;
		} else if (score < 100) {
			scoreString = "00" + score;
		} else if (score < 1000) {
			scoreString = "0" + score;
		} else {
			scoreString = "" + score;
		}
		HighestScoreText.GetComponent<Text> ().text = "<color=#ff0000ff>Highscore</color> " + scoreString;
	}

	public void UpdateFinalScore(int score, int best) {
		string finalScoreString = "final score\n";
		string scoreString = "";
		if (score < 10) {
			scoreString = "000" + score;
		} else if (score < 100) {
			scoreString = "00" + score;
		} else if (score < 1000) {
			scoreString = "0" + score;
		} else {
			scoreString = "" + score;
		}
		scoreString = "<color=red>" + scoreString + "</color>";
		finalScoreString += scoreString;
		if (score > best) {
			finalScoreString += "\n\n<color=yellow>Personal best !</color>";
		}
		ScoreGOText.GetComponent<Text> ().text = finalScoreString;
	}

	public void QuitGame() {
		Application.Quit ();
	}

	public void CancelQuitGame() {
		HideUI (exitUI);
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
