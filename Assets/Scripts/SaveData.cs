using System;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InterstitialManager interstitialManager;
    [SerializeField] private GameButton gameButton;
    [SerializeField] private Timer timer;
    
    static string playerScore_key = "playerHighScore";
    private int playerHighScore;
    
    public event Action<int> onPlayerHighscoreChanged; 

    private void Awake()
    {
        timer.OnTimerEnded += Timer_OnTimerEnded;
    }

    void Start()
    {
        if (PlayerPrefs.HasKey(playerScore_key))
            playerHighScore = PlayerPrefs.GetInt(playerScore_key);
        else
            playerHighScore = 0;
        onPlayerHighscoreChanged?.Invoke(playerHighScore);
    }

    private void OnDestroy()
    {
        timer.OnTimerEnded -= Timer_OnTimerEnded;
    }

    private void Timer_OnTimerEnded()
    {
        if (gameButton.currentScore > playerHighScore)
        {
            UpdateHighScore(gameButton.currentScore);
            onPlayerHighscoreChanged?.Invoke(playerHighScore);
        }
        else
        {
           interstitialManager.ShowInterstitial(); 
        }
    }
    
    private void UpdateHighScore(int value)
    {
        playerHighScore = value;

        PlayerPrefs.SetInt(playerScore_key, playerHighScore);
        PlayerPrefs.Save();
    }

    private void ResetearAvance()
    {
        PlayerPrefs.DeleteAll();
    }
}