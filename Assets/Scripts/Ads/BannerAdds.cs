using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAdds : MonoBehaviour
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

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }

    public void LoadBannerAd()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = BannerLoaded,
            errorCallback = BannerLoadedError
        };

        Advertisement.Banner.Load(unitId, options);
    }


    public void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions
        {
            showCallback = BannerShown,
            clickCallback = BannerClicked,
            hideCallback = BannerHidden
        };
        Advertisement.Banner.Show(unitId, options);
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }
    private void BannerHidden()
    {

    }

    private void BannerClicked()
    {
  
    }

    private void BannerShown()
    {
       
    }

    private void BannerLoadedError(string message)
    {
        Debug.Log("Banner ad loaded error");
    }

    private void BannerLoaded()
    {
        Debug.Log("Banner ad loaded");
    }

}
