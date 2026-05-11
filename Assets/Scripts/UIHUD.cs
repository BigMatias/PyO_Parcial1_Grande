using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHUD : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameButton button;
    [SerializeField] private Timer timer;
    [SerializeField] private SaveData saveData;
    [SerializeField] private RewardedAdManager rewardedAdManager;
    [Header("Buttons: ")]
    [SerializeField] private Button creditsBtn;
    [SerializeField] private Button adsBtn;
    [Header("Text: ")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI pressButtonText;
    [SerializeField] private TextMeshProUGUI gameFinishedText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [Header("Credits Panel: ")]
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private Button creditsBackBtn;

    private void Awake()
    {
        button.onButtonClicked += Button_OnButtonClicked;
        button.onGameStarted += Button_OnGameStarted;
        
        saveData.onPlayerHighscoreChanged += SaveData_OnPlayerHighscoreChanged;
        timer.OnTimerEnded += Timer_OnTimerEnded;
        timer.OnGameReset += Timer_OnGameReset;
        
        creditsBtn.onClick.AddListener(OnCreditsBtnClicked);
        creditsBackBtn.onClick.AddListener(OnCreditsBackBtnClicked);
        adsBtn.onClick.AddListener(OnAdsBtnClicked);
    }

    private void Start()
    {
#if UNITY_IOS || UNITY_ANDROID
    adsBtn.gameObject.SetActive(true);
#else
    adsBtn.gameObject.SetActive(false);
#endif
    }

    private void Update()
    {
        timerText.text = timer.timer.ToString("0");
    }

    private void OnDestroy()
    {
        button.onButtonClicked -= Button_OnButtonClicked;
        button.onGameStarted -= Button_OnGameStarted;
        
        saveData.onPlayerHighscoreChanged -= SaveData_OnPlayerHighscoreChanged;
        timer.OnTimerEnded -= Timer_OnTimerEnded;
        timer.OnGameReset -= Timer_OnGameReset;
        
        creditsBtn.onClick.RemoveListener(OnCreditsBtnClicked);
        creditsBackBtn.onClick.RemoveListener(OnCreditsBackBtnClicked);
        adsBtn.onClick.RemoveListener(OnAdsBtnClicked);
    }
    
    private void Timer_OnGameReset()
    {
        gameFinishedText.gameObject.SetActive(false);
        pressButtonText.gameObject.SetActive(true);
        scoreText.text = "0";
    }
    
    private void SaveData_OnPlayerHighscoreChanged(int highScore)
    {
        highScoreText.text = highScore.ToString();
    }
    
    private void Timer_OnTimerEnded()
    {
        gameFinishedText.gameObject.SetActive(true);
    }
    
    private void Button_OnGameStarted()
    {
        pressButtonText.gameObject.SetActive(false);
    }
    
    private void OnCreditsBtnClicked()
    {
        creditsPanel.SetActive(true);
    }
    
    private void OnCreditsBackBtnClicked()
    {
        creditsPanel.SetActive(false);
    }
    
    private void OnAdsBtnClicked()
    {
        rewardedAdManager.ShowRewardedAd();
    }
    
    private void Button_OnButtonClicked(int currentScore)
    {
        scoreText.text = currentScore.ToString();
    }
}
