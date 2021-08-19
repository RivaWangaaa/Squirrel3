using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingAlarmGround : MonoBehaviour
{
    private Laser theLaser;
    private SoundManager theSoundManager;
    
    // Start is called before the first frame update
    void Start()
    {
        theLaser = FindObjectOfType<Laser>();
        theSoundManager = FindObjectOfType<SoundManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("트리거");
        if (other.CompareTag("Player"))
        {
            theLaser.soundActivated = false;
            theSoundManager.StopAllSE();
        }

    }
}
