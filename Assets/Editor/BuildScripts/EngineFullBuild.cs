using UnityEditor;
using UnityEngine;
using System.IO;

public class EngineFullBuild
{
    [MenuItem("Build/Build Full Version Engine")]
    public static void BuildFullVersionEngine()
    {
        // Build options
        string[] scenes = { "Assets/Scenes/IntroScene.unity", "Assets/Scenes/MainMenuScene.unity", "Assets/Scenes/MainScene.unity" };
        string buildPath = "Build/EngineFullBuild";
        string sourceFolder = "AssetsRepository"; // (this should be in the root of the Unity project)

        BuildUtils.BuildProject(scenes, buildPath, BuildTarget.StandaloneWindows64, BuildOptions.None);

        string sourcePath = Path.Combine(Application.dataPath, "../", sourceFolder); // Root of Unity project

        BuildUtils.CopyFolder(sourcePath, buildPath);
    }
}

