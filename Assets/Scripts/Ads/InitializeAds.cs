using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string adroidGameId;
    [SerializeField] private string iosGameId;
    [SerializeField] private bool isTesting;

    private string gameId;
    public bool IsInitialized { get; private set; } = false;

    private void Awake()
    {
#if UNITY_ANDROID
        gameId = adroidGameId;
#elif UNITY_IOS
        gameId = iosGameId;
#elif UNITY_EDITOR
        gameId = adroidGameId;
#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, isTesting, this);
        }
    }

    public void OnInitializationComplete()
    {
        IsInitialized = true;
        Debug.Log("Ads initialized successfully");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Ads initialization failed: {error.ToString()} - {message}");
    }
}