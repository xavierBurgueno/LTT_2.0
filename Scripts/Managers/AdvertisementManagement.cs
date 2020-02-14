using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


/// <summary>
/// These are the Project Game ID's 
/// "video" = Video,Playable, Allowed to skipped
/// "rewardedVideo" = Video Playable, no skipping, must be rewarded
/// "banner" = Basic Banner Ad
/// 
/// Game Id's
/// Google Play: 3302154
/// Apple Store: 3302155
/// </summary>
public class AdvertisementManagement : MonoBehaviour
{

    public string myGameIdAndroid = "3302154";
    public string myGameIdIOS = "3302155";
    public string myVideoPlacenemt = "video";
    public string myRewardVideoPlacenemt = "rewardedVideo";
    public string myBannerPlacenemt = "banner";
    public bool adStarted;
    public bool adCompleted;
    
    private bool testMode = true;
    ShowOptions options = new ShowOptions();

    private void Start()
    {
#if UNITY_IOS
        Advertisement.Initialize(myGameIdIOS, testMode);
#elif UNITY_ANDROID
        Advertisement.Initialize(myGameIdAndroid, testMode);
#endif


    }

    private void Update()
    {
        if(Advertisement.isInitialized && Advertisement.IsReady(myVideoPlacenemt) && !adStarted)
        {
            //             Advertisement.Show(myVideoPlacenemt);
            //             adStarted = true;

            
            Advertisement.Show(myVideoPlacenemt);
            adStarted = true;
        }
    }

    public void BannerAd()
   {
        if(Advertisement.IsReady() && Advertisement.Banner.isLoaded)
        {
            Advertisement.Banner.Show();
        }
   }
   

    private void AdViewResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}
