using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacked : MonoBehaviour
{
    [SerializeField] private Rigidbody rigid;
    float mass = 3.0F; // defines the character mass
    Vector3 impact = Vector3.zero;
    private CharacterController character;

    private void Start()
    {
        //Rigidbody is not working so using character controller
        character = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // apply the impact force:
        if (impact.magnitude > 0.2F) character.Move(impact * Time.deltaTime);
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 5*Time.deltaTime);
    }

    // call this function to add an impact force:
    public void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        dir += Vector3.up;

        impact += dir.normalized * force / mass;
    }

    //check what is attacking
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Attacked!");
            //check attack direction 
            Vector3 reactVec = transform.position - other.transform.position;
            AddImpact(reactVec, 60f);
        }
    }
}
