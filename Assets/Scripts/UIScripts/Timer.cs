using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private bool isSecondTime;
    [SerializeField] private float timeDuration;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI firstMinute;
    [SerializeField] private TextMeshProUGUI secondMinute;
    [SerializeField] private TextMeshProUGUI separator;
    [SerializeField] private TextMeshProUGUI firstSecond;
    [SerializeField] private TextMeshProUGUI secondSecond;

    private Timer theTimer;
    private float timer;
    private float flashTimer;
    private float flashDuration = 1f;

    public static float timeLeft;
    public static bool isSceneChanging;
    public static bool isTimerIsOn;
    
    // Start is called before the first frame update
    void Start()
    {
        isTimerIsOn = true;
        theTimer = this.gameObject.GetComponent<Timer>();
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSceneChanging)
        {
            SaveLeftTime();

            if (timer > 5)
            {
                timer -= Time.deltaTime;
                UpdateTimerDisplay(timer);
            }
        
            else if (timer <= 5 && timer != 0)
            {
                timer -= Time.deltaTime;
                UpdateTimerDisplay(timer);
                Flash();
            }
            else
            {
                GameOver();
            }
        }
        
    }

    private void ResetTimer()
    {
        if (theTimer.isSecondTime)
        {
            theTimer.timer = timeLeft;
        }

        else
        {
            timer = timeDuration;
        }
    }

    private void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);
        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();
    }

    private void Flash()
    {

        if (timer < 0)
        {
            timer = 0;
            UpdateTimerDisplay(timer);
        }

        if (flashTimer <= 0)
        {
            flashTimer = flashDuration;
        }
        
        else if (flashTimer >= flashDuration/ 2)
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(false);
        }

        else
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(true);
            Debug.Log("2");

        }
    }

    private void SetTextDisplay(bool enabled)
    {
        firstMinute.enabled = enabled;
        secondMinute.enabled = enabled;
        separator.enabled = enabled;
        firstSecond.enabled = enabled;
        secondSecond.enabled = enabled;
    }

    private void GameOver()
    {
        if (timer != 0)
        {
            timer = 0;
            UpdateTimerDisplay(timer);
        }
        
        theTimer.gameOverPanel.SetActive(true);
        theTimer.gameObject.SetActive(false);
        Debug.Log("Game Over");
    }

    public void SaveLeftTime()
    {
        timeLeft = theTimer.timer;
    }

}
