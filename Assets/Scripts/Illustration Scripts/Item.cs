using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Item : MonoBehaviour
{
    public GameObject illustrationCanvas;

    public GameObject illustrationCamera;

    public GameObject player;

    private bool midEnterBool = false;

    private bool endEnterBool = false;

    private UIManager UIManagerInstance;
    
    public GameObject enterEnding;

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
        
        //if first enter this item & this item is a third illustration item, add 1 to SecondFloorItemCount
        if (!midEnterBool && gameObject.name.Contains("Mid"))
        {
            UIManager.SecondFloorItemCount += 1;
            midEnterBool = true;
            
            Debug.Log("SecondFloorItemCount = " + UIManager.SecondFloorItemCount);
        }
        
        if (!endEnterBool && gameObject.name.Contains("End"))
        {
            UIManager.SecondFloorItemCount += 1;
            endEnterBool = true;
           
            Debug.Log("SecondFloorItemCount = " + UIManager.SecondFloorItemCount);
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
        
        //if it's the third illustration + there are two items in the secondFloorItems list, 
        //trigger the flowchart of ask player if they have finished investigating
        if (UIManager.SecondFloorItemCount == 2)
        {
            Debug.Log("Trigger Ending");
            enterEnding.transform.GetChild(0).gameObject.SetActive(true);
        }
        
    }
    
}
