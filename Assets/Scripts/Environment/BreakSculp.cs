using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakSculp : MonoBehaviour
{
    [SerializeField] private float destroyTime;

    [SerializeField] private BoxCollider col;

    //Game Object component
    [SerializeField] private GameObject go_Sculpture;
    [SerializeField] private GameObject go_BrokenPieces;

    public void BreakSculpture()
    {
        Break();
    }

    private void Break()
    {
        col.enabled = false;
        Destroy(go_Sculpture);
        
        go_BrokenPieces.SetActive(true);
        Destroy(go_BrokenPieces, destroyTime);
    }
    
}
