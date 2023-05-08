using System;
using UnityEngine;

public interface AnyTowerEvent
{
  public void Subscribe(Action subscriber);
  public void Unsubscribe(Action subscriber);
}

public interface AnyStatefulTowerEvent : AnyTowerEvent
{
  public void AttemptSet(object value);
  public object objectV { get; }
}
[CreateAssetMenu(menuName = "Events/Tower Event")]
public class TowerEvent : ScriptableObject, AnyTowerEvent
{
  protected Action action { get; set; }

  public void Subscribe(Action subscriber)
  {
    action += subscriber;
  }
  public void Unsubscribe(Action subscriber)
  {
    action -= subscriber;
  }
  public virtual void Trigger()
  {
    if (action != null) action();
  }
  public abstract class WithState<T> : TowerEvent, AnyStatefulTowerEvent
  {
    public T v { get; private set; }
    public object objectV => (object)v;
    public static implicit operator T(WithState<T> stateful) => stateful.v;

    protected Action<T> statefulAction;
    public void Subscribe(Action<T> subscriber)
    {
      statefulAction += subscriber;
    }
    public void Unsubscribe(Action<T> subscriber)
    {
      statefulAction -= subscriber;
    }
    public void Set(T newValue)
    {
      v = newValue;
      if (action != null) action();
      if (statefulAction != null) statefulAction(newValue);
    }
    public void AttemptSet(object value)
    {
      Set((T)value);
    }
  }
  public class Int : WithState<int> { };
  public class IntArray : WithState<int[]> { };
  public class String : WithState<string> { };
  public class StringArray : WithState<string[]> { };
  public class Bool : WithState<bool> { };
  public class BoolArray : WithState<bool[]> { };
  public class Float : WithState<float> { };
  public class FloatArray : WithState<float[]> { };
}
