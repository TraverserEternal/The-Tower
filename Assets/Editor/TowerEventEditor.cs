
using System.IO;
using UnityEditor;
using UnityEngine;

public static class TowerEventEditor
{
  [MenuItem("Assets/Create/Events/Stateful/Int")]
  public static void createInt()
  {
    var towerEvent = ScriptableObject.CreateInstance<TowerEvent.Int>();
    Debug.Log(towerEvent);
    AssetDatabase.CreateAsset(towerEvent, Path.Combine(Application.dataPath, "new int"));
  }
}