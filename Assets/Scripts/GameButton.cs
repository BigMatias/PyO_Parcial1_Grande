using System;
using UnityEngine;
using UnityEngine.UI;

public class GameButton : MonoBehaviour
{
    [SerializeField] private Timer timer;
    
    private Button button;
    public int currentScore { get; private set; }
    private bool gameOver;

    private bool gameStarted;

    public event Action onGameStarted;
    public event Action<int> onButtonClicked;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
        
        timer.OnGameReset += Timer_OnGameReset;
        timer.OnTimerEnded += Timer_OnTimerEnded;
    }

    private void Start()
    {
        Initialize();
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnButtonClicked);
    }
    
    private void Initialize()
    {
        currentScore = 0;
        gameStarted = false;
        gameOver = false;
    }
    
    private void Timer_OnGameReset()
    {
        Initialize();
    }
    
    private void Timer_OnTimerEnded()
    {
        gameOver = true;
    }
    
    private void OnButtonClicked()
    {
        if (!gameOver)
        {
            if (!gameStarted)
                onGameStarted?.Invoke();
            currentScore++;
            onButtonClicked?.Invoke(currentScore);
        }
    }
}
