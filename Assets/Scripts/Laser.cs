using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private StatusController theStatusController;
    
    private LineRenderer lr;
    
    [SerializeField] 
    private Transform startPoint;

    //the alarming sound
    [SerializeField] 
    private string Alarm_Sound;
    
    private void Start()
    {
        theStatusController = FindObjectOfType<StatusController>();
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lr.SetPosition(0, startPoint.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
                
            }

            if (hit.transform.tag == "Player")
            {
                SoundManager.instance.PlaySE(Alarm_Sound);
                theStatusController.DecreaseHp(1);
                //Destroy(hit.transform.gameObject);
            }
        }
        else
        {
            //don't go the laser forever. When this hit -transform.right * 5000 stop it
            lr.SetPosition(1, transform.up * 5000);
        }
    }
}
