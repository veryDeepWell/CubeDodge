using System.Collections.Generic;
using UnityEngine;

public class MainAnalyser : MonoBehaviour
{
    private FakeAnalytics.FakeAnalyticsSDK _analyticsSDK;
    private bool _isInitialized = false;

    void Start()
    {
        _analyticsSDK = new FakeAnalytics.FakeAnalyticsSDK();
    }

    // Инициализация аналитики
    public void InitializeAnalytics(string appKey, string userId)
    {
        if (_analyticsSDK != null)
        {
            _analyticsSDK.Initialize(appKey, userId);
            _isInitialized = true;
        }
    }

    // Отслеживание общего события
    public void TrackEvent(string eventName, Dictionary<string, string> parameters = null)
    {
        if (!_isInitialized)
        {
            Debug.LogWarning("Analytics not initialized. Call InitializeAnalytics first.");
            return;
        }

        _analyticsSDK.TrackEvent(eventName, parameters);
    }

    // Отслеживание старта игры
    public void TrackGameStart()
    {
        if (!_isInitialized)
        {
            Debug.LogWarning("Analytics not initialized. Call InitializeAnalytics first.");
            return;
        }

        _analyticsSDK.TrackGameStartEvent();
    }

    // Отслеживание события уровня
    public void TrackLevel(int level, Dictionary<string, string> parameters = null)
    {
        if (!_isInitialized)
        {
            Debug.LogWarning("Analytics not initialized. Call InitializeAnalytics first.");
            return;
        }

        _analyticsSDK.TrackLevelEvent(level, parameters);
    }

    // Принудительная отправка данных
    public void FlushAnalytics()
    {
        if (!_isInitialized)
        {
            Debug.LogWarning("Analytics not initialized. Call InitializeAnalytics first.");
            return;
        }

        _analyticsSDK.Flush();
    }

    // Проверка инициализации
    public bool IsAnalyticsInitialized()
    {
        return _isInitialized;
    }
}