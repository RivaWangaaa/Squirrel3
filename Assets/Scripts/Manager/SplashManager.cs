using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashManager : MonoBehaviour
{
    [SerializeField] private Image image;

    [SerializeField] private Color colorWhite;
    [SerializeField] private Color colorBlack;

    [SerializeField] private float fadeSpeed;
    [SerializeField] private float fadeSlowSpeed;

    public static bool isfinished;

    public IEnumerator FadeOut(bool _isWhite, bool _isSlow)
    {
        Color t_Color = (_isWhite == true) ? colorWhite : colorBlack;
        t_Color.a = 0; 
    
        image.color = t_Color;

        while (t_Color.a < 1)
        {
            t_Color.a += (_isSlow == true) ? fadeSlowSpeed : fadeSpeed;
            image.color = t_Color;
            yield return null;
        }

        isfinished = true;
    }
    
    public IEnumerator FadeIn(bool _isWhite, bool _isSlow)
    {
        Color t_Color = (_isWhite == true) ? colorWhite : colorBlack;
        t_Color.a = 1; 
    
        image.color = t_Color;

        while (t_Color.a > 0)
        {
            t_Color.a -= (_isSlow == true) ? fadeSlowSpeed : fadeSpeed;
            image.color = t_Color;
            yield return null;
        }

        isfinished = true;
    }
}
