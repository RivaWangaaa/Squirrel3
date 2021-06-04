using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecMessage : MonoBehaviour
{
    public GameObject Message2;
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
            Message2.SetActive(true);
            Debug.Log("충돌!");
            
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Message2.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
