using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private GameButton button;
    
    public float timer { get; private set; }
    private bool gameStarted;
    
    private Coroutine gameReset;
    
    public event Action OnTimerEnded;
    public event Action OnGameReset;
    
    private void Awake()
    {
        button.onGameStarted += Button_onGameStarted;
    }

    void Start()
    {
        gameStarted = false;
        timer = 10;
    }
    
    void Update()
    {
        if (gameStarted)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            gameStarted = false;
            gameReset = StartCoroutine(GameReset());
            OnTimerEnded?.Invoke();
        }
    }

    private void OnDestroy()
    {
        button.onGameStarted -= Button_onGameStarted;
    }

    private void Button_onGameStarted()
    {
        gameStarted = true;
    }

    private IEnumerator GameReset()
    {
        yield return new WaitForSeconds(3);
        timer = 10;
        OnGameReset?.Invoke();
    }

    public void AddRewardTime()
    {
        timer += 2;
    }
    
}
