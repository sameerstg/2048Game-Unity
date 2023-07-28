using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    private string _adUnitIdBanner = "ca-app-pub-3940256099942544/6300978111";
    private string _adUnitIdIntersatial = "ca-app-pub-3940256099942544/1033173712";
//#if UNITY_EDITOR
//#elif  UNITY_ANDROID
//    private string _adUnitIdBanner = "ca-app-pub-4216476940239064/1042688850";
//    private string _adUnitIdIntersatial = "ca-app-pub-4216476940239064/2164198830";
//#endif
    BannerView _bannerView;
    public static AdManager _instance;
    private void Awake()
    {
        _instance = this;
    }
    public void Start()
    {
        
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.

            RequestBanner();
            LoadInterstitialAd();

        });
    }
    private void RequestBanner()
    {
        // These ad units are configured to always serve test ads.


        // Clean up banner ad before creating a new one.
        if (_bannerView != null)
        {
            _bannerView.Destroy();
        }

        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        _bannerView = new BannerView(_adUnitIdBanner, AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth), AdPosition.Bottom);

        // Register for ad events.
        //_bannerView.OnBannerAdLoaded += OnBannerAdLoaded;
        //_bannerView.OnBannerAdLoadFailed += OnBannerAdLoadFailed;

        AdRequest adRequest = new AdRequest();

        // Load a banner ad.
        _bannerView.LoadAd(adRequest);
    }

    private InterstitialAd interstitialAd;

    /// <summary>
    /// Loads the interstitial ad.
    /// </summary>
    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        InterstitialAd.Load(_adUnitIdIntersatial, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
                RegisterEventHandlers(ad);
            });
    }
    public bool ShowIntersatialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();

            return true;
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
            return false;
        }
    }
    //public void LoadAd()
    //{
    //    // create an instance of a banner view first.
    //    if (_bannerView == null)
    //    {
    //        CreateBannerView();
    //    }
    //    // create our request used to load the ad.
    //    var adRequest = new AdRequest();
    //    adRequest.Keywords.Add("unity-admob-sample");

    //    // send the request to load the ad.
    //    Debug.Log("Loading banner ad.");
    //    _bannerView.LoadAd(adRequest);
    //}
    //public void CreateBannerView()
    //{
    //    Debug.Log("Creating banner view");

    //    // If we already have a banner, destroy the old one.
    //    if (_bannerView != null)
    //    {
    //        DestroyAd();
    //    }

    //    // Create a 320x50 banner at top of the screen
    //    _bannerView = new BannerView(_adUnitIdBanner, AdSize.Banner, AdPosition.Top);
    //}
    /// <summary>
    /// listen to events the banner may raise.
    /// </summary>

    //}
    //public void DestroyAd()
    //{
    //    if (_bannerView != null)
    //    {
    //        Debug.Log("Destroying banner ad.");
    //        _bannerView.Destroy();
    //        _bannerView = null;
    //    }
    //}
   
    private void RegisterEventHandlers(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
            Grid._instance.auSource.Stop();
            Time.timeScale = 0;
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");

            Time.timeScale = 1;
            Grid._instance.auSource.Play();

            LoadInterstitialAd();

        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }
}
