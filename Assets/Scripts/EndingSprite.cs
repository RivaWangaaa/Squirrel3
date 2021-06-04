using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingSprite : MonoBehaviour
{
    private SpriteRenderer rend;
    private Sprite end1, end2, end3, end4;
    private bool sceneSwitch = true;
    public Animator transitiion;
    public float transitionTime = 1f;
    public float transitionTime2 = 0.8f;
    
    
    // Start is called before the first frame update
    void Start()
    {

        rend = GetComponent<SpriteRenderer>();
        end1 = Resources.Load<Sprite>("ending1");
        end2 = Resources.Load<Sprite>("ending2");
        end3 = Resources.Load<Sprite>("ending3");
        end4 = Resources.Load<Sprite>("ending4");

        rend.sprite = end1;
        
        // if (sceneSwitch)
        // {
        //     rend.sprite = intro1;
        // }
        
        // if(sceneSwitch == false)
        // {rend.sprite = basement;}

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (rend.sprite == end1)
                rend.sprite = end2;
            else if (rend.sprite == end2)
            {
                rend.sprite = end3;

            }
            else if (rend.sprite == end3)
            {
                NextSprite();

            }
                
            else
            {
                LoadNextLevel();
            }
        }
    }

    public void LoadNextLevel()
    {
        
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transitiion.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("Main Menu_wholeBuilding");

        //load

    }

    public void NextSprite()
    {
        StartCoroutine(SceneTransition());
    }

    IEnumerator SceneTransition()
    {
        transitiion.SetTrigger("Middle");

        yield return new WaitForSeconds(1.3f);
        rend.sprite = end4;

        
        
 
    }


}
