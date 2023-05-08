using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SeamlessSceneManager : Singleton<SeamlessSceneManager>
{
  public static float unloadDelay = 1f;

  private List<string> loadedScenes = new();
  private List<string> scenesToLoad = new();
  private Dictionary<string, float> sceneUnloadTimers = new();

  protected override void Awake()
  {
    var startingScene = gameObject.scene.name;
    base.Awake();
    loadedScenes.Add(startingScene);
  }
  private void Update()
  {
    foreach (string scene in sceneUnloadTimers.Keys.ToArray())
    {
      if (Time.time >= sceneUnloadTimers[scene])
      {
        UnloadScene(scene);
      }
    }
  }

  public void LoadScenes(params string[] scenes)
  {
    foreach (string scene in scenes)
    {
      if (!loadedScenes.Contains(scene))
      {
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        loadedScenes.Add(scene);
      }

      if (!scenesToLoad.Contains(scene))
      {
        scenesToLoad.Add(scene);
      }

      sceneUnloadTimers.Remove(scene);
    }

    foreach (string loadedScene in loadedScenes)
    {
      if (!scenesToLoad.Contains(loadedScene))
      {
        sceneUnloadTimers[loadedScene] = Time.time + unloadDelay;
      }
    }

    scenesToLoad.Clear();
  }

  private void UnloadScene(string scene)
  {
    if (loadedScenes.Contains(scene))
    {
      SceneManager.UnloadSceneAsync(scene);
      loadedScenes.Remove(scene);
    }

    sceneUnloadTimers.Remove(scene);
  }
}