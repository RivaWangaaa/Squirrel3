using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_wholeBuilding : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
    
    public void StartGame()
    {
        LoadNextLevel();
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

        //load

    }
    
}

    




