using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class GPGSHandler : MonoBehaviour {

	public GameObject LoadingScreen;

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
			LoadingScreen.SetActive(false);
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
			PlayGamesPlatform.Instance.ShowLeaderboardUI (GPGSIds.leaderboard_score_ranking);
		} else {
			LogIn ();
		}
	}

	public void PostScore(int score) {
		if (PlayGamesPlatform.Instance.IsAuthenticated()) {
			Social.ReportScore(score, GPGSIds.leaderboard_score_ranking, (bool success) => {
				// handle success or failure
			});
		}
	}

	public void UnlockScoreAchievements(int score) {
		if (score >= 1000) {
			Social.ReportProgress (GPGSIds.achievement_a_good_start, 100.0f, (bool success) => {});
		}
		if (score >= 2000) {
			Social.ReportProgress (GPGSIds.achievement_almost_an_ace, 100.0f, (bool success) => {});
		}
		if (score >= 5000) {
			Social.ReportProgress (GPGSIds.achievement_a_true_ace, 100.0f, (bool success) => {});
		}
		if (score >= 9000) {
			Social.ReportProgress (GPGSIds.achievement_its_over_nine_thousand, 100.0f, (bool success) => {});
		}
		if (score >= 15000) {
			Social.ReportProgress (GPGSIds.achievement_the_lord_of_the_space, 100.0f, (bool success) => {});
		}
	}

	public void IncrementPlayCount() {
		PlayGamesPlatform.Instance.IncrementAchievement (GPGSIds.achievement_space_rookie, 1, (bool success) => {});
		PlayGamesPlatform.Instance.IncrementAchievement (GPGSIds.achievement_space_intermediate, 1, (bool success) => {});
		PlayGamesPlatform.Instance.IncrementAchievement (GPGSIds.achievement_space_expert, 1, (bool success) => {});
		PlayGamesPlatform.Instance.IncrementAchievement (GPGSIds.achievement_space_master, 1, (bool success) => {});
		PlayGamesPlatform.Instance.IncrementAchievement (GPGSIds.achievement_space_allstar, 1, (bool success) => {});
	}
}
