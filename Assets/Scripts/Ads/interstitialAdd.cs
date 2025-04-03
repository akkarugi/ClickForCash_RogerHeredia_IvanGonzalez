using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitaldAdd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string adroidAdUnitId;
    [SerializeField] private string iosAdUnitId;

    private string unitId;
    public bool IsLoaded { get; private set; }

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

    public void LoadInterstitalAd()
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
            Debug.Log("Interstitial Ad Loaded and ready");
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Interstitial ad failed to load: {error.ToString()} - {message}");
    }

    public void ShowInterstitialAd()
    {
        if (IsLoaded)
        {
            Advertisement.Show(unitId, this);
        }
        else
        {
            Debug.Log("Interstitial ad not loaded yet, loading now...");
            LoadInterstitalAd();
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Interstitial ad show failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Interstitial Ad Completed");
        LoadInterstitalAd();
    }
}