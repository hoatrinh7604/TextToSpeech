using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveController : MonoBehaviour
{
    string saveFile = "";

    private void Awake()
    {
        saveFile = Application.persistentDataPath + "/Data.txt";
    }

    public string ReadFile()
    {
        try
        {
            var result = File.ReadAllText(saveFile);
            return result;
        }catch(System.Exception e)
        {
            return "";
        }
    }

    public bool WriteFile(string jsonString)
    {
        try
        {
            File.WriteAllText(saveFile, jsonString);
            return true;
        }catch(System.Exception e)
        {
            return false;
        }
    }

    private string AddExtension(string filename)
    {
        return filename + ".txt";
    }

}
