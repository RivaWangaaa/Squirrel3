using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCountManager : MonoBehaviour
{
    public static int sceneCounts = 1;
    public static SceneCountManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
