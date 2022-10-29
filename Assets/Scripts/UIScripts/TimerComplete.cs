using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TimerComplete : MonoBehaviour
{
    //Data
    [Header("Data")] 
    [Space(10)] 
    
    [SerializeField] private SoundManager theSoundManager;

    //Reward
    [Header("Reward")] 
    [Space(10)] 
    
    [SerializeField] private GameObject f2_key;
    [SerializeField] private GameObject openElevator;
    
    //Flowchart
    [field: Tooltip("Flowchart empty parent object in the basement scene")]
    [SerializeField] private GameObject basementFlowchartObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Timer.isComplete = true;
            theSoundManager.StopAllSE();
            Reward();
        }
    }

    private void Reward()
    {
        Debug.Log("Player is here!");
        f2_key.SetActive(true);
        openElevator.SetActive(true);

        //Yanxi: Trigger flowchart Basement1-2
        basementFlowchartObject.transform.GetChild(1).gameObject.SetActive(true);
    }
}
