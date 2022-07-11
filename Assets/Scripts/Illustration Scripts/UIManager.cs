using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public UIManager UIManagerInstance;
    public static int NutNum = 100;
    public static int nutClickOnce = 6;



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

   
    
}
