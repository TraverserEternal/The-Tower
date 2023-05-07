using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SaveData
{
  public SaveData()
  {
    var properties = GetType().GetProperties();
    foreach (var property in properties)
    {
      if (typeof(TowerEvent.AnyStatefulTowerEvent).IsAssignableFrom(property.PropertyType))
      {
        var towerEvent = ScriptableObject.CreateInstance(property.PropertyType);
        property.SetValue(this, towerEvent);
      }
    }
  }

  private void Initialize()
  {
    throw new NotImplementedException();
  }
  public virtual Dictionary<string, object> CreateDTO()
  {
    var dto = new Dictionary<string, object>();
    var properties = GetType().GetProperties();
    foreach (var property in properties)
    {
      var stateful = property.GetValue(this) as TowerEvent.AnyStatefulTowerEvent;
      if (stateful != null)
      {
        dto[property.Name] = stateful.objectV;
      }
    }
    return dto;
  }
  public virtual void LoadDTO(Dictionary<string, object> dto)
  {
    var properties = GetType().GetProperties();
    var missingProperties = new List<string>();
    foreach (var property in properties)
    {
      if (dto.TryGetValue(property.Name, out var value))
      {
        var stateful = property.GetValue(this) as TowerEvent.AnyStatefulTowerEvent;
        if (stateful != null) stateful.AttemptSet(value);
      }
      else
      {
        missingProperties.Add(property.Name);
      }
    }

    if (missingProperties.Any() || dto.Keys.Except(properties.Select(p => p.Name)).Any())
    {
      throw new ArgumentException($"DTO does not match the expected stateful properties. Missing properties: {string.Join(", ", missingProperties)}. Extra properties: {string.Join(", ", dto.Keys.Except(properties.Select(p => p.Name)))}");
    }
  }
}