using System;
using UnityEngine;

public abstract class SaveListener<T> : MonoBehaviour
{
  public abstract TowerEvent.WithState<T> GetSaveData();

  private bool hasSubscribed;
  protected virtual void OnEnable()
  {
    try { Init(); } catch { Debug.Log("Save Data Not loaded yet"); }
  }

  private void Init()
  {
    GetSaveData().Subscribe(OnSaveDataChange);
    Debug.Log("Initializing...");
    OnSaveDataChange(GetSaveData().v);
    hasSubscribed = true;
  }

  protected virtual void Start()
  {
    if (hasSubscribed) return;
    try { Init(); } catch { throw new Exception("There isn't a save manager!"); }
  }
  protected virtual void OnDisable()
  {
    GetSaveData().Unsubscribe(OnSaveDataChange);
  }

  protected abstract void OnSaveDataChange(T newValue);
}