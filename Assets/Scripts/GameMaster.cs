
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public Vector3 lastCheckPointPos;

    //Don't destroy this objects data
    public GameObject acorn;
    
    //Score
    public Text currentScoreUI;
    private static int currentScore;
    public Text bestScoreUI;
    private int bestScore;
    private static int currentScoreCount;
    
    //Second floor
    public int thirdSectionActive;
    private static int sceneCount;
    public static List<string> saveAcorn = new List<string>();
    private GameObject temp;
    public GameObject floorTwoMessage;
    public GameObject player;

    //basement floor
    public GameObject elevatoropen;
    
    //giving key
    public GameObject keyType1;
    public GameObject keyType2;
    public GameObject keyType2by2Cylinder;
    public GameObject keyType2by2;
    public Camera camera;
  
    //Dead Ending
    [SerializeField] private Transform restartPos;
    [SerializeField] private GameObject dialogueTiggerdialogueTigger;
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
            PlayerPrefs.SetInt("thirdSection Active", 0);
            thirdSectionActive = PlayerPrefs.GetInt("thirdSection Active", 0);
        }

        else
        {
            thirdSectionActive = PlayerPrefs.GetInt("thirdSection Active", 0);
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
            
            player.GetComponent<MouseLook>().enabled = false;
            camera.GetComponent<MouseLook>().enabled = false;
            camera.GetComponent<LockMouse>().enabled = false;
            player.GetComponent<FirstPersonDrifter>().enabled = false;
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.localRotation = Quaternion.Euler(0, 0, 0);
            player.transform.localPosition = new Vector3(-60, 1, 14);
            player.GetComponent<CharacterController>().enabled = true;
            player.GetComponent<FirstPersonDrifter>().enabled = true;
            camera.GetComponent<MouseLook>().enabled = true;
            camera.GetComponent<LockMouse>().enabled = true;
            player.GetComponent<MouseLook>().enabled = true;
        
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
    }
    
    void Start()
    {

        bestScore = PlayerPrefs.GetInt("Best Score", 0);
        bestScoreUI.text = "Best : " + bestScore;
        
        if(thirdSectionActive == 2)
        {
            StartCoroutine(Player.DelayActivation(elevatoropen));
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
    private void DeadEnding()
    {
        //reset the scene counts
        thirdSectionActive = 0;
        
        //Move player to the starting point
        StartCoroutine(ResetPlayerPos(restartPos));
        
        //Remembered the acorns amounts
        bestScore = savingBestAmount;
        currentScore = savingAcornsAmount;
        
        //Set Active Acorns
        StartCoroutine(RestartAcorns());
        
        //Restart Dialogue
        dialogueTiggerdialogueTigger.SetActive(true);

        //Find the broken Sculptures and set active false
        
    }
    
    
    public void SaveAcornsAmounts()
    {
        savingBestAmount = bestScore;
        savingAcornsAmount = currentScore;
        restartAcornsSave = saveAcorn;
    }

    IEnumerator RestartAcorns()
    {
        saveAcorn = restartAcornsSave;
        
        for (int i = 0; i <= saveAcorn.Count; i++)
        {
            temp = GameObject.Find(saveAcorn[i]);
            Destroy(temp.gameObject);
        }
        
        yield return null;
    }

    IEnumerator ResetPlayerPos(Transform pos)
    {
        player.GetComponent<MouseLook>().enabled = false;
        camera.GetComponent<MouseLook>().enabled = false;
        camera.GetComponent<LockMouse>().enabled = false;
        player.GetComponent<FirstPersonDrifter>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.localRotation = pos.localRotation;
        player.transform.localPosition = pos.localPosition;
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<FirstPersonDrifter>().enabled = true;
        camera.GetComponent<MouseLook>().enabled = true;
        camera.GetComponent<LockMouse>().enabled = true;
        player.GetComponent<MouseLook>().enabled = true;

        yield return null;
    }
    
}
