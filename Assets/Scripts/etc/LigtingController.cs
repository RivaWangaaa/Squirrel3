using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigtingController : MonoBehaviour
{
    [SerializeField] private LigtingController theLigtingController;
    [SerializeField] private bool isLigtingReset;
    [SerializeField] private Color lightingColor;

    void Start()
    {
        if (!theLigtingController.isLigtingReset)
        {
            Debug.Log("Color is changing");
            RenderSettings.ambientLight = lightingColor;
        }

        else
        {
            RenderSettings.ambientLight = Color.white;
        }
    }
}
