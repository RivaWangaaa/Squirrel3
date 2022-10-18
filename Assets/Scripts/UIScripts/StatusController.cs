using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{

    [SerializeField] 
    public int hp;
    public int currentHp;

    private int hpRechargeTime;
    private int currentHpRechargeTime;

    //true or false for hp. If the hp is decreasing, hp won't recharge
    //It the bool is false, the hp will be recover
    private bool hpUsed;

    //images that what we need, currently there is only one
    //but I made it array so that I can add more in the future
    [SerializeField] 
    private Image[] images_Gauge;

    private const int HP = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //initializing
        currentHp = hp;
    }

    private void Update()
    {
        GaugeUpdate();
    }

    public void DecreaseHp(int _count)
    {
        hpUsed = true;
        currentHpRechargeTime = 0;
        
        if (currentHp - _count > 0)
        {
            currentHp -= _count;
        }
        
        else
        {
            currentHp = 0;
        }
    }

    private void GaugeUpdate()
    {
        images_Gauge[0].fillAmount = (float)currentHp / hp;
    }
}
