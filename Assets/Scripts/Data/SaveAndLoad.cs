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
    public SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";
    
    private void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";
        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
        }
    }

    public void SaveData()
    {
        //acorns save
        saveData.restartAcornsSave = GameMaster.saveAcorn;
        
        //scene save(if need)
        
        //player position save(if need)
        
        //save the info
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);
        
        Debug.Log("save complete");
        Debug.Log(json);
    }

    public void LoadData()
    {
        
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";
        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
        }
        
        Debug.Log("Starting load data");
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            GameMaster.saveAcorn = saveData.restartAcornsSave;
            Debug.Log("Load Complete");
        }
        else
        { 
            Debug.Log("There is no load data");
        }
    }
    

    public void ResetData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            saveData.restartAcornsSave.Clear();
            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);
        }
    }
}
