using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Illustration : MonoBehaviour
{
    public int nutCollected;

    private int nutClickOnce=1;
    // Start is called before the first frame update
    void OnEnable()
    {
        nutCollected = UIManager.NutNum;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void Hover()
    {
        Debug.Log("Hovering");
    }

    public void Click()
    {
        Debug.Log("Clicked");
        if (UIManager.NutNum > 0)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Not enough nuts");
            this.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void EndClick()
    {
        Debug.Log("End Clicking");
        if (UIManager.NutNum > 0)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            this.transform.GetChild(1).gameObject.SetActive(false);
        }
        
        
    }

    public void Collected()
    {
        if (UIManager.NutNum > 0)
        {
            UIManager.NutNum -= nutClickOnce;
        }
        else
        {
            Debug.Log("Nut = 0");
        }
        Debug.Log(UIManager.NutNum+"Nuts Left");
    }
}
