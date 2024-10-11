using UnityEditor;
using UnityEngine;
using System.IO;

public class GameCustomBuild
{
    [MenuItem("Build/Build Custom Game Version ")]
    public static void BuildCustomGameVersion()
    {
        // Build options
        string[] scenes = { "Assets/Scenes/MainScene.unity" };
        string buildPath = "Build/GameCustomBuild";

        BuildUtils.BuildProject(scenes, buildPath, BuildTarget.StandaloneWindows64, BuildOptions.None);
    }
}
