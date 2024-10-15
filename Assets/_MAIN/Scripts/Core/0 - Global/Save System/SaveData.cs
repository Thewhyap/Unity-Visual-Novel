using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string currentStoryFile;
    public int[] position;
    public int run;
    public Dictionary<string, object> gameVariables;

    public GameData(int run)
    {
        currentStoryFile = "Main";
        position = new int[] { 0, 0 };
        this.run = run;
        gameVariables = new Dictionary<string, object>();
    }
}

[System.Serializable]
public class HeaderData
{
    public int slot;
    public string saveName;
    public DateTime lastUpdate;
    public int run;
    public string color;
    public string screenshot;

    public HeaderData(int slot, GameData data)
    {
        this.slot = slot;
        saveName = data.currentStoryFile; //TODO
        lastUpdate = DateTime.Now;
        this.run = data.run;
        color = string.Empty; //TODO change with default color or color depending on the data
        screenshot = "lastScreenshot"; //TODO
    }
}

