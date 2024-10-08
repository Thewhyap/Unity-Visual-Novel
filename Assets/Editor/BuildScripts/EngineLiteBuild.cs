using UnityEditor;
using UnityEngine;
using System.IO;

public class EngineLiteBuild
{
    [MenuItem("Build/Build Lite Version Engine ")]
    public static void BuildLiteVersionEngine()
    {
        // Build options
        string[] scenes = { "Assets/Scenes/MainScene.unity" };
        string buildPath = "Build/EngineLiteBuild";
        string sourceFolder = "AssetsRepository"; // (this should be in the root of the Unity project)

        BuildUtils.BuildProject(scenes, buildPath, BuildTarget.StandaloneWindows64, BuildOptions.None);

        string sourcePath = Path.Combine(Application.dataPath, "../", sourceFolder); // Root of Unity project

        BuildUtils.CopyFolder(sourcePath, buildPath);
    }
}
