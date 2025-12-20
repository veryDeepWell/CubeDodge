using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace SuperMobileAds
{
    public class SuperMobileAdsRewarded
    {
        [SerializeField] private bool debug = false;
        
        private bool _isInitialized;
        private string _adUnitId;
        private bool _isLoaded;
        private bool _isShowing;

        public Action onAdClicked;
        public Action onAdDismissed;
        public Action onAdLoaded;
        public Action onAdFailedToLoad;
        public Action onAdShown;
        public Action onAdRewarded;

        public void Initialize(string adUnitId)
        {
            if (debug)
            {
                Debug.Log("SuperMobileAdsRewarded Initialized");
            }

            _isInitialized = true;
            _adUnitId = adUnitId;
            _isShowing = false;
            _isLoaded = false;
        }

        public void Load()
        {
            if (!_isInitialized)
            {
                if (debug)
                {
                    Debug.Log("SuperMobileAdsRewarded is not initialized");
                }

                return;
            }

            LoadingBanner();
        }

        public void Show()
        {
            if (!_isInitialized)
            {
                if (debug)
                {
                    Debug.Log("SuperMobileAdsRewarded is not initialized");
                }

                return;
            }

            if (_isShowing)
            {
                if (debug) { Debug.Log("SuperMobileAdsRewarded is already showing"); }
                return;
            }

            if (!_isLoaded)
            {
                if (debug)
                {
                    Debug.Log("SuperMobileAdsRewarded is not loaded");
                }

                return;
            }

            ShowingBanner();

            if (debug)
            {
                Debug.Log("SuperMobileAdsRewarded Showed " + _adUnitId);
            }
        }

        private async void LoadingBanner()
        {
            await Task.Delay(UnityEngine.Random.Range(100, 5000));

            if (UnityEngine.Random.Range(0f, 6f) > 2f)
            {
                onAdLoaded?.Invoke();
                _isLoaded = true;
                if (debug)
                {
                    Debug.Log("SuperMobileAdsRewarded Loaded " + _adUnitId);
                }
            }
            else
            {
                onAdFailedToLoad?.Invoke();
                _isLoaded = false;
                if (debug)
                {
                    Debug.Log("SuperMobileAdsRewarded Failed to Load " + _adUnitId);
                }
            }
        }

        private async void ShowingBanner()
        {
            await Task.Delay(UnityEngine.Random.Range(100, 5000));

            onAdShown?.Invoke();
            _isShowing = true;
            if (debug)
            {
                Debug.Log("SuperMobileAdsRewarded Showed " + _adUnitId);
            }

            if (UnityEngine.Random.Range(0f, 6f) > 5f)
            {
                await Task.Delay(UnityEngine.Random.Range(3000, 5000));
                onAdClicked?.Invoke();
            }
            else
            if (UnityEngine.Random.Range(0f, 6f) > 3f)
            {
                await Task.Delay(UnityEngine.Random.Range(4000, 5000));
                onAdRewarded?.Invoke();
                _isShowing = false;
            }
            else
            {
                await Task.Delay(UnityEngine.Random.Range(500, 5000));
                onAdDismissed?.Invoke();
                _isShowing = false;
            }
        }
    }
}
