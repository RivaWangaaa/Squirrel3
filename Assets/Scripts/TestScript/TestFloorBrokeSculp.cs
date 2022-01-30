using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFloorBrokeSculp : MonoBehaviour
{
    
    private int numberCount = 0;
    private BreakSculp theBreakSculp;


    void Start()
    {
        theBreakSculp = FindObjectOfType<BreakSculp>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Sculpture"))
            theBreakSculp.BreakSculpture();
    }
}
