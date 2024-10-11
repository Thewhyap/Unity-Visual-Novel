using UnityEditor;
using UnityEngine;
using System.IO;

public class GameFullBuild
{
    [MenuItem("Build/Build Full Game Version ")]
    public static void BuildFullGameVersion()
    {
        // Build options
        string[] scenes = { "Assets/Scenes/MainScene.unity" };
        string buildPath = "Build/GameFullBuild";

        BuildUtils.BuildProject(scenes, buildPath, BuildTarget.StandaloneWindows64, BuildOptions.None);
    }
}
