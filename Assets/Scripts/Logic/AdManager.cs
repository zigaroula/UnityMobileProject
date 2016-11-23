using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour {

	private static BannerView bannerView;
	private static InterstitialAd interstitial;

	private static bool toBeRequested;

	// Use this for initialization
	void Start () {
		RequestBanner ();
		RequestInterstitial ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private static void RequestBanner () {
		#if UNITY_EDITOR
			string adUnitId = "unused";
		#elif UNITY_ANDROID
			string adUnitId = "ca-app-pub-4200796796829111/3054609384";
		#elif UNITY_IPHONE
			string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
		#else
			string adUnitId = "unexpected_platform";
		#endif

		bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

		//AdRequest request = new AdRequest.Builder().SetBirthday(new DateTime(1985, 1, 1)).TagForChildDirectedTreatment(true).Build();
		AdRequest request = new AdRequest.Builder().TagForChildDirectedTreatment(true).AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("1BD92B34EEB8D67D57C8BCC2DD766240").Build();

		bannerView.LoadAd(request);
	}

	private static void RequestInterstitial() {
		#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-4200796796829111/2217004581";
		#elif UNITY_IPHONE
		string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		interstitial = new InterstitialAd(adUnitId);

		//AdRequest request = new AdRequest.Builder().SetBirthday(new DateTime(1985, 1, 1)).TagForChildDirectedTreatment(true).Build();
		AdRequest request = new AdRequest.Builder().TagForChildDirectedTreatment(true).AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("1BD92B34EEB8D67D57C8BCC2DD766240").Build();

		interstitial.LoadAd(request);
		toBeRequested = false;
	}

	public static void ShowBanner() {
		bannerView.Show ();
	}

	public static void HideBanner() {
		bannerView.Hide ();
	}

	public static void AskRequestInterstitial() {
		if (toBeRequested) {
			RequestInterstitial ();
		}
	}

	public static void GameOver() {
		if (interstitial.IsLoaded()) {
			interstitial.Show();
			toBeRequested = true;
		}
	}
}
