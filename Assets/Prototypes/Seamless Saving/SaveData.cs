public interface SaveData<T>
{
  public abstract void LoadSaveFileRepresentation(T representation);
  public abstract T CreateSaveFileRepresentation();
}