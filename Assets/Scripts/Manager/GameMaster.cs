﻿
using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public Vector3 lastCheckPointPos;

    //Don't destroy this objects data
    public GameObject acorn;
    
    //Data
    [Header("Data")] 
    [Space(10)] 
    [SerializeField] private SoundManager theSoundManager;
    
    //Score
    [Header("Scores")]
    [Space(10)]
    public Text currentScoreUI;
    private static int currentScore;
    public Text bestScoreUI;
    private int bestScore;
    private static int currentScoreCount;
    
    //Second floor
    [Header("Floor")]
    [Space(10)]
    public int thirdSectionActive;
    private static int sceneCount;
    public static List<string> saveAcorn = new List<string>();
    private GameObject temp;
    public GameObject floorTwoMessage;
    public GameObject player;

    //basement floor
    public GameObject elevatoropen;
    
    //giving key
    [Header("Key")]
    [Space(10)] 
    public GameObject keyType1;
    public GameObject keyType2;
    public GameObject keyType2by2Cylinder;
    public GameObject keyType2by2;
    public Camera camera;
  
    //Dead Ending
    [Header("Dead Ending")]
    [Space(10)] 
    [SerializeField] private Timer theTimer;
    [SerializeField] private Transform restartPos;
    [SerializeField] private GameObject dialogueTrigger;
    [SerializeField] private GameObject deadEndingIllustration;
    [SerializeField] private string firstFloorSCE;
    [SerializeField] private Transform playerDir;
    [SerializeField] private int illustDuration;

    private GameObject SculpTemp;
    private static bool isThisDeadEnding;
    private static bool isPause;
    public static bool isAcornSave;
    private static int savingAcornsAmount;
    private static int savingBestAmount;
    public static List<string> restartAcornsSave = new List<string>();

    
    void Awake()
    {
       // thirdSectionActive = PlayerPrefs.GetInt("thirdSectionActive",0);
        
        if (instance == null)
        {
            instance = this;
        }

        if (sceneCount == 0)
        {
            if (!Timer.isGameOver)
            {
                PlayerPrefs.SetInt("thirdSection Active", 0);
                thirdSectionActive = PlayerPrefs.GetInt("thirdSection Active", 0);
                Debug.Log("Player Preps get int 작동함");
            }
        }

        else
        {
            if (!Timer.isGameOver)
            {
                thirdSectionActive = PlayerPrefs.GetInt("thirdSection Active", 0);
                Debug.Log("Player Preps get int 작동함");

            }
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
        
        SetThirdSectionActive(GetThirdSectionActive() +1);

        if (thirdSectionActive == 3)
        {
            //transform player in front of elevator
            Debug.Log("sce number is 3");

            StartCoroutine(ResetPlayerPos(playerDir));
            
            //give a key to the player
            keyType2by2.SetActive(true);
            keyType2.SetActive(true);
            keyType2by2Cylinder.transform.parent = player.transform;
            keyType2by2Cylinder.transform.position = player.transform.position + (transform.forward * 0.9f);
            keyType1.SetActive(false);


            Debug.Log("thirdSectionActive" + thirdSectionActive);
            Debug.Log("Acorn name saved : " + saveAcorn.Count);
            
            //deactivate acorn
            for (int i = 0; i <= saveAcorn.Count; i++)
            {
                temp = GameObject.Find(saveAcorn[i]);
                Destroy(temp.gameObject);
                //temp.SetActive(false);
            }
            
            //show the message
            floorTwoMessage.SetActive(true);
        }
        
        else if (thirdSectionActive == 2)
        {
            Debug.Log("sce number is 2");
        }

        else
        {
            Debug.Log("sce number is 1 or 0");
            
            if (isThisDeadEnding)
            {
                if (Timer.isGameOver)
                {
                    Debug.Log("Dead Ending SCE Recognized");
                    //First Floor Dead Ending
                    StartCoroutine(DeadEndingReset());
                }
            }
        }
    }
    
    void Start()
    {
        Debug.Log("thirdSectionActive : " + thirdSectionActive);

        bestScore = PlayerPrefs.GetInt("Best Score", 0);
        bestScoreUI.text = "Best : " + bestScore;
        
        if(thirdSectionActive == 2)
        {
            StartCoroutine(Player.DelayActivation(elevatoropen));
        }

    }

    private void Update()
    {
        if (Timer.isGameOver)
        {
            if (!isThisDeadEnding)
            {
                StartCoroutine(DeadEndingSCEChange());
            }
        }
    }

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

    public void SetThirdSectionActive(int num)
    {
        thirdSectionActive = num;
        sceneCount++;
        PlayerPrefs.SetInt("thirdSection Active", thirdSectionActive);
    }

    public int GetThirdSectionActive()
    {
        return thirdSectionActive;
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void SetAcorn(string acornName)
    {
        saveAcorn.Add(acornName);
    }
    
    //Time Limit Game Ending Nutsy captured 
    public IEnumerator DeadEndingSCEChange()
    {
        //Dead Ending trigger on
        isThisDeadEnding = true;
        
        //reset the scene counts
        thirdSectionActive = 0;

        //Stop player and show dead ending illustration
        StartCoroutine(DeadEndingEvent());

        yield return null;
    }

    private IEnumerator DeadEndingReset()
    {
        Debug.Log("Dead Ending Reset");
        //Move player to the starting point
        if (restartPos != null)
        { 
            StartCoroutine(ResetPlayerPos(restartPos));
        }
        
        //acorns amount saving condition
        isAcornSave = false;

        //Remembered the acorns amounts
        currentScore = savingAcornsAmount;

        //Set Active Acorns
        StartCoroutine(RestartAcorns());
        ResetBrokenSculps();
        
        //Reset Timer
        if (theTimer.isSecondTime)
        {
            theTimer.isSecondTime = false;
            Debug.Log("thsi is second");
        }
        TimerReset();
        isPause = false;

        //Find the broken Sculptures and set active false

        //Restart Dialogue
        dialogueTrigger.SetActive(true);

        yield return null;
    }

    //timer Reset
    private void TimerReset()
    {
        Timer.isGameOver = false;
        //theTimer.ResetTimer();
    }
    
    //Show Dead Ending Event
    private IEnumerator DeadEndingEvent()
    {
        isPause = true;
        
        //Stop Player
        StopPlayer();
        
        //stop sounds
        theSoundManager.StopAllSE();
        
        //Reset the bestscore
        bestScore = savingBestAmount;

        //Show illustration
        deadEndingIllustration.SetActive(true);
        
        //WaitForSec
        yield return new WaitForSeconds(illustDuration);
        
        //Make Player Move
        PlayPlayer();

        //Move Player to the First Floor SCE
        DeadEndingFirstFloorMove();

        yield return null;
    }
    
    //Dead Ending : Transform player to the First Floor
    private void DeadEndingFirstFloorMove()
    {
        //Transfer to the new scene
        SceneManager.LoadScene(firstFloorSCE);
    }
    
    //Save Acorns Amounts in the auto saving point
    public void SaveAcornsAmounts()
    {
        savingBestAmount = bestScore;
        savingAcornsAmount = currentScore;
        restartAcornsSave = saveAcorn;
        isAcornSave = true;
    }

    //Acorns Save Data(destroy taken acorns)
    IEnumerator RestartAcorns()
    {
        saveAcorn = restartAcornsSave;

        if (saveAcorn != null)
        {
            for (int i = 0; i <= saveAcorn.Count; i++)
            {
                temp = GameObject.Find(saveAcorn[i]);
                Destroy(temp.gameObject);
            }
        }
        
        yield return null;
    }

    private void ResetBrokenSculps()
    {
        for (int i = 0; i < Timer.saveSculptures.Count; i++)
        {
            SculpTemp = GameObject.Find(Timer.saveSculptures[i]);
            SculpTemp.SetActive(false);
        }
        
    }
    
    //Put the player on the right place
    IEnumerator ResetPlayerPos(Transform pos)
    {
        StopPlayer();
        player.transform.localRotation = pos.localRotation;
        player.transform.localPosition = pos.localPosition;
        PlayPlayer();

        yield return null;
    }
    
    //Pasue Player
    private void StopPlayer()
    {
        player.GetComponent<MouseLook>().enabled = false;
        camera.GetComponent<MouseLook>().enabled = false;
        camera.GetComponent<LockMouse>().enabled = false;
        player.GetComponent<FirstPersonDrifter>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
    }

    private void PlayPlayer()
    {
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<FirstPersonDrifter>().enabled = true;
        camera.GetComponent<MouseLook>().enabled = true;
        camera.GetComponent<LockMouse>().enabled = true;
        player.GetComponent<MouseLook>().enabled = true;
    }
}
