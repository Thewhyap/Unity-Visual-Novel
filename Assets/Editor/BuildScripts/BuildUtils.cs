using UnityEditor;
using UnityEngine;
using System.IO;

public static class BuildUtils
{
    private const string PROJECT_NAME = "TaleTaleEngine.exe";
    public const string SCENES_PATH = "Assets/Scenes/";

    public static void BuildProject(string[] scenes, string buildPath, BuildTarget buildTarget, BuildOptions buildOptions)
    {
        string projectPath = Path.Combine(buildPath, PROJECT_NAME);
        BuildPipeline.BuildPlayer(scenes, projectPath, buildTarget, buildOptions);
        Debug.Log("Build completed at: " + projectPath);
    }

    public static void CopyFolder(string sourceFolder, string destinationPath)
    {
        if (!Directory.Exists(destinationPath))
        {
            Directory.CreateDirectory(destinationPath);
            Debug.Log($"Copied folder: {destinationPath}");
        }

        foreach (string file in Directory.GetFiles(sourceFolder))
        {
            string dest = Path.Combine(destinationPath, Path.GetFileName(file));
            if (!File.Exists(dest))
            {
                File.Copy(file, dest);
                Debug.Log($"Copied file: {file}");
            }
            else
            {
                Debug.Log($"File already exists: {file}");
            }
        }

        foreach (string folder in Directory.GetDirectories(sourceFolder))
        {
            string dest = Path.Combine(destinationPath, Path.GetFileName(folder));
            CopyFolder(folder, dest);
        }
    }
}

