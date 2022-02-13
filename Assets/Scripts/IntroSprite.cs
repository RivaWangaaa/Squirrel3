using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSprite : MonoBehaviour
{
    private SpriteRenderer rend;
    private Sprite dia1, dia2, dia3, dia4, dia5, dia6, dia7, dia8, dia9, dia10, dia11, dia12, dia13, dia14, dia15, dia16;
    private bool sceneSwitch = true;
    public Animator transition;
    public float transitionTime = 1f;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        dia1 = Resources.Load<Sprite>("dia1");
        dia2 = Resources.Load<Sprite>("dia2");
        dia3 = Resources.Load<Sprite>("dia3");
        dia4 = Resources.Load<Sprite>("dia4");
        dia5 = Resources.Load<Sprite>("dia5");
        dia6 = Resources.Load<Sprite>("dia6");
        dia7 = Resources.Load<Sprite>("dia7");
        dia8 = Resources.Load<Sprite>("dia8");
        dia9 = Resources.Load<Sprite>("dia9");
        dia10 = Resources.Load<Sprite>("dia10");
        dia11 = Resources.Load<Sprite>("dia11");
        dia12 = Resources.Load<Sprite>("dia12");
        dia13 = Resources.Load<Sprite>("dia13");
        dia14 = Resources.Load<Sprite>("dia14");
        dia15 = Resources.Load<Sprite>("dia15");
        dia16 = Resources.Load<Sprite>("dia16");
        
        rend.sprite = dia1;
        
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
            if (rend.sprite == dia1)
                rend.sprite = dia2;
            else if (rend.sprite == dia2)
                rend.sprite = dia3;
            else if (rend.sprite == dia3)
                rend.sprite = dia4;
            else if (rend.sprite == dia4)
                rend.sprite = dia5;
            else if (rend.sprite == dia5)
                rend.sprite = dia6;
            else if (rend.sprite == dia6)
                rend.sprite = dia7;
            else if (rend.sprite == dia7)
                rend.sprite = dia8;
            else if (rend.sprite == dia8)
                rend.sprite = dia9;
            else if (rend.sprite == dia9)
                rend.sprite = dia10;
            else if (rend.sprite == dia10)
                rend.sprite = dia11;
            else if (rend.sprite == dia11)
                rend.sprite = dia12;
            else if (rend.sprite == dia12)
                rend.sprite = dia13;
            else if (rend.sprite == dia13)
                rend.sprite = dia14;
            else if (rend.sprite == dia14)
                rend.sprite = dia15;
            else if (rend.sprite == dia15)
                rend.sprite = dia16;
            else
            {
                LoadNextLevel();
            }
        }
    }
    
    public void LoadNextLevel()
    {
        
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
        

    }
}
