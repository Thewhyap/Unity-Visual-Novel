using UnityEditor;
using UnityEngine;
using System.IO;

public class EngineFullBuild
{
    [MenuItem("Build/Build Full Version Engine")]
    public static void BuildFullVersionEngine()
    {
        // Build options
        string[] scenes = { $"{BuildUtils.SCENES_PATH}IntroScene.unity", $"{BuildUtils.SCENES_PATH}MainMenuScene.unity", $"{BuildUtils.SCENES_PATH}MainScene.unity" , $"{BuildUtils.SCENES_PATH}SettingsScene.unity" };
        string buildPath = "Build/EngineFullBuild";
        string sourceFolder = "AssetsRepository"; // (this should be in the root of the Unity project)

        BuildUtils.BuildProject(scenes, buildPath, BuildTarget.StandaloneWindows64, BuildOptions.None);

        string sourcePath = Path.Combine(Application.dataPath, "../", sourceFolder); // Root of Unity project

        BuildUtils.CopyFolder(sourcePath, buildPath);
    }
}

