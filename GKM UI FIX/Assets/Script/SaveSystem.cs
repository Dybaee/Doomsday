using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSystem : MonoBehaviour
{
    private string saveFileName = "savegame.json";

    public void SaveGame()
    {
        // Collect data
        SaveData saveData = new SaveData();
        saveData.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        string jsonData = JsonUtility.ToJson(saveData);
        // Save JSON 
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
        File.WriteAllText(filePath, jsonData);
    }

    public void LoadGame()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
        if (File.Exists(filePath))
        {
            // Read JSON file
            string jsonData = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(jsonData);
            // Load data 
            SceneManager.LoadScene(saveData.sceneIndex);
        }
        else
        {
            Debug.LogWarning("No save file");
        }
    }

    public void DeleteSaveFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Save file deleted");
        }
        else
        {
            Debug.Log("No save file to delete");
        }
    }

    public void NewGame()
    {
        DeleteSaveFile();
        SceneManager.LoadScene(1); 
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}

[System.Serializable]
public class SaveData
{
    public int sceneIndex;
}
