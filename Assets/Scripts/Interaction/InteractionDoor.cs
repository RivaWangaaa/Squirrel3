using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDoor : MonoBehaviour
{
    [SerializeField] private InteractionDoor theInteractionDoor;
    [SerializeField] private string sceneName;
    [SerializeField] private int transferSceNum;

    [Header("TurnOnOff")] [Space(10)] [SerializeField]
    private bool isTurnOnOff;

    [SerializeField] private GameObject[] turnOn;
    [SerializeField] private GameObject[] turnOff;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            StartCoroutine(DoorInteract());
        }
    }


    private void TurnOnAndOff()
    {
        if (theInteractionDoor.turnOn != null)
        {
            for (int i = 0; i < theInteractionDoor.turnOn.Length; i++)
            {
                theInteractionDoor.turnOn[i].SetActive(true);
            }
        }

        if (theInteractionDoor.turnOff != null)
        {
            for (int i = 0; i < theInteractionDoor.turnOff.Length; i++)
            {
                theInteractionDoor.turnOff[i].SetActive(false);
            }
        }
    }

    private IEnumerator DoorInteract()
    {
        yield return new WaitForSeconds(0.4f);
        if (theInteractionDoor.isTurnOnOff)
        {
            TurnOnAndOff();
        }

        SoundManager.instance.StopAllSE();
        StartCoroutine(FindObjectOfType<TransferManager>().Transfer(theInteractionDoor.sceneName));
        SceneCountManager.sceneCounts = theInteractionDoor.transferSceNum;
    }
}

