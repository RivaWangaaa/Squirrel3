using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private SpriteRenderer rend;
    private Sprite main;
    private bool sceneSwitch = true;
    public Animator transitiion;
    public float transitionTime = 1f;
    
    
    // Start is called before the first frame update
    void Start()
    {

        rend = GetComponent<SpriteRenderer>();
        main = Resources.Load<Sprite>("main");

        
        rend.sprite = main;
        
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

                LoadNextLevel();

        }
    }

    public void LoadNextLevel()
    {
        
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transitiion.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

        //load

    }


}
