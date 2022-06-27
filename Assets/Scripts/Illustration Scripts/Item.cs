using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Item : MonoBehaviour
{
    public GameObject illustrationCanvas;

    public GameObject illustrationCamera;

    public GameObject player;


    //To show the cursor when open up UI
    public void EnableFPS(bool enable)
    {
        FirstPersonDrifter fpsScript = player.GetComponent<FirstPersonDrifter>();

        if (enable)
        {
            //Enable FPS script
            fpsScript.enabled = true;
        }
        else
        {
            //Disable FPS script
            fpsScript.enabled = false;
            //Unlock Mouse and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Item triggered");
            
            illustrationCanvas.SetActive(true);
            player.SetActive(false);
            illustrationCamera.SetActive(true);
            EnableFPS(false);
        }
    }
    
    //Click on Back button, back to game
    public void Back()
    {
        Debug.Log("Back to game");
        player.transform.position += Vector3.forward;
        EnableFPS(true);
        illustrationCanvas.SetActive(false);
        player.SetActive(true);
        illustrationCamera.SetActive(false);
    }
}
