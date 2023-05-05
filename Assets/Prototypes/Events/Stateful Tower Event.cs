public partial class TowerEvent
{
  public class StatefulEvent<T>
  {
    public static implicit operator T(StatefulEvent<T> stateful) => stateful.v;
    public T v;
    public System.Action<T> onChange;
    public virtual void Set(T newValue)
    {
      v = newValue;
      if (onChange != null) onChange(newValue);
    }
  }
}
