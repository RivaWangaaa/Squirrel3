using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class MoveWithPlatform : MonoBehaviour
{
    public GameObject player;
    public GameObject fan;
    public GameObject center;
    private float speed;
    private bool onFan=false;
    public float playerHeight;
    private Component fanComponent;


    private void Start()
    {
        speed = 2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("on the fan");
            player.transform.parent = fan.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("leave the fan");
            player.transform.parent = null;
        }
    }

    
    private void Update()
    {
        if (onFan)
        {
            
            
            /*
            Vector3 newPos = (player.transform.position - center.transform.position).normalized * 3f;
            newPos += center.transform.position;
            player.transform.position = newPos;
            newPos = Vector3.zero;
            player.transform.RotateAround(center.transform.position, Vector3.up, speed);
            Debug.Log("Triggered");
            */
        }
    }
    
}