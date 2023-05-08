using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SeamlessSceneManager : Singleton<SeamlessSceneManager>
{
  public static float unloadDelay = 1f;

  private List<Scene> loadedScenes = new();
  private List<Scene> scenesToLoad = new();
  private Dictionary<Scene, float> sceneUnloadTimers = new();

  protected override void Awake()
  {
    var startingScene = gameObject.scene;
    base.Awake();
    loadedScenes.Add(startingScene);
  }
  private void Update()
  {
    foreach (Scene scene in sceneUnloadTimers.Keys.ToArray())
    {
      if (Time.time >= sceneUnloadTimers[scene])
      {
        UnloadScene(scene);
      }
    }
  }

  public void LoadScenes(params Scene[] scenes)
  {
    foreach (Scene scene in scenes)
    {
      if (!loadedScenes.Contains(scene))
      {
        SceneManager.LoadScene(scene.buildIndex);
        loadedScenes.Add(scene);
      }

      if (!scenesToLoad.Contains(scene))
      {
        scenesToLoad.Add(scene);
      }

      sceneUnloadTimers.Remove(scene);
    }

    foreach (Scene loadedScene in loadedScenes)
    {
      if (!scenesToLoad.Contains(loadedScene))
      {
        sceneUnloadTimers[loadedScene] = Time.time + unloadDelay;
      }
    }

    scenesToLoad.Clear();
  }

  private void UnloadScene(Scene scene)
  {
    if (loadedScenes.Contains(scene))
    {
      SceneManager.UnloadSceneAsync(scene.buildIndex);
      loadedScenes.Remove(scene);
    }

    sceneUnloadTimers.Remove(scene);
  }
}