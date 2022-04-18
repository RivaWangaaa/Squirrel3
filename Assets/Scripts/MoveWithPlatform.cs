using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class MoveWithPlatform : MonoBehaviour
{
    public GameObject player;
    public GameObject fan;
    private bool onFan = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.parent = fan.transform;
            onFan = true;
            Debug.Log("on the fan");
        }
    }

    private void Update()
    {
        if (onFan)
        {
            player.transform.rotation = Quaternion.Inverse(fan.transform.rotation);
        }
    }
}
    