using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    public static bool isStop = false;

    public bool isPicked = true;
    private Transform currentTransform;


    //door objects
    public GameObject elevatorClosed;
    public GameObject floorTwoClosed;
    public GameObject floorTwoOpen;

    public GameObject floorThreeClosed;
    public GameObject floorThreeOpen;
    public GameObject keyTypeFour;
    public GameObject FloorThreeMessage;
    
    //scene change to basement
    public GameObject floorTwoMessage;
    public GameObject keyTypeTwo;
    public GameObject keyTypeTwo2;
    public GameObject keyTypeOne;
    
    
    //transition
    public Animator transition;
    public float transitionTime = 1f;

    //Timer
    private Timer isTimer;
    
    //Flowcharts
    public GameObject beforeSecondFloorFlowchartObject;
    public GameObject secondFloorFlowchartObject;
    [field: Tooltip("Flowchart empty parent object in the basement scene")]
    public GameObject basementFlowchartObject;
    


    private void OnTriggerEnter(Collider other)     // 충돌된 오브젝트
    {
        if (other.CompareTag("scenechange"))
        {
            StartCoroutine(changescene());
        }
        
        if (other.CompareTag("Ending"))
        {
            StartCoroutine(LoadLevel(4));

        }
        
        if (other.CompareTag("Door"))
        {
            Debug.Log("충돌! : " + other.gameObject.tag);

            if (currentTransform == null)
            {
                Debug.Log("키를 들고 있지 않습니다//You don't have key");
            }

            // 해당 문과 열쇠가 맞다면
            else if (currentTransform.GetComponent<Key>().keyType == other.GetComponent<Door>().doorType)
            {
                //open the Floor1 elevator
                Debug.Log("Door is open");
                if (other.GetComponent<Door>().doorType == 1)
                {
                    elevatorClosed.SetActive(false);
                }
                
                else if (other.GetComponent<Door>().doorType == 2)
                {
                    LoadPrevLevel();
                }
                
                else if (other.GetComponent<Door>().doorType == 3)
                {
                    floorTwoClosed.SetActive(false);
                    floorTwoOpen.SetActive(true);
                    Destroy(keyTypeTwo.gameObject);
                    Destroy(keyTypeTwo2);
                }
                
                else if (other.GetComponent<Door>().doorType == 4)
                {
                    floorThreeClosed.SetActive(false);
                    floorThreeOpen.SetActive(true);
                    Destroy(keyTypeFour.gameObject);
                }
            }
            else
            {
                Debug.Log("Key is not right.");
            }
            



        }
        if(other.CompareTag("KeyObj"))
        {
            //충돌한 물체를 감지, other는 충돌한 물체
            Debug.Log("충돌! : " + other.tag);

                isPicked = true;
                other.transform.parent = transform;
                other.transform.position = transform.position + (transform.forward * 0.9f);
                

                currentTransform = other.transform;

                // // 기존의 박스는 원래 포지션으로 이동
                // currentTransform.parent = null;
                // //if (currentTransform.CompareTag("cube1"))      // 파랑
                // if(currentTransform.GetComponent<Key>().keyType == 1)
                // {
                //     currentTransform.localPosition = new Vector3(4, 0, 0);
                // }
                //
                // else if(currentTransform.GetComponent<Key>().keyType == 2)  // 빨강
                // {
                //     currentTransform.position = new Vector3(-3, 0, 3);
                // }
                //
                  if(currentTransform.GetComponent<Key>().keyType == 4) // 회색
                {
                    FloorThreeMessage.SetActive(true);
                }
                //
                // else  if(currentTransform.GetComponent<Key>().keyType == 4) // 회색
                // {
                //     currentTransform.position = new Vector3(6, 5, 44);
                // }
                //
                // // 새로운 박스를 다시 든다.
                // other.transform.parent = transform;
                // other.transform.position = transform.position + (transform.forward * 1.3f);
                //
                //
                //
                // // 현재 다시 충돌된 객체를 다시 넣어준다.
                // currentTransform = other.transform;

        }

        //Trigger routine dialogues
        if (other.CompareTag("Stair1-1"))
        {
            if (UIManager.basement1_2 && UIManager.floor2_1 == false)
            {
                beforeSecondFloorFlowchartObject.transform.GetChild(0).gameObject.SetActive(true);
                UIManager.stair1_1 = true;
            }
        }
        
        if (other.CompareTag("Floor2-2"))
        {
            if (UIManager.floor2_1)
            {
                secondFloorFlowchartObject.transform.GetChild(1).gameObject.SetActive(true);
                UIManager.floor2_2 = true;
            }
        }
        
        if (other.CompareTag("Basement1-1"))
        {
            basementFlowchartObject.transform.GetChild(0).gameObject.SetActive(true);
        }

    }
    public void LoadNextLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    
    public void LoadPrevLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
    }
    
    

    
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
        
    }
    
    
    IEnumerator changescene()
    {
        yield return new WaitForSeconds(transitionTime);
        elevatorClosed.SetActive(true);
        LoadNextLevel();

        //SceneManager.LoadScene("Basement Texture 1");
        
        //Timer
        if (Timer.isTimerIsOn)
        {
            Timer.isSceneChanging = true;
            Timer.isTimerIsOn = false;
        }
    }

    public static IEnumerator DelayActivation(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        obj.SetActive(false);
        //SceneManager.LoadScene("Basement Texture 1");
    }
    

    // public void PlayAudio()
    // {
    //
    //     StartCoroutine(Audio());
    // }
    // IEnumerator Audio()
    // {
    //     AudioSource audio = GetComponent<AudioSource>();
    //     
    //     yield return new WaitForSeconds(20f);
    //     audio.Play();
    //     yield return new WaitForSeconds(audio.clip.length);
    //     audio.clip = otherClip;
    //
    // }
}
