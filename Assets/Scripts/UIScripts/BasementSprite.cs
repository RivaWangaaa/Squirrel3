using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasementSprite : MonoBehaviour
{
    private SpriteRenderer rend;
    Sprite basement;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        basement = Resources.Load<Sprite>("sce_basement");
        rend.sprite = basement;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (rend.sprite == basement)
            { rend.sprite = null; 
                SceneManager.LoadScene("Building1_Basement");
            }

        }
        
    }
}
