using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;

public static class FileManager
{
  public static TowerEvent.WithState.String saveFolderPath = ScriptableObject.CreateInstance<TowerEvent.WithState.String>();

  public static async Task SerializeAsync<T>(T obj, string filePath)
  {
    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
    using (var fileStream = new FileStream(filePath + ".bin", FileMode.Create))
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
    if (!File.Exists(filePath + ".bin")) return default(T);
    using (var fileStream = new FileStream(filePath + ".bin", FileMode.Open))
    {
      return await Task.Run(() =>
      {
        BinaryFormatter formatter = new BinaryFormatter();
        return (T)formatter.Deserialize(fileStream);
      });
    }
  }
}