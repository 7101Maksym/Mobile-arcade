using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidGameId = "6080938"; // Замените на свой ID
    [SerializeField] string _adUnitId = "Rewarded_Android"; // Имя блока из Dashboard
    [SerializeField] bool _testMode = true;

    private void Start()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        // 1. Инициализация SDK
        Advertisement.Initialize(_androidGameId, _testMode, this);
    }

    public void LoadAd()
    {
        // 2. Загрузка рекламы (вызывайте заранее, например, при старте уровня)
        Debug.Log("Загрузка рекламы: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd()
    {
        // 3. Показ рекламы (вызывайте по нажатию кнопки)
        Advertisement.Show(_adUnitId, this);
    }

    // --- Обработка результатов (Интерфейсы) ---

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Реклама просмотрена! Выдаем награду.");
            // ЗДЕСЬ ВАШ КОД: Начислите монеты или жизни игроку
        }
        else if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.SKIPPED))
        {
            Debug.Log("Реклама пропущена. Награда не выдается.");
        }
        else if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.UNKNOWN))
        {
            Debug.LogError("Реклама не показалась.");
        }
    }

    // Обязательные методы интерфейсов (можно оставить пустыми или для логов)
    public void OnInitializationComplete() { Debug.Log("Unity Ads Initialized"); LoadAd(); }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message) { }
    public void OnUnityAdsAdLoaded(string adUnitId) { Debug.Log("Реклама готова к показу"); }
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message) { }
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message) { }
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
}
