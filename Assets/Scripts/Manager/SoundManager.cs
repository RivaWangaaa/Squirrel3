using System.Collections;
using System.Collections.Generic;
using UnityEngine;


   [System.Serializable]
    public class Sound
    {
        public string name; //song's name
        public AudioClip clip;
    }

public class SoundManager : MonoBehaviour
{

    //singleton 씬으로 넘어가도 계속 기존에 있던것만 유지하고 새로 생기는것을 없애버림
    //공유자원으로 사용
    static public SoundManager instance;

    #region singleton

    void Awake()
    {
        //instance가 아무것도 없는 껍데기이면 == 처음 실행하면
        if (instance == null)
        {
            instance = this; //선언만된 껍데기에 자기자신을 넣음
            DontDestroyOnLoad(gameObject); //다음신으로 가도 이거 삭제하지 않게하는것
        }

        else
            Destroy(gameObject);
    }

    #endregion singleton

    public AudioSource[] audioSourceEffects;
    public AudioSource audioSourceBgm;

    public string[] playSoundName;

    public Sound[] effectSounds;
    public Sound[] bgmSounds;

    void Start()
    {
        playSoundName = new string[audioSourceEffects.Length];
    }

    public void PlaySE(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            {
                if (_name == effectSounds[i].name)
                    for (int j = 0; j < audioSourceEffects.Length; j++)
                    {
                        if (!audioSourceEffects[j].isPlaying)
                        {
                            playSoundName[j] = effectSounds[i].name;
                            audioSourceEffects[j].clip = effectSounds[i].clip;
                            audioSourceEffects[j].Play();
                            return;
                        }
                    }

                Debug.Log("Audio Source is still playing.");
            }
        }

        Debug.Log(_name + "The sound is not in the lists.");
    }
    
    public void StopAllSE()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (playSoundName[i] == _name)
            {
                audioSourceEffects[i].Stop();
                return;
            }
        }
        Debug.Log("재생 중인" + _name + "사운드가 없습니다.");
    }
}
