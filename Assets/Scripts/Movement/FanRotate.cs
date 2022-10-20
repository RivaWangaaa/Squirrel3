using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotate : MonoBehaviour
{
    public float radius;
    public GameObject centerObject;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (centerObject != null)
        {
            this.transform.RotateAround(centerObject.transform.position, Vector3.up, speed);
        }
        
    }
}
