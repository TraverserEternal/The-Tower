using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
  protected bool destroying;
  public static T c { get; private set; }
  protected virtual void Awake()
  {
    if (c != null && c != this)
    {
      Destroy(gameObject);
      destroying = true;
    }
    else
    {
      c = (T)this;
      DontDestroyOnLoad(gameObject);
    }
  }
  private void OnDestroy()
  {
    if (c == this) c = null;
  }
}