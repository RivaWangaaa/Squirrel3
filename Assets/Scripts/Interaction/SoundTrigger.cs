using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField] private SoundTrigger theSoundTrigger;
    
    [Header("Sound Effect")] 
    [Space(10)] 
    [SerializeField] private bool isSoundEffect;
    [SerializeField] private string soundName;
    
    [Header("Musics")] 
    [Space(10)] 
    [SerializeField] private bool isMusic;
    [SerializeField] private string musicName;
    
    void Start()
    {
        if (theSoundTrigger.isSoundEffect)
        {
            SoundManager.instance.PlaySE(theSoundTrigger.soundName);
        }
    }
}
