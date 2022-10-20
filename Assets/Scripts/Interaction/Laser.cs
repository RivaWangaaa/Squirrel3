using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private StatusController theStatusController;
    public bool isActivated = true;
    
    public bool soundActivated = false;
    public bool isGround;


    [SerializeField] private int damage;
    [SerializeField] private Transform startPoint;

    //the alarming sound
    [SerializeField] 
    private string Alarm_Sound;

    public LineRenderer lr;

    private void Start()
    {
        theStatusController = FindObjectOfType<StatusController>();
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (isGround)
        {
            if (isActivated)
            {
                if (!lr.enabled)
                    lr.enabled = true;
            }
            if (lr.enabled)
            {
                lr.SetPosition(0, startPoint.position);
        
                if (isActivated) 
                    Damage();

                if (soundActivated)
                    SoundManager.instance.PlaySE(Alarm_Sound);
            }
        }
    }

    private void Damage()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }

            if (hit.transform.tag == "Player")
            {
                lr.SetPosition(1, hit.point);
                theStatusController.DecreaseHp(damage);
                soundActivated = true;
                lr.enabled = !lr.enabled;
                isActivated = false;
            }
        }
        else
        {
            //don't go the laser forever. When this hit -transform.right * 5000 stop it
            lr.SetPosition(1, transform.up * 5000);

        }
    }
    
}
