using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class GPGSHandler : MonoBehaviour {

	private string lbID = "CgkI377R8oAUEAIQAQ";

	void Awake () {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ().Build ();
		PlayGamesPlatform.InitializeInstance (config);
		PlayGamesPlatform.Activate ();
		LogIn ();
	}

	public bool IsGPGSEnabled() {
		return PlayGamesPlatform.Instance.IsAuthenticated();
	}

	public void LogIn() {
		Social.localUser.Authenticate ((bool success) => {
			Debug.Log(success);
		});
	}

	public void ShowAchivements() {
		if (PlayGamesPlatform.Instance.IsAuthenticated()) {
			// TODO
		} else {
			LogIn ();
		}
	}

	public void ShowLeaderboards() {
		if (PlayGamesPlatform.Instance.IsAuthenticated()) {
			PlayGamesPlatform.Instance.ShowLeaderboardUI (lbID);
		} else {
			LogIn ();
		}
	}

	public void PostScore(int score) {
		if (PlayGamesPlatform.Instance.IsAuthenticated()) {
			Social.ReportScore(score, lbID, (bool success) => {
				// handle success or failure
			});
		}
	}
}
