using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SeamlessSceneManager : Singleton<SeamlessSceneManager>
{
  public float unloadDelay = 1f;

  private List<SceneNames> loadedScenes = new List<SceneNames>();
  private List<SceneNames> scenesToLoad = new List<SceneNames>();
  private Dictionary<SceneNames, float> sceneUnloadTimers = new Dictionary<SceneNames, float>();

  protected override void Awake()
  {
    SceneNames startingScene = (SceneNames)gameObject.scene.buildIndex;
    base.Awake();
    loadedScenes.Add(startingScene);
  }
  private void Update()
  {
    foreach (SceneNames sceneName in sceneUnloadTimers.Keys.ToArray())
    {
      if (Time.time >= sceneUnloadTimers[sceneName])
      {
        UnloadScene(sceneName);
      }
    }
  }

  public void LoadScenes(params SceneNames[] scenes)
  {
    foreach (SceneNames sceneName in scenes)
    {
      if (!loadedScenes.Contains(sceneName))
      {
        SceneManager.LoadScene(sceneName.ToString().Replace("_", " "), LoadSceneMode.Additive);
        loadedScenes.Add(sceneName);
      }

      if (!scenesToLoad.Contains(sceneName))
      {
        scenesToLoad.Add(sceneName);
      }

      sceneUnloadTimers.Remove(sceneName);
    }

    foreach (SceneNames loadedScene in loadedScenes)
    {
      if (!scenesToLoad.Contains(loadedScene))
      {
        sceneUnloadTimers[loadedScene] = Time.time + unloadDelay;
      }
    }

    scenesToLoad.Clear();
  }

  private void UnloadScene(SceneNames sceneName)
  {
    if (loadedScenes.Contains(sceneName))
    {
      Debug.Log("Unloading scene " + sceneName.ToString().Replace("_", " "));
      SceneManager.UnloadSceneAsync(sceneName.ToString().Replace("_", " "));
      loadedScenes.Remove(sceneName);
    }

    sceneUnloadTimers.Remove(sceneName);
  }
}