using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject illustration;

    public GameObject Camera;

    public GameObject Player;


    //To show the cursor when open up UI
    public void EnableFPS(bool enable)
    {
        FirstPersonDrifter fpsScript = Player.GetComponent<FirstPersonDrifter>();

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
            
            illustration.SetActive(true);
            Player.SetActive(false);
            Camera.SetActive(true);
            EnableFPS(false);
        }
    }
    
    //Click on Back button, back to game
    public void Back()
    {
        Debug.Log("Back to game");
        Player.transform.position += Vector3.forward;
        EnableFPS(true);
        illustration.SetActive(false);
        Player.SetActive(true);
        Camera.SetActive(false);
    }
}
