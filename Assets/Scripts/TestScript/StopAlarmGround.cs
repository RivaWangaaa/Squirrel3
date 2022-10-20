using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAlarmGround : MonoBehaviour
{
    private Laser[] theLasers;
    private SoundManager theSoundManager;

    public static bool isCountDownStart;
    
    // Start is called before the first frame update
    void Start()
    {
        theLasers = FindObjectsOfType<Laser>();
        theSoundManager = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isCountDownStart)
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
}
