using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour

{ 
    private int numberCount = 0;
    private BreakSculp theBreakSculp;


    void Start()
    {
        theBreakSculp = FindObjectOfType<BreakSculp>();
    }

    void Update()
    {

    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            GameObject smObject = GameObject.Find("GameMaster");
            GameMaster sm = smObject.GetComponent<GameMaster>();


            if (CompareTag("Score-"))
            {
                sm.SetScore(sm.GetScore() - 1);

                if (sm.thirdSectionActive == 1)
                {
                    sm.SetAcorn(gameObject.name);
                }
                
                else if (sm.thirdSectionActive == 3)
                {
                    sm.SetAcorn(gameObject.name);
                }
                
                Destroy(gameObject);
            }
             
            else if (CompareTag("Score--"))
            {
                if (numberCount == 0)
                {
                    sm.SetScore(sm.GetScore() - 1);
                    numberCount++;
                    theBreakSculp.BreakSculpture();
                }

            }
            else
            {
                sm.SetScore(sm.GetScore() + 1);

                if (sm.thirdSectionActive == 1)
                {
                    sm.SetAcorn(gameObject.name);
                }
                
                Destroy(gameObject);
            }

        

        }
    }
    private void OnTriggerExit(Collider other)
    {
        numberCount = 0;
    }
}

