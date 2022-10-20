using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDoor : MonoBehaviour
{
    [SerializeField] private InteractionDoor theInteractionDoor; 
    [SerializeField] private string sceneName;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            StartCoroutine(FindObjectOfType<TransferManager>().Transfer(theInteractionDoor.sceneName));
        }
    }
}
