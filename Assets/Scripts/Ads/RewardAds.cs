using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string adroidAdUnitId;
    [SerializeField] private string iosAdUnitId;

    private string unitId;

    private void Awake()
    {
#if UNITY_ANDROID
        unitId = adroidAdUnitId;
#elif UNITY_IOS
        unitId = iosAdUnitId;
#elif UNITY_EDITOR
        unitId = adroidAdUnitId;
#endif
    }


    public void LoadRewardedlAd()
    {
        Advertisement.Load(unitId, this);

    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Interstital Ad Loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(unitId, this);
        LoadRewardedlAd();
    }

    #region ShowCallbacks
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
       if (placementId == unitId && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Ads fully watch");        }
    }
    #endregion
}

