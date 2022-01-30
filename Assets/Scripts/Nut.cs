using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nut : MonoBehaviour
{
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
            UIManager.NutNum += 1;
            Debug.Log("Nut collected." + "Nuts: " + UIManager.NutNum);
            this.gameObject.SetActive(false);
        }
    }
    
}
