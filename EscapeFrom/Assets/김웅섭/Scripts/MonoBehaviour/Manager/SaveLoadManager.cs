using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoadManager
{    
    /// <summary>
    /// Create file and write object
    /// </summary>
    /// <param name="path">Path to save file. contains Application.datapath</param>
    /// <param name="obj">Object to write file</param>
    /// <typeparam name="T">Typeof object</typeparam>
    public static void Save(string path, object obj)
    {
        var directorys = path.Split("/");
        
        StringBuilder checkPath = new StringBuilder();
        checkPath.Append(Application.dataPath);
        for(int i = 0; i < directorys.Length - 1; i++)
        {
            checkPath.Append('/' + directorys[i]);
            
            if(!Directory.Exists(checkPath.ToString())) 
                Directory.CreateDirectory(checkPath.ToString());
        }
        checkPath.Append('/' + directorys[directorys.Length - 1]);

        var jsonStr = JsonUtility.ToJson(obj);
        File.WriteAllText(checkPath.ToString(), jsonStr);
    }

    /// <summary>
    /// Read file and overrite at object
    /// </summary>
    /// <param name="path">Path to read file, contains Application.datapath</param>
    /// <param name="obj2overrite">Object to overrite</param>
    /// <typeparam name="T">Typeof object</typeparam>
    /// <returns>Return false if load is failure</returns>
    public static bool Load<T>(string path, ref T obj2overrite)
    {
        var directorys = path.Split("/");

        StringBuilder checkPath = new StringBuilder();
        checkPath.Append(Application.dataPath);
        for(int i = 0; i < directorys.Length - 1; i++)
        {
            checkPath.Append('/' + directorys[i]);
            
            if(!Directory.Exists(checkPath.ToString())) 
                return false;
        }
        checkPath.Append('/' + directorys[directorys.Length - 1]);

        if(!File.Exists(checkPath.ToString())) return false;

        var jsonStr = File.ReadAllText(checkPath.ToString());
        JsonUtility.FromJsonOverwrite(jsonStr, obj2overrite);
        return true;
    }
}
