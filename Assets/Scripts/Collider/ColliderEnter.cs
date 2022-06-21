using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayingAnim
{
    public bool isInteractable;
    public InteractObj theInteractObj;
    public bool isBoolTrigger;
    public Animator anim;
    public string boolName;
    public bool isAnimTrueOrFalse;
    public bool isAnimExit;
}

[System.Serializable]
public class TurningObject
{
    public GameObject[] turnOnObj;
    public GameObject[] turnOffObj;
}

public class ColliderEnter : MonoBehaviour
{
    [SerializeField] private ColliderEnter theColliderEnter;
    [SerializeField] private bool isPlayingAnimator;
    [SerializeField] private bool isTurningObj;
    [SerializeField] private bool isMovingPlayer;
    [SerializeField] private GameObject ob_player;
    [SerializeField] private GameObject ob_parent;
    [SerializeField] private TurningObject theTurningObj;
    [SerializeField] private PlayingAnim thePlayingAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (theColliderEnter.isMovingPlayer)
        {
            if (other.CompareTag("Player"))
            {
                ob_player.transform.parent = ob_parent.transform;
            }
        }
        if (theColliderEnter.isPlayingAnimator)
        {
            if (theColliderEnter.thePlayingAnim.isInteractable)
            {
                if (other.CompareTag("Player"))
                {
                    if (!InteractObj.isActivate)
                    {
                        theColliderEnter.thePlayingAnim.theInteractObj.PlayAnim();

                    }
                }
            }

            if (theColliderEnter.thePlayingAnim.isBoolTrigger)
            {
                if (other.CompareTag("Player"))
                {
                    theColliderEnter.thePlayingAnim.anim.SetBool(theColliderEnter.thePlayingAnim.boolName,theColliderEnter.thePlayingAnim.isAnimTrueOrFalse);
                }
            }
        }

        if (theColliderEnter.isTurningObj)
        {
            if (other.CompareTag("Player"))
            {
                if (theColliderEnter.isTurningObj)
                {
                    if (theColliderEnter.theTurningObj.turnOnObj != null)
                    {
                        for (int i = 0; i < theColliderEnter.theTurningObj.turnOnObj.Length; i++)
                        {
                            theColliderEnter.theTurningObj.turnOnObj[i].SetActive(true);
                            Debug.Log("Obj turn on");

                        }                    
                    }

                    if (theColliderEnter.theTurningObj.turnOffObj != null)
                    {
                        for (int i = 0; i < theColliderEnter.theTurningObj.turnOffObj.Length; i++)
                        {
                            theColliderEnter.theTurningObj.turnOffObj[i].SetActive(false);

                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (theColliderEnter.isPlayingAnimator)
        {
            if (theColliderEnter.thePlayingAnim.isInteractable)
            {
                if (InteractObj.isActivate)
                {
                    theColliderEnter.thePlayingAnim.theInteractObj.StopAnimation();
                    theColliderEnter.thePlayingAnim.theInteractObj.SetComplete();
                }
            }

            if (theColliderEnter.thePlayingAnim.isAnimExit)
            {
                if (theColliderEnter.thePlayingAnim.isBoolTrigger)
                {
                    if (other.CompareTag("Player"))
                    {
                        theColliderEnter.thePlayingAnim.anim.SetBool(theColliderEnter.thePlayingAnim.boolName,!theColliderEnter.thePlayingAnim.isAnimTrueOrFalse);
                    }
                }
            }
        }
        
        
        
        if(theColliderEnter.isMovingPlayer)
            if (other.CompareTag("Player"))
            {
                ob_player.transform.parent = null;
            }
        
    }
}
