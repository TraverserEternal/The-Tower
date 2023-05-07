using UnityEngine;
using System.Linq;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

public abstract class SaveManager<T> : Singleton<SaveManager<T>> where T : SaveData, new()
{
  public string persistentDataPath;
  public T data;
  public TowerEvent.None loaded;
  public abstract string filePath { get; }
  private Action SaveData;
  protected override void Awake()
  {
    base.Awake();
    if (destroying) return;
    persistentDataPath = Application.persistentDataPath;
    FileManager.saveFolderPath.Set("Save1"); // Should be removed; Testing
    SaveData = Util.AddDebounce(SaveDataImmediate, 1000);
    prepareDataObject(SaveData);
  }

  private void prepareDataObject(System.Action listener)
  {
    data = new();
    typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
      .Where(p => p.GetValue(data) is TowerEvent.AnyStatefulTowerEvent).ToList().ForEach(property =>
      {
        TowerEvent.AnyTowerEvent dataPoint = (TowerEvent.AnyTowerEvent)property.GetValue(data);
        Debug.Log("Adding event listener to " + property.Name);
        dataPoint.Subscribe(listener);
      });
  }

  protected virtual void Start()
  {
    LoadData(FileManager.saveFolderPath);
  }
  private async void LoadData(string saveFolderPath)
  {
    var fileRep = await FileManager.DeserializeAsync<Dictionary<string, object>>(Path.Combine(
      persistentDataPath,
      FileManager.saveFolderPath,
      filePath));
    if (fileRep == null) Debug.Log("Data was null");
    else data.LoadDTO(fileRep);
    loaded.Trigger();
  }
  private async Task SaveDataImmediate()
  {
    Debug.Log("Saving Data!"); try
    {
      await FileManager.SerializeAsync(data.CreateDTO(), Path.Combine(
        persistentDataPath,
        FileManager.saveFolderPath,
        filePath));
    }
    catch (Exception error) { Debug.Log(error); }
  }

}



