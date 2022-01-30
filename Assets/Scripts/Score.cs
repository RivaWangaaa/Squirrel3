using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int numberCount = 0;

    void Start()
    {

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

             if (this.CompareTag("Score-"))
             {
                 sm.SetScore(sm.GetScore() - 1);
                 Destroy(this.gameObject);
             }
             
             else if (this.CompareTag("Score--"))
             {
                 if (numberCount == 0)
                 {
                     sm.SetScore(sm.GetScore() - 1);
                     numberCount++;
                 }

             }
             else
             {
                 sm.SetScore(sm.GetScore() + 1);
                 Destroy(this.gameObject);
             }

        

        }
    }
    private void OnTriggerExit(Collider other)
    {
        numberCount = 0;
    }
}

