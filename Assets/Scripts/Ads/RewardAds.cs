using UnityEngine;
using UnityEngine.Advertisements;

public class RewardAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string adroidAdUnitId;
    [SerializeField] private string iosAdUnitId;

    private string unitId;
    public bool IsLoaded { get; private set; }
    public bool IsShowing { get; private set; }

    public delegate void AdCompleteHandler(bool completed);
    public static event AdCompleteHandler OnAdComplete;

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
        if (Advertisement.isInitialized)
        {
            IsLoaded = false;
            Advertisement.Load(unitId, this);
        }
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId == unitId)
        {
            IsLoaded = true;
            Debug.Log("Rewarded Ad Loaded and ready");
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Rewarded ad failed to load: {error.ToString()} - {message}");
    }

    public void ShowRewardedAd()
    {
        if (IsLoaded)
        {
            IsShowing = true;
            Advertisement.Show(unitId, this);
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        IsShowing = false;
        OnAdComplete?.Invoke(false);
        Debug.LogError($"Rewarded ad show failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        IsShowing = false;
        bool completed = showCompletionState == UnityAdsShowCompletionState.COMPLETED;
        OnAdComplete?.Invoke(completed);
        LoadRewardedlAd();
    }
}