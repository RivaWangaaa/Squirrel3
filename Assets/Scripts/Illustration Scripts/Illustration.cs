using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Illustration : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    /*
    public void Hover()
    {
        Debug.Log("Hovering");
    }*/

    public void Click()
    {
        Debug.Log("Clicked");
        if (UIManager.NutNum >= UIManager.nutClickOnce || transform.GetChild(2).gameObject.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Not enough nuts");
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void EndClick()
    {
        Debug.Log("End Clicking");
        if (UIManager.NutNum >= UIManager.nutClickOnce || transform.GetChild(2).gameObject.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        
        
    }

    public void Collected()
    {
        if (UIManager.NutNum >= UIManager.nutClickOnce && transform.GetChild(2).gameObject.activeSelf != true)  
        {
            UIManager.NutNum -= UIManager.nutClickOnce;
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (transform.GetChild(2).gameObject.activeSelf)
        {
            Debug.Log("Clue collected");
        }
        else if (UIManager.NutNum <= UIManager.nutClickOnce)
        {
            Debug.Log("Nut not enough");
        }
        Debug.Log(UIManager.NutNum+"Nuts Left");
    }
}
