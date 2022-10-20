using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakSculp : MonoBehaviour
{
    [SerializeField] private BreakSculp theBreakSculpt;
    public bool isAlarmRing;

    [SerializeField] private float destroyTime;
    [SerializeField] private BoxCollider col;
    
    //the alarming sound
    [SerializeField] private string alarm_Sound;
    
    //Game Object component
    [SerializeField] private GameObject go_Sculpture;
    [SerializeField] private GameObject go_BrokenPieces;
    
    //remember broken sculptures name
    public static List<string> saveBrokenSculptures = new List<string>();

    public void BreakSculpture()
    {
        Break();
    }

    private void Break()
    {
        col.enabled = false;
        Destroy(theBreakSculpt.go_Sculpture);
        
        theBreakSculpt.go_BrokenPieces.SetActive(true);
        saveBrokenSculptures.Add(theBreakSculpt.name);
        Destroy(theBreakSculpt.go_BrokenPieces, destroyTime);
    }

    public void RingAlarm()
    {
        if (theBreakSculpt.isAlarmRing)
        {
            SoundManager.instance.PlaySE(theBreakSculpt.alarm_Sound);
        }
    }
    
}
