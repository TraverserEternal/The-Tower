using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class SceneLoader : MonoBehaviour
{
  [SerializeField] List<SceneAsset> scenesToLoad;
  private List<Scene> scenes;
#pragma warning disable CS0108
  private Collider2D collider2D;
#pragma warning restore CS0108

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player")) SeamlessSceneManager.current.LoadScenes(scenes.ToArray());
  }
  private void Start()
  {
    scenes = scenes.Where(s => s != null).ToList();
  }

  #region Validation
  private void OnValidate()
  {
    collider2D = GetComponent<Collider2D>();
    scenes = scenesToLoad.Select(sceneAsset => SceneManager.GetSceneByName(sceneAsset.name)).ToList();
    if (scenes.Count != scenesToLoad.Count) Debug.LogError($"You have selected scenes for {gameObject.name} that are not in the build order.");
    var sameScene = scenes.Find(scene => scene.buildIndex == gameObject.scene.buildIndex);
    if (sameScene.name == null) return;
    Debug.LogWarning($"You added the scene that {gameObject.name} belongs to. This isn't necessary.");
    scenes.Remove(sameScene);
    scenesToLoad.Remove(scenesToLoad.Find(sceneAsset => sceneAsset.name == sameScene.name));
#if DEBUG
    EditorUtility.SetDirty(this);
#endif
  }
  #endregion
}
