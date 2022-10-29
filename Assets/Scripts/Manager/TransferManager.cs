using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferManager : MonoBehaviour
{
    [SerializeField] private GameObject theUI;
    SplashManager theSplashManager;

    private void Start()
    {
        theSplashManager = FindObjectOfType<SplashManager>();
        StartCoroutine(Done());
    }

    public IEnumerator Transfer(string p_SceneName)
    {
        theUI.SetActive(false);
        SplashManager.isfinished = false;
        StartCoroutine(theSplashManager.FadeOut(false, true));
        yield return new WaitUntil(() => SplashManager.isfinished);
        SceneManager.LoadScene(p_SceneName);
    }
    
    public IEnumerator TransferbyNum(int _num)
    {
        theUI.SetActive(false);
        SplashManager.isfinished = false;
        StartCoroutine(theSplashManager.FadeOut(false, true));
        yield return new WaitUntil(() => SplashManager.isfinished);
        SceneManager.LoadScene(_num);
    }

    public IEnumerator Done()
    {
        SplashManager.isfinished = false;
        StartCoroutine(theSplashManager.FadeIn(false, true));
        yield return new WaitUntil(() => SplashManager.isfinished);
        
        theUI.SetActive(true);
    }
}
