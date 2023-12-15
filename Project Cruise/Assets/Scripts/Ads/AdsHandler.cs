﻿using System;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Example script showing how to invoke the Google Mobile Ads Unity plugin.
public class AdsHandler : MonoBehaviour
{

    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardedVideo;
    public static AdsHandler instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        RequestInterstitialAD();
    }

    public void RequestInterstitialAD()
    {
        //string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        string adUnitId = "ca-app-pub-7623091422700152/2521661296";
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
        
    }

    //public void RequestRewardedVideoAD()
    //{
    //    string adUnitId = "ca-app-pub-3940256099942544/5224354917";
    //    this.rewardedVideo = RewardBasedVideoAd.Instance;

    //    // Called when an ad request has successfully loaded.
    //    this.rewardedVideo.OnAdLoaded += HandleOnVideoAdLoaded;
    //    // Called when an ad request failed to load.
    //    this.rewardedVideo.OnAdFailedToLoad += HandleOnVideoAdFailedToLoad;
    //    // Called when an ad is shown.
    //    this.rewardedVideo.OnAdOpening += HandleOnVideoAdOpened;
    //    // Called when the ad is closed.
    //    this.rewardedVideo.OnAdClosed += HandleOnVideoAdClosed;
    //    // Called when the ad click caused the user to leave the application.
    //    this.rewardedVideo.OnAdLeavingApplication += HandleOnVideoAdLeavingApplication;

    //    // Create an empty ad request.
    //    AdRequest request = new AdRequest.Builder().AddTestDevice("2B14A1568BC5F0CD5FE89A0505F28B39").Build();
    //    // Load the interstitial with the request.
    //    this.rewardedVideo.LoadAd(request,adUnitId);

    //}

    //Interstitial Event
    #region
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        //ShowInterstitialAD();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestInterstitialAD();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        RequestInterstitialAD();
        this.interstitial.Destroy();
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }
    #endregion

    ////Video Event
    //#region
    //public void HandleOnVideoAdLoaded(object sender, EventArgs args)
    //{
    //    //ShowInterstitialAD();
    //}

    //public void HandleOnVideoAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    //{
    //        RequestRewardedVideoAD();
    //}

    //public void HandleOnVideoAdOpened(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleAdOpened event received");
    //}

    //public void HandleOnVideoAdClosed(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleAdClosed event received");
    //    //Double coins here
    //}

    //public void HandleOnVideoAdLeavingApplication(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleAdLeavingApplication event received");
    //}
    //#endregion

    //public void ShowVideoAD()
    //{
    //    if (this.rewardedVideo.IsLoaded())
    //    {
    //        this.rewardedVideo.Show();
    //    }
    //    else
    //    {
    //        Debug.Log("Video Ad is not loaded.");
    //    }
    //}

    public void ShowInterstitialAD()
    {
        if (this.interstitial.IsLoaded() && GameData.Instance.interstitialAdsCounter < 1)
        {
            this.interstitial.Show();
            GameData.Instance.interstitialAdsCounter = 3;
        }
        else
        {
            Debug.Log("Interstitial Ad is not loaded.");
        }
    }
}