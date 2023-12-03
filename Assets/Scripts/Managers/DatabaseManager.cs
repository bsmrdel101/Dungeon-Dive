using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DatabaseManager
{
    [Header("Save Data")]
    public static Save Save;
    public static void SetSave (Save newSave) => Save = newSave;
    public static void SaveNewPartyMember (Player player) => Save.Party.Add(player);

    
    // C:/Users/firek/AppData/LocalLow/FirebugStudios/Dungeon Dive
    public static void SaveData(string fileName = "save")
    {
        string fullFilePath = Application.persistentDataPath + "/" + fileName + ".json";
        string json = JsonUtility.ToJson(Save);
        File.WriteAllText(fullFilePath, json);
    }

    public static void LoadData(string fileName = "save")
    {
        string fullFilePath = Application.persistentDataPath + "/" + fileName + ".json";
        if (File.Exists(fullFilePath))
        {
            string json = File.ReadAllText(fullFilePath);
            Save = JsonUtility.FromJson<Save>(json);
        }
        else
        {
            Save = null;
        }
    }
}
