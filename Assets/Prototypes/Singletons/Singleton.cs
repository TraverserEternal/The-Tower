using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
  public static T current { get; private set; }
  protected virtual void Awake()
  {
    if (current != null && current != this)
    {
      Destroy(gameObject);
    }
    else
    {
      current = (T)this;
      DontDestroyOnLoad(gameObject);
    }
  }
}