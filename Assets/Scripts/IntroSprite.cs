using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSprite : MonoBehaviour
{
    private SpriteRenderer rend;
    private Sprite intro1, intro2, intro3, intro4, intro5, intro6, intro7, sce1, sce2, sce3;
    private bool sceneSwitch = true;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        intro1 = Resources.Load<Sprite>("sce_intro");
        intro2 = Resources.Load<Sprite>("sce_intro2");
        intro3 = Resources.Load<Sprite>("sce_intro3");
        intro4 = Resources.Load<Sprite>("sce_intro4");
        intro5 = Resources.Load<Sprite>("sce_intro5");
        intro6 = Resources.Load<Sprite>("sce_intro6");
        intro7 = Resources.Load<Sprite>("sce_intro7");
        sce1 = Resources.Load<Sprite>("sce1");
        sce2 = Resources.Load<Sprite>("sce2");
        sce3 = Resources.Load<Sprite>("sce3");
        
        rend.sprite = intro1;
        
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
            if (rend.sprite == intro1)
                rend.sprite = intro2;
            else if (rend.sprite == intro2)
                rend.sprite = intro3;
            else if (rend.sprite == intro3)
                rend.sprite = intro4;
            else if (rend.sprite == intro4)
                rend.sprite = intro5;
            else if (rend.sprite == intro5)
                rend.sprite = intro6;
            else if (rend.sprite == intro6)
                rend.sprite = intro7;
            else if (rend.sprite == intro7)
                rend.sprite = sce1;
            else if (rend.sprite == sce1)
                rend.sprite = sce2;
            else if (rend.sprite == sce2)
                rend.sprite = sce3;
            else
            {
                SceneManager.LoadScene("Building_Floor1~3");
            }
        }
        
    }
}
