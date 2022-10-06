using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public UIManager UIManagerInstance;
    public static int NutNum = 100;
    public static int nutClickOnce = 6;
    public static int SecondFloorItemCount = 0;
    public static string itemName = "none";
    public GameObject fallingVase;
    public GameObject itemExample;
    public GameObject player;

    public GameObject introImage2;


    void Awake()
    {
        if (UIManagerInstance == null)
        {
            DontDestroyOnLoad(gameObject);

            UIManagerInstance = this;
        }
        else if (UIManagerInstance != this)
        {
            Destroy(gameObject);
        }
    }

    public void StartSculptureBreak()
    { 
        fallingVase.transform.GetChild(0).gameObject.SetActive(false);
        fallingVase.transform.GetChild(1).gameObject.SetActive(true);
        
        itemExample.GetComponent<Item>().ReturnToGame();
    }

    public void FreezeControl()
    {
        itemExample.GetComponent<Item>().EnableFPS(false);
    }

    public void OpenControl()
    {
        itemExample.GetComponent<Item>().EnableFPS(true);
    }

    public void IntroLookAround()
    {
        introImage2.transform.localScale = new Vector3(1.5f, 1.5f, 0);
    }
    


}
