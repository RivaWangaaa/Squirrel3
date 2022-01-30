using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class thirdKey : MonoBehaviour
{
    public GameObject doorclosed;

    public GameObject doorOpen;
    public GameObject message1;
    public GameObject messageClose;
   
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
            doorOpen.SetActive(true);
            doorclosed.SetActive(false);
            message1.SetActive(true);
            messageClose.SetActive(true);
            Debug.Log("충돌!");
            Destroy(this.gameObject);
            
        }
        
    }
}
