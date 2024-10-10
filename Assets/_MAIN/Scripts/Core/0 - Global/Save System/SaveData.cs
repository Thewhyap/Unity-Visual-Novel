using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string saveName;
    public int run;
    public string currentFile;
    public int[] position;
    public Dictionary<string, object> gameVariables;

    public SaveData(int run)
    {
        saveName = string.Empty;
        this.run = run;
        currentFile = "Main";
        position = new int[] {0,0};
        gameVariables = new Dictionary<string, object>();
    }
}

