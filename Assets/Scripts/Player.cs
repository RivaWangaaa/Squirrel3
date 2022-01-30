using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public  static bool isStop = false;

    private bool isPicked = false;
    private Transform currentTransform;
    


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKey(KeyCode.W))
        // {
        //     transform.Translate( transform.forward * Time.deltaTime * 5f, Space.World);
        // }
        // else if (Input.GetKey(KeyCode.S))
        // { 
        //     transform.Translate( transform.forward * -Time.deltaTime * 5f, Space.World);
        // }
        // if (Input.GetKey(KeyCode.A))
        // { 
        //     transform.Translate( transform.right * -Time.deltaTime * 5f, Space.World);
        // }
        // else if (Input.GetKey(KeyCode.D))
        // { 
        //     transform.Translate( transform.right * Time.deltaTime * 5f, Space.World);
        // }
        // if (Input.GetKey(KeyCode.R))
        // {
        //     
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // }



    }

    private void OnTriggerEnter(Collider other)     // 충돌된 오브젝트
    {
        if (other.CompareTag("scenechange"))
        {
            SceneManager.LoadScene("basement_intro");
        }
        if (other.CompareTag("Door"))
        {
            Debug.Log("충돌! : " + other.gameObject.tag);

            if (currentTransform == null)
            {
                Debug.Log("키를 들고 있지 않습니다");
            }

            // 해당 문과 열쇠가 맞다면
            else if (currentTransform.GetComponent<Key>().keyType == other.GetComponent<Door>().doorType)
            {
                Debug.Log("문이 열립니다");
                if (other.GetComponent<Door>().doorType == 1)
                {
                    SceneManager.LoadScene("Building_Floor2_sample2");
                    //transform.position = new Vector3(-8, 10, 9);     // 플레이어의 위치를 변경(해당 스테이지로 이동)
                }
                else if (other.GetComponent<Door>().doorType == 2)
                {
                    SceneManager.LoadScene("Building1_Basement");
                }
                else if (other.GetComponent<Door>().doorType == 3)
                {
                    
                }
                
                else if (other.GetComponent<Door>().doorType == 4)
                {
                    
                }
            }
            else
            {
                Debug.Log("열쇠가 맞지 않습니다.");
            }



        }
        if(other.CompareTag("KeyObj"))
        {
            //충돌한 물체를 감지, other는 충돌한 물체
            Debug.Log("충돌! : " + other.tag);

            if (isPicked == false)
            {
                isPicked = true;
                other.transform.parent = transform;
                other.transform.position = transform.position + (transform.forward * 1.3f);
                

                currentTransform = other.transform;
            }
            else
            {
                // 기존의 박스는 원래 포지션으로 이동
                currentTransform.parent = null;
                //if (currentTransform.CompareTag("cube1"))      // 파랑
                if(currentTransform.GetComponent<Key>().keyType == 1)
                {
                    currentTransform.localPosition = new Vector3(4, 0, 0);
                }

                else if(currentTransform.GetComponent<Key>().keyType == 2)  // 빨강
                {
                    currentTransform.position = new Vector3(-4, 0, 3);
                }
            
                else  if(currentTransform.GetComponent<Key>().keyType == 3) // 회색
                {
                    currentTransform.position = new Vector3(3, 0, 6);
                }
                
                else  if(currentTransform.GetComponent<Key>().keyType == 4) // 회색
                {
                    currentTransform.position = new Vector3(6, 5, 44);
                }

                // 새로운 박스를 다시 든다.
                other.transform.parent = transform;
                other.transform.position = transform.position + (transform.forward * 1.3f);
                
         

                // 현재 다시 충돌된 객체를 다시  넣어준다.
                currentTransform = other.transform;
            }
        }

    }
}
