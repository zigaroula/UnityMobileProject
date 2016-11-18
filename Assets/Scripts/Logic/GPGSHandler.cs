using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class GPGSHandler : MonoBehaviour {

	private string lbID = "CgkI377R8oAUEAIQAQ";
	private string achievement_10_games = "CgkI377R8oAUEAIQBg";
	private string achievement_100_games = "CgkI377R8oAUEAIQBw";
	private string achievement_200_games = "CgkI377R8oAUEAIQCA";
	private string achievement_500_points = "CgkI377R8oAUEAIQAg";
	private string achievement_1000_points = "CgkI377R8oAUEAIQAw";
	private string achievement_5000_points = "CgkI377R8oAUEAIQBA";
	private string achievement_9000_points = "CgkI377R8oAUEAIQBQ";

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

	public void ShowAchievements() {
		if (PlayGamesPlatform.Instance.IsAuthenticated()) {
			Social.ShowAchievementsUI ();
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

	public void UnlockScoreAchievements(int score) {
		if (score >= 500) {
			Social.ReportProgress (achievement_500_points, 100.0f, (bool success) => {});
		}
		if (score >= 1000) {
			Social.ReportProgress (achievement_1000_points, 100.0f, (bool success) => {});
		}
		if (score >= 5000) {
			Social.ReportProgress (achievement_5000_points, 100.0f, (bool success) => {});
		}
		if (score >= 9000) {
			Social.ReportProgress (achievement_9000_points, 100.0f, (bool success) => {});
		}
	}

	public void IncrementPlayCount() {
		PlayGamesPlatform.Instance.IncrementAchievement (achievement_10_games, 1, (bool success) => {});
		PlayGamesPlatform.Instance.IncrementAchievement (achievement_100_games, 1, (bool success) => {});
		PlayGamesPlatform.Instance.IncrementAchievement (achievement_200_games, 1, (bool success) => {});
	}
}
