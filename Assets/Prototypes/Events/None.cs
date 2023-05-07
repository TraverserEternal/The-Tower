using System;
using System.Runtime.Serialization;
using UnityEngine;
namespace TowerEvent
{
  [CreateAssetMenu(menuName = "Events/Tower Event")]
  public partial class None : ScriptableObject, AnyTowerEvent
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
  }

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

  [DataContract(Name = "Stateful")]
  public abstract class Stateful<T> : ScriptableObject, AnyStatefulTowerEvent
  {
    [DataMember(Name = "value")]
    public T v { get; private set; }
    public object objectV => (object)v;
    public static implicit operator T(Stateful<T> stateful) => stateful.v;
    protected Action action { get; set; }

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

    public void Subscribe(Action subscriber)
    {
      action += subscriber;
    }

    public void Unsubscribe(Action subscriber)
    {
      action -= subscriber;
    }

    public void AttemptSet(object value)
    {
      Set((T)value);
    }
  }
}