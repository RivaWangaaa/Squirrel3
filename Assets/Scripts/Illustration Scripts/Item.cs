using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Item : MonoBehaviour
{
    public GameObject illustrationCanvas;

    public GameObject illustrationCamera;

    public GameObject player;

    private bool midEnterBool3 = false;

    private bool endEnterBool3 = false;

    private UIManager UIManagerInstance;
    
    public GameObject enterEnding;

    public GameObject enterSculptureBreak;


    //To show the cursor when open up UI
    public void EnableFPS(bool enable)
    {
        FirstPersonDrifter fpsScript = player.GetComponent<FirstPersonDrifter>();

        if (enable)
        {
            //Enable FPS script
            fpsScript.enabled = true;
            player.transform.GetChild(0).GetComponent<LockMouse>().enabled = true;
            player.GetComponent<MouseLook>().sensitivityX = 7;
            player.transform.GetChild(0).GetComponent<MouseLook>().sensitivityY = 8;
        }
        else
        {
            //Disable FPS script
            fpsScript.enabled = false;
            //Unlock Mouse and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            player.transform.GetChild(0).GetComponent<LockMouse>().enabled = false;
            player.GetComponent<MouseLook>().sensitivityX = 0;
            player.transform.GetChild(0).GetComponent<MouseLook>().sensitivityY = 0;
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

        //Save this item's name to UIManager script so that it doesn't get changed when 'back' button
        //calls the specific item script
        UIManager.itemName = name;
        
        //if first enter this item & this item is a third illustration item, add 1 to SecondFloorItemCount
        if (!midEnterBool3 && gameObject.name.Contains("3Mid"))
        {
            UIManager.SecondFloorItemCount += 1;
            midEnterBool3 = true;
            
            Debug.Log("SecondFloorItemCount = " + UIManager.SecondFloorItemCount);
        }
        
        if (!endEnterBool3 && gameObject.name.Contains("3End"))
        {
            UIManager.SecondFloorItemCount += 1;
            endEnterBool3 = true;
           
            Debug.Log("SecondFloorItemCount = " + UIManager.SecondFloorItemCount);
        }

    }
    
    //Click on Back button, back to game
    public void Back()
    {

        ReturnToGame();
        
        //When the player clicks the 'back' button of the end item of the 1st illustration,
        //ask them if they have finished investigating the 1st illustration by triggering the according flowchart
        if (UIManager.itemName.Contains("1End"))
        {
            Debug.Log("Ask if end exploring to trigger sculpture animation.");
            enterSculptureBreak.transform.GetChild(0).gameObject.SetActive(true);
            EnableFPS(false);
        }
        
        Debug.Log(UIManager.itemName);
            
            //if it's the third illustration + there are two items in the secondFloorItems list, 
        //trigger the flowchart of ask player if they have finished investigating
        if (UIManager.SecondFloorItemCount == 2)
        {
            Debug.Log("Trigger Ending");
            enterEnding.transform.GetChild(0).gameObject.SetActive(true);
            EnableFPS(false);
        }

    }
    
    private void ReturnToGame()
    {
        Debug.Log("Back to game");
        player.transform.position += Vector3.forward;
        EnableFPS(true);
        illustrationCanvas.SetActive(false);
        player.SetActive(true);
        illustrationCamera.SetActive(false);

        enterSculptureBreak.transform.GetChild(0).gameObject.SetActive(false);
    }
    
}
