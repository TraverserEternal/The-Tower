using UnityEngine;
using System.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

public abstract class SaveManager<T, T2> : Singleton<SaveManager<T, T2>> where T : SaveData<T2>, new()
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
    data = new();
    persistentDataPath = Application.persistentDataPath;
    FileManager.saveFolderPath.Set("Save1");
    SaveData = Util.AddDebounce(SaveDataImmediate, 1000);
  }
  private void Start()
  {
    LoadData(FileManager.saveFolderPath);
  }
  private async void LoadData(string saveFolderPath)
  {
    var fileRep = await FileManager.DeserializeAsync<T2>(Path.Combine(
      persistentDataPath,
      FileManager.saveFolderPath,
      filePath));
    if (fileRep == null) Debug.Log("Data was null");
    else data.LoadSaveFileRepresentation(fileRep);
    typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
      .Where(p => p.GetValue(data) is TowerEvent.AnyTowerEvent).ToList().ForEach(property =>
    {
      TowerEvent.AnyTowerEvent dataPoint = (TowerEvent.AnyTowerEvent)property.GetValue(data);
      Debug.Log("Adding event listener to " + property.Name);
      dataPoint.Subscribe(SaveData);
    });
    loaded.Trigger();
  }
  private async Task SaveDataImmediate()
  {
    Debug.Log("Saving Data!"); try
    {
      await FileManager.SerializeAsync<T2>(data.CreateSaveFileRepresentation(), Path.Combine(
        persistentDataPath,
        FileManager.saveFolderPath,
        filePath));
    }
    catch (Exception error) { Debug.Log(error); }
  }

}



