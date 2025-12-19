using UnityEngine;
using UnityEngine.Events;
using SuperMobileAds; // Подключаем SDK

public class AdvertiseManager : MonoBehaviour
{
    [Header("SuperMobileAds Settings")]
    public string interstitialAdUnitId = "interstitial_default";
    public string rewardedAdUnitId = "rewarded_default";
    
    [Header("Events")]
    public UnityEvent onAdClicked;
    public UnityEvent onAdDismissed;
    public UnityEvent onAdLoaded;
    public UnityEvent onAdFailedToLoad;
    public UnityEvent onAdShown;
    public UnityEvent onAdRewarded;
    
    // SDK объекты
    private SuperMobileAdsInterstitial _interstitialAd;
    private SuperMobileAdsRewarded _rewardedAd;
    
    private bool _isAdShowing = false;
    
    private void Start()
    {
        InitializeAds();
    }
    
    private void InitializeAds()
    {
        Debug.Log("Инициализация рекламы...");
        
        // Инициализируем Interstitial
        _interstitialAd = new SuperMobileAdsInterstitial();
        _interstitialAd.Initialize(interstitialAdUnitId);
        
        // Подписываемся на события Interstitial
        _interstitialAd.onAdClicked += () => onAdClicked?.Invoke();
        _interstitialAd.onAdDismissed += () => {
            _isAdShowing = false;
            onAdDismissed?.Invoke();
        };
        _interstitialAd.onAdLoaded += () => onAdLoaded?.Invoke();
        _interstitialAd.onAdFailedToLoad += () => onAdFailedToLoad?.Invoke();
        _interstitialAd.onAdShown += () => {
            _isAdShowing = true;
            onAdShown?.Invoke();
        };
        
        // Инициализируем Rewarded
        _rewardedAd = new SuperMobileAdsRewarded();
        _rewardedAd.Initialize(rewardedAdUnitId);
        
        // Подписываемся на события Rewarded
        _rewardedAd.onAdClicked += () => onAdClicked?.Invoke();
        _rewardedAd.onAdDismissed += () => {
            _isAdShowing = false;
            onAdDismissed?.Invoke();
        };
        _rewardedAd.onAdLoaded += () => onAdLoaded?.Invoke();
        _rewardedAd.onAdFailedToLoad += () => onAdFailedToLoad?.Invoke();
        _rewardedAd.onAdShown += () => {
            _isAdShowing = true;
            onAdShown?.Invoke();
        };
        _rewardedAd.onAdRewarded += () => onAdRewarded?.Invoke();
        
        // Предзагружаем рекламу
        LoadInterstitial();
        LoadRewarded();
    }
    
    // === PUBLIC METHODS ===
    
    public void LoadInterstitial()
    {
        if (_interstitialAd != null)
        {
            _interstitialAd.Load();
        }
    }
    
    public void ShowInterstitial()
    {
        if (_interstitialAd != null && !_isAdShowing)
        {
            _interstitialAd.Show();
        }
        else
        {
            Debug.Log("Реклама уже показывается или не инициализирована");
            onAdDismissed?.Invoke(); // Пропускаем рекламу если нельзя показать
        }
    }
    
    public void LoadRewarded()
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.Load();
        }
    }
    
    public void ShowRewarded()
    {
        if (_rewardedAd != null && !_isAdShowing)
        {
            _rewardedAd.Show();
        }
        else
        {
            Debug.Log("Rewarded реклама не готова");
        }
    }
    
    public bool IsAdShowing()
    {
        return _isAdShowing;
    }
    
    // Для тестов из инспектора
    [ContextMenu("Test Show Interstitial")]
    private void TestShowInterstitial()
    {
        ShowInterstitial();
    }
}