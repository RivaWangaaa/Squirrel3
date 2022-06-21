using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    //[SerializeField] private Transform playerTargetPoint;
    [SerializeField] private Transform player;

    public LayerMask playerMask;
    
    //Attacking
    public float timeBetweenAttack;
    private bool alreadyAttack;
    public GameObject projectile;
    
    //damage temp
    //public float tempHealth;


    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    
 

    

    private void Update()
    {
        //playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        //playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
        
        AttackPlayer();

    }

    private void AttackPlayer()
    {
        //Make Sure enemy doesn't move
        transform.LookAt(player);
        
        if (!alreadyAttack)
        {
            //Attack Code
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
           
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            //takeDamage();

            alreadyAttack = true;
            Invoke(nameof(ResetAttack), timeBetweenAttack);
        }
    }

    private void ResetAttack()
    {
        alreadyAttack = false;
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
