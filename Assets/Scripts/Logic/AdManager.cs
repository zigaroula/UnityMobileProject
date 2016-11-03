using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour {

	private static BannerView bannerView;

	// Use this for initialization
	void Start () {
		RequestBanner ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void RequestBanner () {
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

		//AdRequest request = new AdRequest.Builder().Build();
		AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("3787c9310d84e029").Build(); //FIXME

		bannerView.LoadAd(request);
	}

	public static void ShowBanner() {
		bannerView.Show ();
	}

	public static void HideBanner() {
		bannerView.Hide ();
	}
}
