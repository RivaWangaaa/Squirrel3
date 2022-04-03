using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Read : MonoBehaviour
{
    public GameObject memo;
    

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            memo.SetActive(enabled);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        memo.SetActive(false);
    }
}
