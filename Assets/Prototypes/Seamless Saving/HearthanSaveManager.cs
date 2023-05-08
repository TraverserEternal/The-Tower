public class HearthanSaveManager : SaveManager<HearthanSaveData>
{
  public override string filePath => "hearthan";
}

public class HearthanSaveData : SaveData
{
  public TowerEvent.Bool doorOpened { get; private set; }
  public HearthanSaveData() : base()
  {
    doorOpened.Set(false);
  }
}