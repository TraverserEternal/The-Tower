using System.Collections;
using System.Collections.Generic;
using TowerEvent.WithState;
using UnityEngine;

public class HearthanSaveManager : SaveManager<HearthanSaveData, HearthanSaveData.HearthanSaveFileRepresentation>
{
  public override string filePath => "hearthan";
}

public class HearthanSaveData : SaveData<HearthanSaveData.HearthanSaveFileRepresentation>
{
  public TowerEvent.WithState.Bool doorUnlocked { get; private set; }
  public HearthanSaveData()
  {
    doorUnlocked = ScriptableObject.CreateInstance<TowerEvent.WithState.Bool>();
    doorUnlocked.Set(false);
  }

  [System.Serializable]
  public class HearthanSaveFileRepresentation
  {
    public bool doorUnlocked;

    public HearthanSaveFileRepresentation(bool doorUnlocked)
    {
      this.doorUnlocked = doorUnlocked;
    }
  }

  public void LoadSaveFileRepresentation(HearthanSaveFileRepresentation representation)
  {
    doorUnlocked.Set(representation.doorUnlocked);
  }

  public HearthanSaveFileRepresentation CreateSaveFileRepresentation()
  {
    return new HearthanSaveFileRepresentation(doorUnlocked);
  }
}