using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;

public abstract class SaveManager<T> : Singleton<SaveManager<T>> where T : SaveData
{
  public T data;
  public TowerEvent loaded = new TowerEvent();
  public abstract string filePath { get; }
  private void Start()
  {
    FileManager.saveFolderPath.onChange += AutoLoadData;
    if (FileManager.saveFolderPath != null) AutoLoadData(FileManager.saveFolderPath);
  }
  private async void AutoLoadData(string saveFolderPath)
  {
    data = await FileManager.DeserializeAsync<T>(Application.persistentDataPath + saveFolderPath + filePath);
    loaded.Trigger();
  }
}

public static class FileManager
{
  public static TowerEvent.StatefulEvent<string> saveFolderPath = new();

  public static async Task SerializeAsync<T>(T obj, string filePath)
  {
    using (var fileStream = new FileStream(filePath, FileMode.Create))
    {
      await Task.Run(() =>
      {
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fileStream, obj);
      });
    }
  }

  public static async Task<T> DeserializeAsync<T>(string filePath)
  {
    using (var fileStream = new FileStream(filePath, FileMode.Open))
    {
      return await Task.Run(() =>
      {
        BinaryFormatter formatter = new BinaryFormatter();
        return (T)formatter.Deserialize(fileStream);
      });
    }
  }
}

public interface SaveData { }

public record HearthanSaveData : SaveData
{

}

