using UnityEngine;

public class HearthanSaveManager : SaveManager<HearthanSaveData>
{
  public override string filePath => "hearthan";
}

public class HearthanSaveData : SaveData
{
  public TowerEvent.WithState.Bool doorUnlocked { get; private set; }
  public HearthanSaveData() : base()
  {
    doorUnlocked.Set(false);
  }
}