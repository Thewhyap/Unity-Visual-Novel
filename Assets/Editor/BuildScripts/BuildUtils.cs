using UnityEditor;
using UnityEngine;
using System.IO;

public static class BuildUtils
{
    private const string projectName = "TaleTaleEngine.exe";

    // Common method to handle the build process
    public static void BuildProject(string[] scenes, string buildPath, BuildTarget buildTarget, BuildOptions buildOptions)
    {
        string projectPath = Path.Combine(buildPath, projectName);
        BuildPipeline.BuildPlayer(scenes, projectPath, buildTarget, buildOptions);
        Debug.Log("Build completed at: " + projectPath);
    }

    // Method to copy folder from source to destination
    public static void CopyFolder(string sourceFolder, string destinationPath)
    {
        if (!Directory.Exists(destinationPath))
        {
            Directory.CreateDirectory(destinationPath);
        }

        foreach (string file in Directory.GetFiles(sourceFolder))
        {
            string dest = Path.Combine(destinationPath, Path.GetFileName(file));
            File.Copy(file, dest);
        }

        foreach (string folder in Directory.GetDirectories(sourceFolder))
        {
            string dest = Path.Combine(destinationPath, Path.GetFileName(folder));
            CopyFolder(folder, dest);
        }

        Debug.Log("Folder copied to: " + destinationPath);
    }
}

