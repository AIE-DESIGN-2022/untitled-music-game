using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class SaveToFile : MonoBehaviour
{
    public string fileName = "/Save.dat";
    public void Load()
    {
        string dest = Application.persistentDataPath + fileName;
        FileStream file;
        if (File.Exists(dest)) file = File.OpenRead(dest);
        else
        {
            print("uh oh no file");
            return;
        }
        SaveData saveDat = new SaveData();
        BinaryFormatter bf = new BinaryFormatter();
        SaveData tempSave = (SaveData)bf.Deserialize(file);
        SaveManager.instance.HighestLevel = tempSave.highestLevel;
        file.Close();
    }
    public void Save()
    {
        string dest = Application.persistentDataPath + fileName;
        FileStream file;
        if (File.Exists(dest)) file = File.OpenWrite(dest);
        else file = File.Create(dest);

        SaveData saveDat = new SaveData();
        saveDat.highestLevel = SaveManager.instance.HighestLevel;

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, saveDat);
        file.Close();
    }
    [System.Serializable]
    class SaveData
    {
        public int highestLevel;
    }
}
