using System;
using UnityEngine;
using GoogleMobileAds.Api;

// Example script showing how to invoke the Google Mobile Ads Unity plugin.
public class TestADs : MonoBehaviour
{
    //To fill your APP ID
    private String AppID = "";

    private InterstitialAd interstisialAd;

    private void Start()
    {
        
    }

    public void RequestInterstitialAD()
    {
        String interstisial_ID = "ca-app-pub-3940256099942544/1033173712";
        interstisialAd = new InterstitialAd(interstisial_ID);

        AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
        interstisialAd.LoadAd(adRequest);
    }

    public void DisplayInterstitialAD()
    {
        if(interstisialAd.IsLoaded())
        {
            interstisialAd.Show();
        }
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        DisplayInterstitialAD();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestInterstitialAD();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    public void HandleInterstitialAD(bool subscribe)
    {
        if (subscribe)
        {
            // Called when an ad request has successfully loaded.
            this.interstisialAd.OnAdLoaded += HandleOnAdLoaded;
            // Called when an ad request failed to load.
            this.interstisialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            // Called when an ad is shown.
            this.interstisialAd.OnAdOpening += HandleOnAdOpened;
            // Called when the ad is closed.
            this.interstisialAd.OnAdClosed += HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            this.interstisialAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        }
        else
        {
            // Called when an ad request has successfully loaded.
            this.interstisialAd.OnAdLoaded -= HandleOnAdLoaded;
            // Called when an ad request failed to load.
            this.interstisialAd.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
            // Called when an ad is shown.
            this.interstisialAd.OnAdOpening -= HandleOnAdOpened;
            // Called when the ad is closed.
            this.interstisialAd.OnAdClosed -= HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            this.interstisialAd.OnAdLeavingApplication -= HandleOnAdLeavingApplication;
        }
    }

    private void OnEnable()
    {
        HandleInterstitialAD(true);
    }

    private void OnDisable()
    {
        HandleInterstitialAD(false);
    }
}