using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class ShowAds : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        AdsHandler.instance.RequestInterstitialAD();
        AdsHandler.instance.RequestRewardedVideoAD();
        //AdsHandler.instance.ShowInterstitialAD();
    }
    
}
