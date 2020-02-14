using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.Advertisements; //Banners ad here
using ShowResult = UnityEngine.Monetization.ShowResult; //explicit about what ShowResult to use since it the enum exist on both Advert API and Montiz API

 namespace GameServices
{
    public class AdService : MonoBehaviour, IService
    {
        private string iOSGameID = "3302155";
        private string androidGameID = "3302154";
        private bool testMode = true;

        //Placement ID's
        private string video_ad = "video";
        private string reward_video_ad = "rewardedVideo";
        private string banner_ad = "banner";
        private string ar_ad = "AdAR";

        private PlacementContent arContent;


        //Events for when a reward video is completed;

        public void Initialize()
        {
#if UNITY_IOS
            Monetization.Initialize(iOSGameID, testMode);
            Monetization.onPlacementContentReady += ContentReady;

            Advertisement.Initialize(iOSGameID, testMode);
#elif UNITY_ANDROID
            Advertisement.Initialize(androidGameID, testMode); // mostly for the banner
            Monetization.Initialize(androidGameID, testMode);
            Monetization.onPlacementContentReady += ContentReady;
#else
            Debug.LogWarning("The current platform doesn't support Unity Monetization");
#endif

        }


        public void DisplayNonRewardedAd()
        {
            if (!Monetization.IsReady(video_ad))
            {
                Debug.LogWarningFormat("Placement <{0}> not ready to display.", video_ad);
                return;
            }

            ShowAdPlacementContent adContent = Monetization.GetPlacementContent(video_ad) as ShowAdPlacementContent;

            if (adContent != null)
                adContent.Show();
            else
                Debug.LogWarning("Placement Content Return Null");

        }

        public void DisplayRewardedAd()
        {
            StartCoroutine(WaitAndDisplayRewardedAd());
        }

        public IEnumerator WaitAndDisplayRewardedAd()
        {
            yield return new WaitUntil(() => Monetization.IsReady(reward_video_ad));

            ShowAdPlacementContent adContent = Monetization.GetPlacementContent(reward_video_ad) as ShowAdPlacementContent;
            adContent.gamerSid = "[CALL BACK SERVER ID HURR]";

            ShowAdCallbacks callBackOptions = new ShowAdCallbacks();
            callBackOptions.finishCallback += RewardCallback;

            if (adContent != null)
                adContent.Show(callBackOptions);
            else
                Debug.LogWarning("Placement Content Returned a Null");
        }

        private void RewardCallback(ShowResult result)
        {
            switch(result)
            {
                case ShowResult.Finished:
                    Debug.Log("Rewarded");
                    break;
                case ShowResult.Skipped:
                    Debug.Log("You skipped, Bruv");
                    break;
                case ShowResult.Failed:
                    Debug.Log("Failed to display ad");
                    break;

            }
        }

        public void DisplayARContent()
        {
            ShowAdPlacementContent adContent = arContent as ShowAdPlacementContent;

            if (adContent != null)
                adContent.Show();
            else
                Debug.LogWarning("Ad Placement returned null");
        }

        private void ContentReady(object sender, PlacementContentReadyEventArgs e)
        {
            if(e.placementId == ar_ad)
            {
                arContent = e.placementContent;
                Debug.Log("Placement is ready to go");
            }
        }

        public void DisplayBannerContent()
        {
            
                StartCoroutine(WaitAndDisplayBanner());
            
        }
        
        public void HideBannerContent()
        {
            Advertisement.Banner.Hide();
        }
        private IEnumerator WaitAndDisplayBanner()
        {
            
            yield return new WaitUntil(() => Advertisement.IsReady(banner_ad));
            Advertisement.Banner.Show(banner_ad);
        }

    }

}