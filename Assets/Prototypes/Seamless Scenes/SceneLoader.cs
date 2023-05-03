using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class SceneLoader : MonoBehaviour
{
  [SerializeField] List<SceneNames> sceneNames;
  private new Collider2D collider2D;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player")) SeamlessSceneManager.current.LoadScenes(sceneNames.ToArray());
    Debug.Log("Triggered!");
  }

  #region Validation
  private void OnValidate()
  {
    collider2D = GetComponent<Collider2D>();
  }
  [InitializeOnLoadMethod]
  private static void RegisterPlayModeStateChange()
  {
    EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
  }

  private static void OnPlayModeStateChanged(PlayModeStateChange state)
  {
    if (state == PlayModeStateChange.ExitingEditMode)
    {
      // Save any changes made to the component before entering play mode
      foreach (var obj in Object.FindObjectsOfType<SceneLoader>())
      {
      }
      AssetDatabase.SaveAssets();
    }
  }
  public void PrepareSceneList()
  {
    List<SceneNames> uniqueSceneNames = new List<SceneNames>();
    uniqueSceneNames.Add((SceneNames)gameObject.scene.buildIndex);
    foreach (SceneNames e in sceneNames)
    {
      if (!uniqueSceneNames.Contains(e)) uniqueSceneNames.Add(e);
    }
    if (new HashSet<SceneNames>(sceneNames).Equals(new HashSet<SceneNames>(uniqueSceneNames)))
    {
      sceneNames = uniqueSceneNames;
      EditorUtility.SetDirty(this);
    }
  }
  #endregion
}
