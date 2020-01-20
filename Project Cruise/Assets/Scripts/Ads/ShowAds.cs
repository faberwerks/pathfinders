using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class ShowAds : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        if (GameData.Instance.interstitialAdsCounter == 0 && Application.internetReachability != NetworkReachability.NotReachable)
        {
            AdsHandler.instance.RequestInterstitialAD();
            AdsHandler.instance.RequestRewardedVideoAD();
        }
        //AdsHandler.instance.ShowInterstitialAD();
    }
    
}
