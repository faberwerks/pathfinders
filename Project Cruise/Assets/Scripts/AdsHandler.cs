using System;
using UnityEngine;
using GoogleMobileAds.Api;

// Example script showing how to invoke the Google Mobile Ads Unity plugin.
public class AdsHandler : MonoBehaviour
{
    //To fill your APP ID
    private String AppID = "";

    private InterstitialAd interstitialAd;

    private void Start()
    {
        RequestInterstitialAD();
        DisplayInterstitialAD();
    }

    public void RequestInterstitialAD()
    {
        String interstitial_ID = "ca-app-pub-3940256099942544/1033173712";
        interstitialAd = new InterstitialAd(interstitial_ID);

        AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
        interstitialAd.LoadAd(adRequest);
        Debug.Log("Ads Loaded");
    }

    public void DisplayInterstitialAD()
    {
        if(interstitialAd.IsLoaded())
        {
            Debug.Log("Ads Appear");
            interstitialAd.Show();
        }else
        {
            RequestInterstitialAD();
            DisplayInterstitialAD();
        }
    }

    //public void HandleOnAdLoaded(object sender, EventArgs args)
    //{
    //    DisplayInterstitialAD();
    //}

    //public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    //{
    //    RequestInterstitialAD();
    //}

    //public void HandleOnAdOpened(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleAdOpened event received");
    //}

    //public void HandleOnAdClosed(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleAdClosed event received");
    //}

    //public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleAdLeavingApplication event received");
    //}

    //public void HandleInterstitialAD(bool subscribe)
    //{
    //    if (subscribe)
    //    {
    //        // Called when an ad request has successfully loaded.
    //        this.interstitialAd.OnAdLoaded += HandleOnAdLoaded;
    //        // Called when an ad request failed to load.
    //        this.interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
    //        // Called when an ad is shown.
    //        this.interstitialAd.OnAdOpening += HandleOnAdOpened;
    //        // Called when the ad is closed.
    //        this.interstitialAd.OnAdClosed += HandleOnAdClosed;
    //        // Called when the ad click caused the user to leave the application.
    //        this.interstitialAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;
    //    }
    //    else
    //    {
    //        // Called when an ad request has successfully loaded.
    //        this.interstitialAd.OnAdLoaded -= HandleOnAdLoaded;
    //        // Called when an ad request failed to load.
    //        this.interstitialAd.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
    //        // Called when an ad is shown.
    //        this.interstitialAd.OnAdOpening -= HandleOnAdOpened;
    //        // Called when the ad is closed.
    //        this.interstitialAd.OnAdClosed -= HandleOnAdClosed;
    //        // Called when the ad click caused the user to leave the application.
    //        this.interstitialAd.OnAdLeavingApplication -= HandleOnAdLeavingApplication;
    //    }
    //}

    //private void OnEnable()
    //{
    //    HandleInterstitialAD(true);
    //}

    //private void OnDisable()
    //{
    //    HandleInterstitialAD(false);
    //}
}