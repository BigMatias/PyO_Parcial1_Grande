using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId;
    public bool adLoaded { get; private set; }
    private BannerManager theBanner;

    void Start()
    {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif        
    }

    internal void Initialize(BannerManager banner)
    {
        theBanner = banner;
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        adLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Interstitial: Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
    }

    public void ShowInterstitial()
    {
        if (adLoaded)
        {
            theBanner.HideBanner();
            Advertisement.Show(_adUnitId, this);
        }
    }

    public void OnUnityAdsShowStart(string _adUnitId)
    {
        Debug.Log("mostrando interstitial");
    }

    public void OnUnityAdsShowClick(string _adUnitId) 
    {
        Debug.Log("clickearon el ad");
    }

    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) 
    {
        Debug.Log("terminó de ver el ad");

        theBanner.ShowBanner();

        //Cargo el siguiente ad
        adLoaded = false;
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
    }
}
