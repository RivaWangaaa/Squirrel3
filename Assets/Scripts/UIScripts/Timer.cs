using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public bool isSecondTime;
    [SerializeField] private GameMaster theGameMaster;
    [SerializeField] private float timeDuration;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI firstMinute;
    [SerializeField] private TextMeshProUGUI secondMinute;
    [SerializeField] private TextMeshProUGUI separator;
    [SerializeField] private TextMeshProUGUI firstSecond;
    [SerializeField] private TextMeshProUGUI secondSecond;
    [SerializeField] private GameObject[] countDownTurnOff;

    private Timer theTimer;
    private float timer;
    private float flashTimer;
    private float flashDuration = 1f;

    public static float timeLeft;
    public static bool isSceneChanging;
    public static bool isTimerIsOn;
    public static bool isGameOver;
    public static bool isComplete;
    public static List<string> saveSculptures = new List<string>();


    private void Awake()
    {
        theTimer = this.gameObject.GetComponent<Timer>();
        isTimerIsOn = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
        CountDownTurnOff();
        if (!GameMaster.isAcornSave)
        {
            theGameMaster.SaveAcornsAmounts();
            saveSculptures = BreakSculp.saveBrokenSculptures;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isComplete)
        {
            if (!isSceneChanging)
            {
                StopAlarmGround.isCountDownStart = true;
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

        else
        {
            CompleteCountDown();
        }
    }

    public void ResetTimer()
    {

        if (theTimer.isSecondTime)
        {
            Debug.Log("Time Left : " + timeLeft);
            theTimer.timer = timeLeft;
            UpdateTimerDisplay(theTimer.timer);
            isSceneChanging = false;
        }

        else
        {
            timeLeft = 0;
            isSceneChanging = false;
            theTimer.timer = theTimer.timeDuration;
            isSceneChanging = false;
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
    
    //Turn Off objects while turn off
    private void CountDownTurnOff()
    {
        for (int i = 0; i < countDownTurnOff.Length; i++)
        {
            countDownTurnOff[i].SetActive(false);
        }
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
            StopAlarmGround.isCountDownStart = false;
            timer = 0;
            UpdateTimerDisplay(timer);
        }

        if (theTimer.gameOverPanel != null)
        {
            theTimer.gameOverPanel.SetActive(true);
        }
        
        theTimer.gameObject.SetActive(false);
        isGameOver = true;
    }

    public void SaveLeftTime()
    {
        timeLeft = theTimer.timer;
    }

    private void CompleteCountDown()
    {
        StopAlarmGround.isCountDownStart = false;
        ResetTimer();
        isGameOver = false;
        gameObject.SetActive(false);
    }

}
