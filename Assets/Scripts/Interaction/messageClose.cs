using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class messageClose : MonoBehaviour
{
    public GameObject message1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            message1.SetActive(false);
        }

    }
}
