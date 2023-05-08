using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Collider2D))]
public class SceneLoader : MonoBehaviour
{
  [SerializeField] List<string> scenes;
#pragma warning disable CS0108
  private Collider2D collider2D;
#pragma warning restore CS0108

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player")) SeamlessSceneManager.current.LoadScenes(scenes.ToArray());
  }
  private void Awake()
  {
    Debug.Log("Awake");
    scenes.Add(gameObject.scene.name);
  }

  #region Validation
  private void OnValidate()
  {
  }
  #endregion
}
