using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperGameManager : MonoBehaviour
{
  public static JumperGameManager c { get; private set; }
  public JStatefulEvent<int> numCoins = new JStatefulEvent<int>();
  private void Awake()
  {
    if (c != null) Destroy(gameObject);
    else
    {
      c = this;
      numCoins.onChange += logCoins;
    }
  }

  private void logCoins(int coins)
  {
    Debug.Log(coins);
  }
}

public class JEvent
{
  public System.Action onFire;
  public void Trigger()
  {
    if (onFire != null) onFire();
  }
}

public class JStatefulEvent<T>
{
  public T v { get; private set; }
  public System.Action<T> onChange;
  public void Set(T newValue)
  {
    v = newValue;
    if (onChange != null) onChange(v);
  }
}