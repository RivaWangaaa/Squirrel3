using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public int sceneCount;
    public List<string> restartAcornsSave = new List<string>();
}


public class SaveAndLoad : MonoBehaviour
{
    private SaveData saveData = new SaveData();
    private GameMaster theGameMaster;

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";

    private void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";
        theGameMaster = FindObjectOfType<GameMaster>();
        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
        }
    }

    public void SaveData()
    {
        saveData.restartAcornsSave = GameMaster.saveAcorn;
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);
        
        Debug.Log("save complete");
        Debug.Log(json);
    }

    public void LoadData()
    {
        
    }
}
