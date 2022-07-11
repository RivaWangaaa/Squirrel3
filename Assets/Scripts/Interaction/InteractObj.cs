using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class PlayAnimation
{
    public bool isPlayAnim;
    public string playAnimName;
    public Animator anim;
}

[System.Serializable]
public class TurningObj
{
    public bool isTurningObj;
    public GameObject[] turnOnObj;
    public GameObject[] turnOffObh;
}

[System.Serializable]
public class TurningScript
{
    public bool isTurningScript;
    public string scriptName;
}

public class InteractObj : MonoBehaviour
{
    [SerializeField] private bool isAuto;

    [SerializeField] private InteractObj theInteractObj;
    [SerializeField] private PlayAnimation thePlayAnimation;
    
    public static bool isActivate;
    private float animationLength;

    void Update()
    {
        if (theInteractObj.isAuto)
        {
            if (theInteractObj.thePlayAnimation.isPlayAnim)
            {
                Debug.Log("anima Turned On");

                if (isActivate)
                {
                    if (theInteractObj.isAnimtorDone())
                    {
                        StartCoroutine(Complete());
                    }
                }
        
                else if (!isActivate)
                {
                    theInteractObj.thePlayAnimation.anim.SetBool(theInteractObj.thePlayAnimation.playAnimName, false);
                    PlayAnim();
                }
            }
        }
    }

    private bool isAnimtorDone()
    {
        return theInteractObj.thePlayAnimation.anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

    public void CompleteAnim()
    {
        StartCoroutine(Complete());
    }

    private IEnumerator Complete()
    {
        theInteractObj.thePlayAnimation.anim.SetBool(theInteractObj.thePlayAnimation.playAnimName, false);
        yield return new WaitForSeconds(0.8f);
        Debug.Log("anima done");
        isActivate = false;
        yield return null;
    }

    public void StopAnimation()
    {
        theInteractObj.thePlayAnimation.anim.SetBool(theInteractObj.thePlayAnimation.playAnimName, false);
        isActivate = false;
    }
    

    public void SetComplete()
    {
        StartCoroutine(Complete());
    }

    public void PlayAnim()
    {
        Debug.Log("Play anim");

        isActivate = true;
        theInteractObj.thePlayAnimation.anim.SetBool(theInteractObj.thePlayAnimation.playAnimName, true);
        
        if (theInteractObj.isAnimtorDone())
        {
            StartCoroutine(Complete());
        }
    }

}
