
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public Vector3 lastCheckPointPos;

    
    //Score
    public Text currentScoreUI;
    private static int currentScore;
    public Text bestScoreUI;
    private int bestScore;
    private static int currentScoreCount;

    public void SetScore(int value)
    {
        currentScore = value;
        currentScoreUI.text = "Score : " + currentScore;
        PlayerPrefs.SetInt("Current Score", currentScore);
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            bestScoreUI.text = "Best : " + bestScore;
            PlayerPrefs.SetInt("Best Score", bestScore);
        }
    }

    public int GetScore()
    {
        return currentScore;
    }
    
     void Awake()
    {
        if (instance == null)
        {
            instance = this;
    //        DontDestroyOnLoad(instance);
        }
        else
        { 
            //    Destroy(gameObject);
        }
        

        if (currentScoreCount == 0)
        {
            currentScore = 0;
            currentScoreUI.text = "Score : " + currentScore;
            currentScoreCount++;
        }
        else
        {

            currentScore = PlayerPrefs.GetInt("Current Score", currentScore);
            currentScoreUI.text = "Score : " + currentScore;
        }

    }

    void Start()
    {
        bestScore = PlayerPrefs.GetInt("Best Score", 0);
        bestScoreUI.text = "Best : " + bestScore;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
