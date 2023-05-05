using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Tower Event")]
public class ScriptableTowerEvent : ScriptableObject
{
  public TowerEvent e = new();
  public abstract class StatefulScriptableTowerEvent<T> : ScriptableObject
  {
    public TowerEvent.StatefulEvent<T> e = new();
  }
  public abstract class WithState
  {
    [CreateAssetMenu(menuName = "Events/With State/Int")]
    public class Int : StatefulScriptableTowerEvent<int> { };
    [CreateAssetMenu(menuName = "Events/With State/String")]
    public class String : StatefulScriptableTowerEvent<string> { };
    [CreateAssetMenu(menuName = "Events/With State/Bool")]
    public class Bool : StatefulScriptableTowerEvent<bool> { };
  }
}
