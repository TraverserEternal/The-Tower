using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TowerEvent.WithState;
using UnityEngine;

public class Menu : MonoBehaviour
{
  private string persistentDataPath;
  public MenuSaveData[] saves;
  private void Start()
  {
    saves = new MenuSaveData[4] { new(), new(), new(), new() };
    persistentDataPath = Application.persistentDataPath;
    Parallel.ForEach(saves, async (save, loopState, index) =>
    {
      var DTO = await FileManager.DeserializeAsync<Dictionary<string, object>>(Path.Combine(persistentDataPath, index.ToString(), "menuData"));
      save.LoadDTO(DTO);
    });
  }
}

public class MenuSaveData : SaveData
{
  public SceneName sceneToLoad { get; private set; }

  public MenuSaveData() : base()
  {
    this.sceneToLoad.Set(SceneNames.None);
  }
}