using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFloorBrokeSculp : MonoBehaviour
{
    
    private int numberCount = 0;
    private BreakSculp theBreakSculp;
    [SerializeField] private GameObject[] sculps;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sculpture"))
        {
            if (sculps != null)
            {
                Debug.Log("sculps 비지않음");

                for (int i = 0; i < sculps.Length; i++)
                {
                    if (other.transform.name == sculps[i].transform.name)
                    {        
                        theBreakSculp = sculps[i].transform.GetComponent<BreakSculp>();
                        StartCoroutine(BreakSculpt(theBreakSculp, i));
                    }
                }
            }
        }
    }

    IEnumerator BreakSculpt(BreakSculp _temp, int _num)
    {
        _temp.BreakSculpture();
        
        if (_temp.isAlarmRing)
        {
            _temp.RingAlarm();
        }
        yield return null;
    }
}
