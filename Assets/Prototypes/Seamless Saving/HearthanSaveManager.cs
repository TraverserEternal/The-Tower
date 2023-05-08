using TowerEvent.WithState;

public class HearthanSaveManager : SaveManager<HearthanSaveData>
{
  public override string filePath => "hearthan";
}

public class HearthanSaveData : SaveData
{
  public Bool doorOpened { get; private set; }
  public HearthanSaveData() : base()
  {
    doorOpened.Set(false);
  }
}