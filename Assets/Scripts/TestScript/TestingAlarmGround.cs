using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingAlarmGround : MonoBehaviour
{
    private Laser[] theLasers;
    private SoundManager theSoundManager;
    
    // Start is called before the first frame update
    void Start()
    {
        theLasers = FindObjectsOfType<Laser>();
        theSoundManager = FindObjectOfType<SoundManager>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (theLasers != null)
            {
                for (int i = 0; i < theLasers.Length; i++)
                {
                    theLasers[i].isGround = false;
                    theLasers[i].isActivated = false;
                    theLasers[i].soundActivated = false;
                    theLasers[i].lr.enabled = false;
                }
            }
            theSoundManager.StopAllSE();
        }

    }
}
