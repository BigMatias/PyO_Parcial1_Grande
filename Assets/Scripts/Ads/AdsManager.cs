using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private bool isTestMode = true;
    [SerializeField] private BannerManager banner;
    [SerializeField] private InterstitialManager interstitial;
    [SerializeField] private RewardedAdManager rewardedAd;

    private string _gameId;

    void Awake()
    {
#if UNITY_IOS
        _gameId = "6095550";
#elif UNITY_ANDROID
        _gameId = "6095551";
#elif UNITY_EDITOR
        _gameId = "6095551";
#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, isTestMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        banner.Show();
        interstitial.Initialize(banner);
        rewardedAd.Initialize(banner);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
