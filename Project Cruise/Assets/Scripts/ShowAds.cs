using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class ShowAds : MonoBehaviour
{

    private string AppID = "ca-app-pub-7623091422700152~9683332999";
    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(AppID);
        AdsHandler.instance.RequestInterstitialAD();
        //AdsHandler.instance.ShowInterstitialAD();
    }
    
}
