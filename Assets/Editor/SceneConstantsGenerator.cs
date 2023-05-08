using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public static class SceneConstantsGenerator
{
  [MenuItem("Tools/Generate Scenes Enum")]
  public static void GenerateScenesEnum()
  {
    List<string> sceneNames = new List<string>();
    int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;

    for (int i = 0; i < sceneCount; i++)
    {
      string scenePath = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i);
      string sceneName = Path.GetFileNameWithoutExtension(scenePath);
      sceneNames.Add(sceneName);
    }

    string outputFilePath = Application.dataPath + "/Prototypes/Seamless Scenes/SceneNames.cs";

    using (StreamWriter writer = new StreamWriter(outputFilePath))
    {
      writer.WriteLine("  public enum SceneNames");
      writer.WriteLine("  {");
      writer.WriteLine("    " + "None,");

      foreach (string sceneName in sceneNames)
      {
        writer.WriteLine("    " + sceneName.Replace(" ", "_") + ",");
      }
      writer.WriteLine("  }");
    }

    AssetDatabase.Refresh();
  }
}